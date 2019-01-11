using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathLearn : MonoBehaviour
{
    void Start()
    {
        /*
         *      WWW 类 支持的路径表示
         *
         *      特别注意:路径中的斜杠要统一，虽然有的时候可以有正有反
         *                                但是 1、有的格式下，路径会出错
         *                                     2、解析路径时，比较麻烦
         *                                 所以路径推荐统一
         */

        // 方法1：Windows反斜杠
        string path1 = @"D:\Desktop\编程学习总结\ProgramSummary\Unity总结\Unity_WWW和WWWForm类\WWWLearn\Assets\StreamingAssets\1.txt";
        StartCoroutine(PathTest(path1, "方法1"));
        // 方法2: 路径全部为 一般斜杠
        string path2 = @"D:/Desktop/编程学习总结/ProgramSummary/Unity总结/Unity_WWW和WWWForm类/WWWLearn/Assets/StreamingAssets/1.txt";
        StartCoroutine(PathTest(path2, "方法2"));
        // 方法3：file:// 开头 + 路径一般斜杠
        string path3 = @"file://" + path2;
        StartCoroutine(PathTest(path3, "方法3"));
        // 方法4：file:///  开头 + 路径一般斜杠
        string path4 = @"file:///" + path2;         // 注意点：Unity建议WWW类使用file时，使用三条斜杠 + 路径一般斜杠
        StartCoroutine(PathTest(path4, "方法4"));
        // 方法5：file:// 开头 + 路径反斜杠
        string path5 = @"file://" + path1;
        StartCoroutine(PathTest(path5, "方法5"));
        // 方法6：file:/// 开头 + 路径反斜杠
        string path6 = @"file:///" + path1;
        StartCoroutine(PathTest(path6, "方法6"));


        /*
        *      File 类 支持的路径表示
        *      特别注意:路径中的斜杠要统一，虽然这里正反都可以
        *                                但是 1、有的格式下，路径会出错
        *                                     2、解析路径时，比较麻烦
        *                                 所以路径推荐统一
        */
        // 方法1：Windows反斜杠
        string path7 = @"D:\Desktop\编程学习总结\ProgramSummary\Unity总结\Unity_WWW和WWWForm类\WWWLearn\Assets\StreamingAssets\1.txt";
        ResdFile(path7, "方法7");  // 一般C#中推荐
        // 方法2: 路径全部为 一般斜杠
        string path8 = @"D:/Desktop/编程学习总结/ProgramSummary/Unity总结/Unity_WWW和WWWForm类/WWWLearn/Assets/StreamingAssets/1.txt";
        ResdFile(path8, "方法8");  // Unity中推荐
    }


    private IEnumerator PathTest(string path, string str)
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone)
        {
            print(str + "      " + path + "    " + www.text);
        }
    }

    private void ResdFile(string path, string str)
    {
        print(path);
        string txt = File.ReadAllText(path);
        print(str + "      " + path + "    " + txt);
    }
}
