using Microsoft.Extensions.Configuration;
using Minio;
using System.IO;
using System.Threading.Tasks;

namespace Package.FileServer
{
    public class FileServerService : IFileServerService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;

        public FileServerService(IConfiguration configuration)
        {
            var endpoint = configuration["Minio:Endpoint"];
            var accessKey = configuration["Minio:AccessKey"];
            var secretKey = configuration["Minio:SecretKey"];
            _bucketName = configuration["Minio:Bucket"].ToLower();

            _minioClient = new MinioClient(endpoint, accessKey, secretKey);

            if (!_minioClient.BucketExistsAsync(_bucketName).GetAwaiter().GetResult())
                _minioClient.MakeBucketAsync(_bucketName).GetAwaiter().GetResult();
        }

        public async Task UploadFileAsync(string filePath, byte[] fileBytes)
        {
            await using var fileStream = new MemoryStream();
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Seek(0, SeekOrigin.Begin);
            await _minioClient.PutObjectAsync(_bucketName, NormalFileName(filePath), fileStream, fileStream.Length, GetContentType(filePath));
        }

        public async Task<Stream> DownloadFileStreamAsync(string fileName)
        {
            var stream = new MemoryStream();
            await _minioClient.GetObjectAsync(_bucketName, NormalFileName(fileName), cb => cb.CopyTo(stream));
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public async Task<byte[]> DownloadFileByteAsync(string fileName)
        {
            await using var stream = new MemoryStream();
            await _minioClient.GetObjectAsync(_bucketName, NormalFileName(fileName), cb => cb.CopyTo(stream));
            return stream.ToArray();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            await _minioClient.RemoveObjectAsync(_bucketName, NormalFileName(fileName));
        }

        private static string NormalFileName(string fileName)
        {
            while (fileName.Contains("\\"))
                fileName = fileName.Replace("\\", "/");
            return fileName;
        }

        private static string GetContentType(string fileName)
        {
            var fileType = Path.GetExtension(fileName);
            return fileType switch
            {
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".xls" => "application/vnd.ms-excel",
                ".pdf" => "application/pdf",
                ".zip" => "application/zip",
                _ => "application/octet-stream",
            };
        }
    }
}