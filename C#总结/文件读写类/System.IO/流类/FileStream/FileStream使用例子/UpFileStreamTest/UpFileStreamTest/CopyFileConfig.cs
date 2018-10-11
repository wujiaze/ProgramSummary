using System.IO;
namespace UpFileStreamTest
{
    class CopyFileConfig : IFileConfig
    {
        public bool IsAsync { get; set; }

        public string OriginalFileUrl { get; set; }

        public string DestinationFileUrl { get; set; }

        public FileStream OriginalFileStream { get; set; }

        public byte[] OriginalFileBytes { get; set; }

        public CopyFileConfig()
        {
        }

        public CopyFileConfig(bool isAsync, string originalFileUrl, string destinationFileUrl)
        {
            IsAsync = isAsync;
            OriginalFileUrl = originalFileUrl;
            DestinationFileUrl = destinationFileUrl;
            OriginalFileStream = null;
            OriginalFileBytes = null;
        }
    }
}
