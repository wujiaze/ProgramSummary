using System.IO;
using static System.Console;

namespace DriveInfoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
            {
                if (driveInfo.IsReady)
                {
                    WriteLine($"驱动器名字:{driveInfo.Name}");
                    WriteLine($"文件系统的类型名称:{driveInfo.DriveFormat}");
                    WriteLine($"驱动器类型:{driveInfo.DriveType}");
                    WriteLine($"驱动器根目录:{driveInfo.RootDirectory}");
                    WriteLine($"驱动器的卷标:{driveInfo.VolumeLabel}");
                    WriteLine($"驱动器的总的空闲总量:{driveInfo.TotalFreeSpace}");
                    WriteLine($"驱动器上的可用空闲总量:{driveInfo.AvailableFreeSpace}"); // 即不同用户有不同的空间大小
                    WriteLine($"驱动器的总量:{driveInfo.TotalSize}");
                    WriteLine();
                }
            }
            ReadLine();
        }
    }
}
