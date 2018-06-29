using System;
using System.Runtime.InteropServices;

namespace FileStreamTest
{
    class FileEnumLearn
    {
        //指定操作系统打开文件的方式
        public enum FileMode
        {
            CreateNew = 1,          // 指定操作系统创建新文件，，并找到文件开头，如果文件存在则引发异常                                     // 配合  FileAccess.Write/FileAccess.ReadWrite 权限 
            Create = 2,             // 指定操作系统创建新文件，如果文件已存在则覆盖之，并找到文件开头（实际上是使用了CreateNew 和 Truncate）  // 配合  FileAccess.Write/FileAccess.ReadWrite 权限 
            Open = 3,               // 指定操作系统打开现有文件，若文件存在，则找到文件并找到文件开头,如果文件不存在则抛出异常。               // 配合  任何 权限 
            OpenOrCreate = 4,       // 指定操作系统打开文件，若文件存在，则找到文件并找到文件开头,如果文件不存在则创建之。                    //  配合  任何 权限 
            Truncate = 5,           // 指定操作系统打开现有文件，如果文件已存在则清空，如果文件不存在则抛出异常。                            // 配合  FileAccess.Write/FileAccess.ReadWrite 权限 
            Append = 6,             // 指定操作系统打开文件,若文件存在，则找到文件并找到文件结尾，或者创建一个新文件。                       // 配合  FileAccess.Write 权限 
        }

        //对于读、 写或读/写访问的文件中定义的常数
        [Flags][ComVisible(true)][Serializable]
        public enum FileAccess                              // 可以简单理解为 本进程 对该文件的权限
        {
            Read = 1,                                       // 读取权限
            Write = 2,                                      // 编写权限
            ReadWrite = Write | Read, // 0x00000003         // 读写权限
        }

        // 其他FileStream对象/其他进程 对 相同文件 的访问权限。
        [Flags]
        [ComVisible(true)]
        [Serializable]
        public enum FileShare                               // 可以简单理解为 本进程 已打开当前文件，其他进程对 当前文件 的访问权限
        {
            None = 0,                                       // 拒绝共享当前文件。在关闭文件之前， 通过此进程或另一个进程 ，任何请求打开当前文件都会失败。
            Read = 1,                                       // 当前文件打开时，其他进程只能读取
            Write = 2,                                      // 当前文件打开时，其他进程只能写入
            ReadWrite = Write | Read, // 0x00000003         // 当前文件打开时，其他进程可读可写
            Delete = 4,                                     // 当前文件打开时，其他进程可以删除该文件
            Inheritable = 16, // 0x00000010                 // 在源代码内部   FileShare fileShare = FileShare.Inheritable & ~FileShare.Inheritable ，结果为0，即 None = 0；所以没有意义 TODO 以后有需要了再看
        }
        //表示用于创建FileStream对象的高级选项
        [Flags]
        [ComVisible(true)]
        [Serializable]
        public enum FileOptions
        {
            None = 0,                                           // 不添加任何高级选项
            WriteThrough = -2147483648, // -0x80000000          // TODO 暂时不是很清楚
            Asynchronous = 1073741824, // 0x40000000            // 表示可以异步读写文件
            RandomAccess = 268435456, // 0x10000000             // TODO 暂时不是很清楚
            DeleteOnClose = 67108864, // 0x04000000             // 当关闭文件后，自动销毁文件
            SequentialScan = 134217728, // 0x08000000           // TODO 暂时不是很清楚
            Encrypted = 16384, // 0x00004000                    // TODO 暂时不是很清楚
        }


        //定义要创建访问和审核规则时使用的访问权限
        [Flags]
        public enum FileSystemRights                            // todo 全部弄完再看
        {
            ReadData = 1,
            ListDirectory = ReadData, // 0x00000001
            WriteData = 2,
            CreateFiles = WriteData, // 0x00000002
            AppendData = 4,
            CreateDirectories = AppendData, // 0x00000004
            ReadExtendedAttributes = 8,
            WriteExtendedAttributes = 16, // 0x00000010
            ExecuteFile = 32, // 0x00000020
            Traverse = ExecuteFile, // 0x00000020
            DeleteSubdirectoriesAndFiles = 64, // 0x00000040
            ReadAttributes = 128, // 0x00000080
            WriteAttributes = 256, // 0x00000100
            Delete = 65536, // 0x00010000
            ReadPermissions = 131072, // 0x00020000
            ChangePermissions = 262144, // 0x00040000
            TakeOwnership = 524288, // 0x00080000
            Synchronize = 1048576, // 0x00100000
            FullControl = Synchronize | TakeOwnership | ChangePermissions | ReadPermissions |
                          Delete | WriteAttributes | ReadAttributes | DeleteSubdirectoriesAndFiles |
                          Traverse | WriteExtendedAttributes | ReadExtendedAttributes | CreateDirectories |
                          CreateFiles | ListDirectory, // 0x001F01FF
            Read = ReadPermissions | ReadAttributes | ReadExtendedAttributes | ListDirectory, // 0x00020089
            ReadAndExecute = Read | Traverse, // 0x000200A9
            Write = WriteAttributes | WriteExtendedAttributes | CreateDirectories | CreateFiles, // 0x00000116
            Modify = Write | ReadAndExecute | Delete, // 0x000301BF
        }

       
    }
}
