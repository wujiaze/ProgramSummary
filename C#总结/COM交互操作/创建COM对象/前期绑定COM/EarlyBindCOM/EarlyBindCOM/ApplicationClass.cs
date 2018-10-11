using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace EarlyBindCOM
{
    [ComImport]                                         // 说明这个类已经在COM组件中定义好了
    [Guid("000209FF-0000-0000-C000-000000000046")]      // 通过反编译或其他方法获取Guid
    public class ApplicationClass
    {
        [DispId(373)]                                   // 方法的特性
        [MethodImpl(MethodImplOptions.InternalCall)]    // 方法的特性
        public virtual extern float PicasToPoints(float Picas);
    }
    [ComImport]
    [Guid("00020970-0000-0000-C000-000000000046")]
    [CoClass(typeof(ApplicationClass))]                 // 对应的实现类
    public interface Application
    {

    }
}
