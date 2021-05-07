using System.IO;
using System.Threading.Tasks;

namespace Package.FileServer
{
    public interface IFileServerService
    {
        Task UploadFileAsync(string filePath, byte[] fileBytes);

        Task<Stream> DownloadFileStreamAsync(string fileName);

        Task<byte[]> DownloadFileByteAsync(string fileName);

        Task DeleteFileAsync(string fileName);
    }
}