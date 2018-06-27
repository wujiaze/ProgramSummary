using System.IO;

namespace FileStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 构造函数 */

            // FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy)
            // 在内部使用了
            //  FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy)
            // 
            // Init(string path, FileMode mode, FileAccess access, int rights, bool useRights, FileShare share, int bufferSize, FileOptions options, Win32Native.SECURITY_ATTRIBUTES secAttrs, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
            // Init(path, mode, (FileAccess) 0, (int) rights, true, share, bufferSize, options, secAttrs, Path.GetFileName(path), false, false, false);


            // 构造函数   FileStream(string path, FileMode mode)
            // 在内部使用了  FileStream(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)

            // 构造函数   FileStream(string path, FileMode mode, FileAccess access)
            // 在内部使用了 FileStream(path, mode, access, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)

            // 构造函数   FileStream(string path, FileMode mode, FileAccess access, FileShare share)
            // 在内部使用了 FileStream(path, mode, access, share, 4096, FileOptions.None, Path.GetFileName(path), false)

            // 构造函数   FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
            // 在内部使用了   FileStream(path, mode, access, share, bufferSize, FileOptions.None, Path.GetFileName(path), false)

            // 构造函数   FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
            // 在内部使用了  FileStream(path, mode, access, share, bufferSize, options, Path.GetFileName(path), false)

            // 构造函数   FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
            // 在内部使用了  FileStream(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None, Path.GetFileName(path), false)



            //FileStream stream =new FileStream();

        }
    }
}
