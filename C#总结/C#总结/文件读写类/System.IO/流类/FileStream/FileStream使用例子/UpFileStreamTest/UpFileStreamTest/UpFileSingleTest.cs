using System;
using System.IO;
using System.Text;

namespace UpFileStreamTest
{
    class UpFileSingleTest
    {
        public const int BUFFER_COUNT = 1024;
        public static readonly object _lockObj = new object();

        /// <summary>
        /// 根据配置需求，创建文件
        /// </summary>
        /// <param name="config"></param>
        public static void Create(IFileConfig config)
        {
            lock (_lockObj)
            {
                CreateFileConfig createFileConfig = config as CreateFileConfig;
                if (createFileConfig == null) return;
                if (createFileConfig.CreateUrl == null) return;
                char[] insertContent = "HelloWorld".ToCharArray();
                byte[] byteArrayContent = Encoding.Default.GetBytes(insertContent);
                FileStream stream = createFileConfig.IsAsync
                    ? new FileStream(createFileConfig.CreateUrl, FileMode.Create, FileAccess.ReadWrite, FileShare.Read,
                        4096, true)
                    : new FileStream(createFileConfig.CreateUrl, FileMode.Create);
                using (stream)
                {
                    if (stream.CanWrite)
                    {
                        if (!stream.IsAsync)
                        {
                            stream.Write(byteArrayContent, 0, byteArrayContent.Length);
                            Console.WriteLine($"同步创建文件地址{stream.Name}");
                        }
                        else
                        {
                            stream.BeginWrite(byteArrayContent, 0, byteArrayContent.Length, EndCreateFileCallBack, stream);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 根据配置需求，复制文件
        /// </summary>
        /// <param name="config"></param>
        public static void Copy(IFileConfig config)
        {
            lock (_lockObj)
            {
                CopyFileConfig copyFileConfig = config as CopyFileConfig;
                if (copyFileConfig == null) return;
                if (copyFileConfig.OriginalFileUrl == null || copyFileConfig.DestinationFileUrl == null) return;
                if (!File.Exists(copyFileConfig.OriginalFileUrl)) return;
                FileStream stream = copyFileConfig.IsAsync ?
                    new FileStream(copyFileConfig.OriginalFileUrl, FileMode.Open, FileAccess.ReadWrite, FileShare.Read,
                        4096, true)
                    : new FileStream(copyFileConfig.OriginalFileUrl, FileMode.Open, FileAccess.ReadWrite, FileShare.Read,
                        4096, false);
                byte[] originalFileBytes = new byte[stream.Length];
                using (stream)
                {
                    if (stream.CanRead)
                    {
                        if (stream.IsAsync)
                        {
                            // 异步
                            copyFileConfig.OriginalFileStream = stream;
                            copyFileConfig.OriginalFileBytes = originalFileBytes;
                            stream.BeginRead(originalFileBytes, 0, originalFileBytes.Length, EndReadFileCallBack, copyFileConfig);

                        }
                        else
                        {
                            // 同步
                            stream.Read(originalFileBytes, 0, originalFileBytes.Length);
                            using (FileStream copyStream = new FileStream(copyFileConfig.DestinationFileUrl, FileMode.Create))
                            {
                                copyStream.Write(originalFileBytes, 0, originalFileBytes.Length);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 复制大文件
        /// </summary>
        /// <param name="localFilePath">源文件</param>
        /// <param name="uploadFilePath">目标位置</param>
        /// <param name="speed">最大1024*2048</param>
        public static void CopyBigFile(string localFilePath, string uploadFilePath,int speed)
        {
            if(!File.Exists(localFilePath))return;
            FileInfo info = new FileInfo(localFilePath);
            long fileLength = info.Length;
            long perFileLength = fileLength / cutNum;
            long restLength = fileLength % cutNum;
            for (int i = 0; i <= cutNum; i++)
            {
                long startPostion = i * perFileLength;
                long currentCount = fileLength - startPostion < perFileLength
                    ? restLength
                    : perFileLength;
                UpLoadFileFromLocal(localFilePath, uploadFilePath, startPostion, (int)currentCount, speed);
            }
            Console.WriteLine("完成");
        }

        private static void UpLoadFileFromLocal(string localFilePath,string uploadFilePath, long startPosition,int currentCount,int speed)
        {
            if(currentCount==0)return;
            long tempReadCount = 0;
            int tempBuffer = BUFFER_COUNT* speed;
            byte[] bufferArray = new byte[tempBuffer];
            using (FileStream fileStream = new FileStream(localFilePath, FileMode.Open))
            {
                fileStream.Position = startPosition;
                while (tempReadCount < currentCount)
                {
                    long writeStartPosition = startPosition + tempReadCount;
                    if (tempBuffer + tempReadCount <= currentCount)
                    {
                        fileStream.Read(bufferArray, 0, tempBuffer);
                        WriteToServer(uploadFilePath, writeStartPosition, bufferArray);
                    }
                    else 
                    {
                        tempBuffer = currentCount - tempReadCount;
                        byte[] tempBytes = new byte[tempBuffer];
                        fileStream.Read(tempBytes, 0, tempBuffer);
                        WriteToServer(uploadFilePath, writeStartPosition, tempBytes);
                    }
                    tempReadCount += tempBuffer;
                }
            }
        }

        private static void WriteToServer(string filePath, long startPosition,byte[] bytesArr)
        {
            using (FileStream fileStream = new FileStream(filePath,FileMode.OpenOrCreate))
            {
                fileStream.Position = startPosition;
                fileStream.Write(bytesArr,0,bytesArr.Length);
            }
        }

        private static void EndCreateFileCallBack(IAsyncResult ar)
        {
            FileStream stream = ar.AsyncState as FileStream;
            stream.EndWrite(ar);
            Console.WriteLine($"异步创建文件地址{stream.Name}");
        }
        private static void EndReadFileCallBack(IAsyncResult ar)
        {
            CopyFileConfig config = ar.AsyncState as CopyFileConfig;
            if (config == null) return;
            config.OriginalFileStream.EndRead(ar);
            if (File.Exists(config.DestinationFileUrl))
            {
                File.Delete(config.DestinationFileUrl);
            }
            FileStream copyFileStream = new FileStream(config.DestinationFileUrl, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 4096, true);
            using (copyFileStream)
            {
                Console.WriteLine("异步复制原文件地址：{0}", config.OriginalFileStream.Name);
                Console.WriteLine("复制后的新文件地址：{0}", config.DestinationFileUrl);
                copyFileStream.BeginWrite(config.OriginalFileBytes, 0, config.OriginalFileBytes.Length,EndCreateFileCallBack, copyFileStream); 
            }
        }


       
    }
}
