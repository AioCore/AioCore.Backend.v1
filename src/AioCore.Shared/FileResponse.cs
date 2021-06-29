using MimeTypes;

namespace AioCore.Shared
{
    public class FileResponse
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string ContentType => MimeTypeMap.GetMimeType(FileName);
    }
}
