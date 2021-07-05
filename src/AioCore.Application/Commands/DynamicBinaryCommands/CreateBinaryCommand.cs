using AioCore.Domain.CoreEntities;
using AioCore.Domain.Models;
using AioCore.Mediator;
using Microsoft.AspNetCore.Http;
using Package.Extensions;
using Package.FileServer;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.UnitOfWorks;

namespace AioCore.Application.Commands.DynamicBinaryCommands
{
    public class CreateBinaryCommand : IRequest<string>
    {
        public IFormFile File { get; set; }

        public Guid? ParentId { get; set; }

        internal class Handler : IRequestHandler<CreateBinaryCommand, string>
        {
            private readonly int[] _imageSizes = { 100, 200, 400, 800, 1600 };

            private readonly IFileServerService _fileServerService;
            private readonly IAioCoreUnitOfWork _context;

            public Handler(
                IFileServerService fileServerService,
                IAioCoreUnitOfWork context)
            {
                _fileServerService = fileServerService;
                _context = context;
            }

            public async Task<Response<string>> Handle(CreateBinaryCommand request, CancellationToken cancellationToken)
            {
                var file = request.File;

                var fileId = file.FileName.CreateMd5().ToLower();

                var fullPath = GetPhysicalPath(file.FileName);

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                var buffer = file.OpenReadStream().ReadToEnd();

                var filePath = $"{fullPath}\\{fileId}";

                if (request.File.ContentType.StartsWith("image"))
                {
                    var files = new List<SystemBinary>();

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

                        files.Add(new SystemBinary
                        {
                            ParentId = request.ParentId,

                            SourceName = file.FileName,

                            DestinationName = fileId + $"-x{size}" + fileExtension,

                            FilePath = filePathResize,

                            Created = DateTimeOffset.Now,

                            Size = fileResize.Length,

                            SizeType = size.ToSizeType(),

                            ContentType = file.ContentType
                        });
                    }

                    await _context.SystemBinaries.AddRangeAsync(files, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    var filePathExtension = filePath + fileExtension;

                    await _fileServerService.UploadFileAsync(filePathExtension, buffer);

                    var binary = new SystemBinary
                    {
                        ParentId = request.ParentId,

                        SourceName = file.FileName,

                        DestinationName = file.FileName,

                        FilePath = filePathExtension,

                        Created = DateTimeOffset.Now,

                        Size = file.Length,

                        ContentType = file.ContentType
                    };

                    var entity = await _context.SystemBinaries.AddAsync(binary, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return fileId;
            }

            private static string GetPhysicalPath(string fileName)
            {
                var fileExtension = Path.GetExtension(fileName)?.Replace(".", string.Empty).ToLower();
                return $"{DirectoryExtensions.GenerateDirectory()}\\{fileExtension}";
            }
        }
    }
}