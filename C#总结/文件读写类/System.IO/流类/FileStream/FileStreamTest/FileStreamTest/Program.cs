using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace FileStreamTest
{
   
    class Program
    {
        // 首先介绍四种文件枚举  详见 FileEnumLearn
        static void Main(string[] args)
        {
            /* 构造函数 */
            // 大致分为三种

            // 第一种
            // 1.1  FileStream(string path, FileMode mode)                                                                           // (path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)
            // 1.2  FileStream(string path, FileMode mode, FileAccess access)                                                        // (path, mode, access, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false)
            // 1.3  FileStream(string path, FileMode mode, FileAccess access, FileShare share)                                       // (path, mode, access, share, 4096, FileOptions.None, Path.GetFileName(path), false)
            // 1.4  FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)                       // (path, mode, access, share, bufferSize, FileOptions.None, Path.GetFileName(path), false)
            // 1.5  FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)        // (path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None, Path.GetFileName(path), false)
            // 1.6  FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)  // (path, mode, access, share, bufferSize, options, Path.GetFileName(path), false)

            // 内部采用
            // 步骤1、FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy)
            // 其中一些参数：                                                                                                  msgPath：Path.GetFileName(path)  bFromProxy: fasle
            // 步骤2、Init(string path, FileMode mode, FileAccess access, int rights, bool useRights, FileShare share, int bufferSize, FileOptions options, Win32Native.SECURITY_ATTRIBUTES secAttrs, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost) // TODO 注意 checkHost 这个参数在内部根本没有使用，所以false 和true 都没有 关系
            // 其中一些参数：                                                 rights=0     useRights=false                                                   secAttrs = FileStream.GetSecAttrs(share);                                useLongPath: fasle   checkHost: fasle 
            // 总结可知： 主要还是 FileMode FileAccess FileOptions 这三个枚举的作用


            // 第二种
            // 2.1  FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
            // 2.2  FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)  // FileSystemRights 的作用：类似 FileAccess，但权限更多
            // 内部采用
            // Init(string path, FileMode mode, FileAccess access, int rights, bool useRights, FileShare share, int bufferSize, FileOptions options, Win32Native.SECURITY_ATTRIBUTES secAttrs, string msgPath, bool bFromProxy, bool useLongPath, bool checkHost)
            // 其中一些参数：                                      (int)rights,  useRights=true                                                  secAttrs = FileStream.GetSecAttrs(share, fileSecurity, out pinningHandle)     useLongPath: fasle   checkHost: fasle


            // 第三种
            // 3.1  FileStream(SafeFileHandle handle, FileAccess access)
            // 3.2  FileStream(SafeFileHandle handle, FileAccess access, int bufferSize)   // TODO  SafeFileHandle 的作用
            // 内部采用
            // 3.3  FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)



            /* 属性
            *  CanRead
            *  CanSeek
            *  CanTimeout
            *  CanWrite
            *  IsAsync
            *  Length
            *  Name
            *  Position
            *  ReadTimeout
            *  SafeFileHandle
            *  WriteTimeout
            */

            /* 方法
             * Write
             * WriteAsync           // 一般实际的用法，看最下面
             *
             * Read                    
             * ReadAsync
             * ReadByte             //  流是基于 字节数组
             *
             * BeginRead
             * BeginWrite
             * EndRead
             * EndWrite
             *
             * CopyTo
             * CopyToAsync
             *
             * Seek
             *
             * SetLength
             *  
             * Flush        一般用于写入
             * FlushAsync
             *
             * Lock
             * UnLock
             *
             * GetAccessControl         
             * SetAccessControl         
             *
             */
            #region 采用 构造函数 1.6 测试  
            Console.WriteLine("---------------------属性---------------------");
            string filePath = "D:\\Desktop\\MyFile1.txt"; 
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.None))   
            { 
                // 只读属性
                Console.WriteLine("CanRead: " + fileStream.CanRead);                //  在本类构造函数中 CanRead 由 FileAccess 确定  
                Console.WriteLine("CanWrite: " + fileStream.CanWrite);              //  在本类构造函数中 CanWrite 由 FileAccess 确定
                Console.WriteLine("CanTimeout：" + fileStream.CanTimeout);          //  在本类构造函数中 CanTimeout 内部设计是： fasle
                Console.WriteLine("CanSeek: " + fileStream.CanSeek);                //  在本类构造函数中 CanSeek    内部设计是： true 
                Console.WriteLine("Length: " + fileStream.Length);                  //  流的长度
                Console.WriteLine("IsAsync: " + fileStream.IsAsync);                //  在本类构造函数中  IsAsync 由  FileOptions.Asynchronous 确定
                Console.WriteLine("Name: " + fileStream.Name);                      //  在本类构造函数中 Name 为 filePath 绝对路径 或 Debug/bin 相对路径
                Console.WriteLine("SafeFileHandle: " + fileStream.SafeFileHandle);  //  返回 SafeFileHandle 对象
                // 可读可写属性
                Console.WriteLine("Position: " + fileStream.Position);              //  set方法:内部封装了 Seek 方法   
                //Console.WriteLine("ReadTimeout: " + fileStream.ReadTimeout);        // 在本类构造函数中，内部设计是：直接抛出异常
                //Console.WriteLine("WriteTimeout: " + fileStream.WriteTimeout);      // 在本类构造函数中，内部设计是：直接抛出异常
            }

            // 方法  Write
            Console.WriteLine("---------------------Write---------------------");
            string filePath2 = "D:\\Desktop\\MyFile2.txt";
            using (FileStream fileStream = new FileStream(filePath2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.None))
            {
                string str = "ABC 你好！";
                byte[] bytes = Encoding.UTF8.GetBytes(str);                                     // 写入的编码格式，根据 Encoding.Default.GetBytes 这个方法确定
                fileStream.Write(bytes,0,bytes.Length);
                Console.WriteLine(fileStream.Length);
            }
            // 方法 WriteAsync
            Console.WriteLine("---------------------WriteAsync---------------------");
            string filePath3 = "D:\\Desktop\\MyFile3.txt";
            Task task1 = WriteToFileAsync(filePath3, "ssss");               // 一般实际的用法,见最下面
            task1.Wait();
            using (FileStream fileStream = new FileStream(filePath3, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 1024, FileOptions.Asynchronous))
            {
                
                string str = "96tyt 你好！";
                byte[] bytes = Encoding.Default.GetBytes(str);
                Task task = fileStream.WriteAsync(bytes, 0, bytes.Length);                  // 异步操作时，FileOptions 可以为 Asynchronous，也可以是 None ，这两者都不会阻塞当前线程
                task.Wait();                                                                // 但是，None 内部采用同步方法  Asynchronous 内部采用异步方法，所以建议采用 Asynchronous
            }

            // 结合  StreamWriter 
            Console.WriteLine("---------------------StreamWriter---------------------");
            string filePath4 = "D:\\Desktop\\MyFile4.txt";
            using (FileStream fileStream = new FileStream(filePath4, FileMode.Append, FileAccess.Write, FileShare.None, 1024, FileOptions.Asynchronous)) // FileMode.Append 表示在定位在流的末尾， StreamWriter 类只能在规定的地方增加内容，合起来就是在末尾增加内容
            {                                                                                                                                            //  FileMode.OpenOrCreate 这种就是定位在流的开头，StreamWriter 类也就从头开始写入，就形成覆盖的感觉，并且是一点一点的覆盖，这两种是本质上没有区别
                using (StreamWriter write = new StreamWriter(fileStream, Encoding.UTF8, 1024, true))      // 由于 FileStream 自带的写入方法过少，一般采用 StreamWriter 类来写入
                {                                                                                         // 写入的编码格式，根据 StreamWriter 构造函数确定
                    write.WriteAsync("poi");
                    write.Write("[][");
                }
            }
            // Read
            Console.WriteLine("---------------------Read---------------------");
            using (FileStream fileStream = new FileStream(filePath2, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.None))
            {                                                                                           // 测试说明：以 Encoding 方法写入的内容，需要以 Encoding 方法来读取
                byte[] readBuffer = new byte[200];                        
                int length = fileStream.Read(readBuffer, offset:0, count:100);                                       // 测试说明：当读取接受的 数组 大于流的长度时，只读取流本身的长度
                Console.WriteLine("读取的长度： " + length); 
                string str = Encoding.UTF8.GetString(readBuffer);
                Console.WriteLine(str);
            }
            // ReadAsync
            Console.WriteLine("--------------------- ReadAsync ---------------------");
            using (FileStream fileStream = new FileStream(filePath3, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.Asynchronous))
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                byte[] readBuffer = new byte[fileStream.Length];
                Task<int> task = fileStream.ReadAsync(readBuffer, 0, readBuffer.Length, token);    //  测试带令牌的异步方法（对比 WriteAsync）
                //source.Cancel();                                                                 //  可以取消异步方法。   当 ReadAsync 方法用时比较长，就可以测试出来
                Console.WriteLine("读取的长度：" + task.Result);
                string str = Encoding.Default.GetString(readBuffer);
                Console.WriteLine(str);
            }
            // StreamReader
            Console.WriteLine("---------------StreamReader------------------");
            using (FileStream fileStream = new FileStream(filePath4, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.None))
            {                                                                                            // 测试说明：以 StreamWriter/Encoding 方法写入的内容，都可以 StreamReader 方法来读取，所以推荐 StreamReader 来读取
                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {
                    char[] readBuffer = new char[100];
                    int length = reader.Read(readBuffer, 0, 100);                                       // 测试说明：当读取接受的 数组长度 大于 流的长度时，只读取流本身的长度
                    Console.WriteLine("读取的长度： " + length);
                    Console.WriteLine(reader.CurrentEncoding);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            // ReadByte 
            Console.WriteLine("---------------ReadByte------------------");
            using (FileStream fileStream = new FileStream(filePath2, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.SequentialScan))
            {
                char c1 = (char) fileStream.ReadByte();
                Console.WriteLine(c1);
            }
            // CopyTo /  CopyToAsync
            Console.WriteLine("---------------CopyTo  -  CopyToAsync-----------------");
            using (FileStream fileStream = new FileStream(filePath2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.Asynchronous))
            {
                Console.WriteLine("Position: " + fileStream.Position);

                Stream stream = new MemoryStream();
                fileStream.CopyTo(stream);                                           
                Console.WriteLine("复制的长度： " + stream.Length);                       

                Console.WriteLine("Position: " + fileStream.Position);
                fileStream.Position = 0;

                Stream stream2 = new MemoryStream();
                Task task = fileStream.CopyToAsync(stream2, 10);            // 缓冲区大小：内部是 初始化一个 缓冲区大小的 byte数组 ，然后循环读取 fileStream 的内容，并写入 目标流中，直到读完 fileStream的内容
                task.Wait();                                                // 所以，缓冲区的大小 太大或太小 都不好，默认是 81920
                Console.WriteLine("复制的长度： " + stream2.Length);
            }
            // Seek
            Console.WriteLine("---------------Seek------------------");
            using (FileStream fileStream = new FileStream(filePath2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.None))
            {
                fileStream.Seek(4, SeekOrigin.Current);                     // 设置流的位置
                Console.WriteLine("当前位置： " + fileStream.Position);
                fileStream.Position = 0;                                    // Position 也可以设置位置，内部是 Seek(value, SeekOrigin.Begin);
                Console.WriteLine("当前位置： " + fileStream.Position);
            }

            // SetLength
            Console.WriteLine("---------------SetLength------------------");
            using (FileStream fileStream = new FileStream(filePath2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.None))
            {
                Console.WriteLine(fileStream.Position);
                Console.WriteLine(fileStream.Length);
                fileStream.ReadByte();
                fileStream.SetLength(5);                                    // 测试表明 ，当设置的长度 小于 原长度时，则从头截取 设置的长度
                Console.WriteLine(fileStream.Position);
                Console.WriteLine(fileStream.Length);
                fileStream.SetLength(10);                                   // 测试表明 ，当设置的长度 大于 原长度时，则使用 '\0' 来代替空的字符串，
                for (int i = 0; i < 9; i++)                                 // '\0' 表示字符串已结束，'\0'后面即使内容，也不会去读取
                {
                    char a = (char)fileStream.ReadByte();
                    Console.WriteLine(a.Equals('\0'));
                }
                Console.WriteLine(fileStream.Position);
                Console.WriteLine(fileStream.Length);
            }

            // Flush
            Console.WriteLine("-------------- Flush ------------------");
            string filePath6 = "D:\\Desktop\\MyFile6.txt";
            using (FileStream fileStream = new FileStream(filePath6, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 10, FileOptions.Asynchronous))
            {                                                                               
                Console.WriteLine(fileStream.Length);
                string str = "123456789abcsdfghj";
                byte[] bytes = Encoding.Default.GetBytes(str);      // 默认缓冲区大小是4096 ,这里改为 10 进行测试，测试结果表明：当写入的内容长度大于等于 10 个字节时，内部就会调用 Flush(false)
                Console.WriteLine(bytes.Length);                    // 所以，缓冲区就采用默认的 4096 就好了，这样的话写入的颗粒度比较合适
                fileStream.Write(bytes, 0, bytes.Length);           // 不过要是有需要，也可以手动调用 Flush(false)
                                                                    // 对于UTF8这类多个字节代表一个内容的编码方式，使用Flush(true)，可能会导致读写错误，所以推荐用 Flush()（内部是Flush(false) ）
                byte[] readBuffer = new byte[15];
                for (int i = 0; i < readBuffer.Length; i++)
                {
                    Console.WriteLine($"{i} : " + readBuffer[i]);
                }
                Console.WriteLine("-------");
                fileStream.Position = 0;
                fileStream.Read(readBuffer, 0, 15);                 // 对于读取，缓存大小似乎没什么作用，也意味着 Flush 没什么作用。 总结可得：Flush一般用于写入，并且可以在必要的地方手动调用
                for (int i = 0; i < readBuffer.Length; i++)
                {
                    Console.WriteLine($"{i} : " +readBuffer[i]);
                }
                Thread.Sleep(3000);
                Task task = fileStream.FlushAsync();                // 普通的异步方法
                task.Wait();
            }

            // Lock/UnLock
            Console.WriteLine("-------------- -Lock / UnLock ------------------");
            string filePath5 = "D:\\Desktop\\MyFile5.txt";
            using (FileStream fileStream = new FileStream(filePath5, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, FileOptions.None))
            {
                fileStream.Lock(1, fileStream.Length);    // 测试可知：FileAccess.ReadWrite 和 FileShare.ReadWrite 配合，别的进程也可以读写该文件
                Thread.Sleep(3000);                       // 但是，使用了  Lock(long position, long length) 方法之后，就锁住了 （position，length）这一段 ,其他进程不能读取整个文件，但是可以在未加锁的部分写入
                fileStream.Unlock(1, fileStream.Length);
            }


            #endregion



            #region 采用 2.2 测试   同时 测试 多线程 方法
            // 下面的多线程的方法 不适用 using 语句，同时自身也快被淘汰了，用 Async来代替更好
            /* 
             * BeginRead
             * BeginWrite
             * EndRead
             * EndWrite
             */
            // FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
            // 内部也是 Init 方法 和 构造函数第一类 是类似的
            // 
            /* 主要的几个枚举
             * FileSystemRights.Write           // 写    权限
             * FileSystemRights.Read            // 读    权限
             * FileSystemRights.Modify          // 读 写 运行 删除 权限
             * FileSystemRights.FullControl     // 全部权限
             */
            Console.WriteLine("--------------  2.1 测试 ------------------");
            Console.WriteLine("--------------  BeginWrite / EndWrite  ------------------");
            string pathSecond = "D:\\Desktop\\FileSecond.txt";
            FileStream writeStream = new FileStream(pathSecond, FileMode.Create, FileSystemRights.Write, FileShare.Read, 4096, FileOptions.None);// BeginWrite 这类方法也不需要一定要 异步选项
            string writeStr = "Hello你好！";
            byte[] writeByteArr = Encoding.Default.GetBytes(writeStr);
            writeStream.BeginWrite(writeByteArr, 0, writeByteArr.Length, BeginWriteCallBack, writeStream);              // 新开线程执行任务
            Console.WriteLine("线程方法： " + Thread.CurrentThread.ManagedThreadId);


            Console.WriteLine("--------------  BeginRead / EndRead  ------------------");
            string pathSecond2 = "D:\\Desktop\\MyFile2.txt";
            FileStream readStream = new FileStream(pathSecond2, FileMode.Open, FileSystemRights.Read, FileShare.Read, 4096, FileOptions.Asynchronous);// 但是最好还是使用异步选项
            byte[] readBytes = new byte[readStream.Length];
            MyClass myClass = new MyClass() { Bytes = readBytes, FileStream = readStream };
            readStream.BeginRead(readBytes, 0, readBytes.Length, BeginReadCallBack, myClass);


            Console.WriteLine("--------------  2.2 测试 ------------------");
            string pathSecond3 = "D:\\Desktop\\FileSecond2.txt";
            //FileSecurity sec = new FileSecurity(pathSecond,AccessControlSections.All);        //TODO 以后再看，目前无法解答
            //using (FileStream fileStream = new FileStream(pathSecond2, FileMode.Create, FileSystemRights.Write, FileShare.Read, 4096, FileOptions.Asynchronous, sec))
            //{

            //}
            #endregion


            #region 采用 3.3 构造函数 进行测试

            // TODO
            /* GetAccessControl         // TODO 以后再看   ACL技术
             * SetAccessControl         // TODO 以后再看
             */
            // FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)   TODO 以后再看，目前无法解答
            //string pathThird = "D:\\Desktop\\FileThird.txt";
            //SafeHandle handle = new SafeFileHandle(,);
            //using (FileStream fileStream = new FileStream(SafeHandle))
            //{

            //}
            #endregion

            Console.Read();
        }
        private static void BeginWriteCallBack(IAsyncResult ar)
        {
            FileStream stream = ar.AsyncState as FileStream;
            if (stream == null)return;
            stream.EndWrite(ar);
            Console.WriteLine("回调线程: "+Thread.CurrentThread.ManagedThreadId);
            stream.Close();
        }
        private static void BeginReadCallBack(IAsyncResult ar)
        {
            MyClass myClass = ar.AsyncState as MyClass;
            if (myClass == null) return;
            myClass.FileStream.EndRead(ar);
            Console.WriteLine("回调：" + Encoding.Default.GetString(myClass.Bytes));
            myClass.FileStream.Close();
        }

        class MyClass
        {
            public byte[] Bytes { get; set; }
            public FileStream FileStream { get; set; }
        }

        private static async Task WriteToFileAsync(string filePath,string value)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 1024, FileOptions.Asynchronous))
            {
                byte[] bytes = Encoding.Default.GetBytes(value);
                await fileStream.WriteAsync(bytes, 0, bytes.Length);                       
            }
        }
    }
}
