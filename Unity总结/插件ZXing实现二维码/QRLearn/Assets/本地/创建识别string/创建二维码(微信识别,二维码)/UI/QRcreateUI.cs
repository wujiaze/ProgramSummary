using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class QRcreateUI : MonoBehaviour
{
    public Texture2D Icon;
    private RawImage _qrCodeStr1;
    private RawImage _qrCodeStr2;
    private RawImage _qrCodeStr3;
    private void Awake()
    {
        _qrCodeStr1 = GameObject.Find("RimgStr1").GetComponent<RawImage>();
        _qrCodeStr2 = GameObject.Find("RimgStr2").GetComponent<RawImage>();
        _qrCodeStr3 = GameObject.Find("RimgStr3").GetComponent<RawImage>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _qrCodeStr1.texture = QRTool.GeneQRwithString1("方法一的二维码", 300, 300); // 显示String
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _qrCodeStr2.texture = QRTool.GeneQRwithString2("方法二的二维码", 300, 300, Icon);//显示String，并且添加小图标
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // 写入文件，并且 跳转到网址
            string path = @"D:\Desktop\22.jpg";
            QRTool.WriteToFile(path, QRTool.TexStyle.JPG, @"https://www.baidu.com/", 300, 300, Icon);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // 将bytes[] 数组转换到 Texture2D
            byte[] bytes = QRTool.TexConvertBytes(QRTool.GeneQRwithString2(@"https://www.baidu.com/", 300, 300, Icon), QRTool.TexStyle.JPG);
            _qrCodeStr3.texture = QRTool.BytesConvertTex2D(bytes, 300, 300);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 上传bytes到服务器 todo
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // 接受服务器数据 todo
            //QRTool.SendQrToServer(@"D:\编程软件安装包\系统镜像文件\OS\OS X 10.11.1(15B42).cdr");
            stopwatch.Stop();
            print(stopwatch.ElapsedMilliseconds);
        }
    }
}


