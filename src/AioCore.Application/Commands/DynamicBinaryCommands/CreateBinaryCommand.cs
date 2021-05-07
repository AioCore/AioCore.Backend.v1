using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Package.Extensions;
using Package.FileServer;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicBinaryCommands
{
    public class CreateBinaryCommand : IRequest<string>
    {
        public IFormFile File { get; set; }

        public Guid ParentId { get; set; }

        internal class Handler : IRequestHandler<CreateBinaryCommand, string>
        {
            private readonly int[] _imageSizes = { 25, 50, 100, 200, 400, 800, 1600 };

            private readonly IFileServerService _fileServerService;
            private readonly IDynamicBinaryRepository _dynamicBinaryRepository;

            public Handler(IFileServerService fileServerService, IDynamicBinaryRepository dynamicBinaryRepository)
            {
                _fileServerService = fileServerService ?? throw new ArgumentNullException(nameof(fileServerService));
                _dynamicBinaryRepository = dynamicBinaryRepository ?? throw new ArgumentNullException(nameof(dynamicBinaryRepository));
            }

            public async Task<string> Handle(CreateBinaryCommand request, CancellationToken cancellationToken)
            {
                var fileId = Guid.NewGuid().ToString("N");

                var file = request.File;

                var fullPath = GetPhysicalPath(file.FileName);

                var fileExtension = Path.GetExtension(file.FileName);

                var buffer = file.OpenReadStream().ReadToEnd();

                var filePath = $"{fullPath}\\{fileId}";

                if (request.File.ContentType.StartsWith("image"))
                {
                    var files = new List<DynamicBinary>();

                    var sourceBitmap = SKBitmap.Decode(buffer);

                    var ratio = (float)sourceBitmap.Width / (float)sourceBitmap.Height;

                    foreach (var size in _imageSizes)
                    {
                        var filePathResize = filePath + $"-x{size}" + fileExtension;

                        var targetHeight = (int)(size / ratio);

                        var skImageInfo = new SKImageInfo(size, targetHeight);

                        const SKFilterQuality quality = SKFilterQuality.High;

                        var scaledBitmap = sourceBitmap.Resize(skImageInfo, quality);

                        var scaledImage = SKImage.FromBitmap(scaledBitmap);

                        var data = scaledImage.Encode();

                        var fileResize = data.ToArray();

                        await _fileServerService.UploadFileAsync(filePathResize, fileResize);

                        files.Add(new DynamicBinary
                        {
                            ParentId = request.ParentId,

                            FileName = file.FileName,

                            FilePath = filePathResize,

                            Created = DateTimeOffset.Now,

                            Size = fileResize.Length,

                            ContentType = file.ContentType
                        });
                    }

                    _dynamicBinaryRepository.AddRange(files);

                    await _dynamicBinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    return fileId;
                }

                var filePathExtension = filePath + fileExtension;

                var binary = new DynamicBinary
                {
                    ParentId = request.ParentId,

                    FileName = file.FileName,

                    FilePath = filePathExtension,

                    Created = DateTimeOffset.Now,

                    Size = file.Length,

                    ContentType = file.ContentType
                };
                _dynamicBinaryRepository.Add(binary);

                await _fileServerService.UploadFileAsync(filePathExtension, buffer);

                await _dynamicBinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return fileId;
            }

            private static string GetPhysicalPath(string fileName)
            {
                var fileExtension = Path.GetExtension(fileName)?.Replace(".", string.Empty).ToLower();
                return $"{DirectoryExtensions.GenerateDirectory()}\\{fileExtension}";
            }
        }

        public void MergeParams(Guid parentId, IFormFile file)
        {
            ParentId = parentId;
            File = file;
        }
    }
}