﻿using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Package.Elasticsearch;
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

        public Guid? ParentId { get; set; }

        internal class Handler : IRequestHandler<CreateBinaryCommand, string>
        {
            private readonly int[] _imageSizes = { 25, 50, 100, 200, 400, 800, 1600 };

            private readonly IFileServerService _fileServerService;
            private readonly IDynamicBinaryRepository _dynamicBinaryRepository;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                IFileServerService fileServerService,
                IDynamicBinaryRepository dynamicBinaryRepository,
                IElasticsearchService elasticsearchService)
            {
                _fileServerService = fileServerService ?? throw new ArgumentNullException(nameof(fileServerService));
                _dynamicBinaryRepository = dynamicBinaryRepository ?? throw new ArgumentNullException(nameof(dynamicBinaryRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<string> Handle(CreateBinaryCommand request, CancellationToken cancellationToken)
            {
                var file = request.File;

                var fileId = file.FileName.CreateMd5().ToLower();

                var fullPath = GetPhysicalPath(file.FileName);

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

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

                            SourceName = file.FileName,

                            DestinationName = fileId + $"-x{size}" + fileExtension,

                            FilePath = filePathResize,

                            Created = DateTimeOffset.Now,

                            Size = fileResize.Length,

                            SizeType = size.ToSizeType(),

                            ContentType = file.ContentType
                        });
                    }

                    var entities = _dynamicBinaryRepository.AddRange(files);

                    await _dynamicBinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    await _elasticsearchService.IndexManyAsync(entities);
                }
                else
                {
                    var filePathExtension = filePath + fileExtension;

                    await _fileServerService.UploadFileAsync(filePathExtension, buffer);

                    var binary = new DynamicBinary
                    {
                        ParentId = request.ParentId,

                        SourceName = file.FileName,

                        DestinationName = file.FileName,

                        FilePath = filePathExtension,

                        Created = DateTimeOffset.Now,

                        Size = file.Length,

                        ContentType = file.ContentType
                    };

                    var entity = _dynamicBinaryRepository.Add(binary);

                    await _dynamicBinaryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    await _elasticsearchService.IndexAsync(entity);
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