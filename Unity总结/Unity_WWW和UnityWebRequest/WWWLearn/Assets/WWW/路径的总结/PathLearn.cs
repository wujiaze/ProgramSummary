/*
 *   WWW 类支持的路径(文字、图片、音频、视频)
 *
 *      方法1：本地文件
 *             纯Windows反斜杠               支持的内容：  文字、图片、音频、视频
 *             file://  + 纯Windows反斜杠    支持的内容：  文字、图片、音频、视频
 *             file:/// + 纯Windows反斜杠    支持的内容：  文字、图片、音频、视频
 *             纯一般斜杠                    支持的内容：  文字、图片、音频、视频
 *             file:/// + 纯一般斜杠         支持的内容：  文字、图片、音频、视频      Unity推荐
 *             file://  + 纯一般斜杠         支持的内容：  文字、图片、音频、视频      自己推荐
 *      方法2：局域网模式
 *              file://  + 纯一般斜杠(//ip/xxx)         支持的内容： 文字、图片、音频            只能使用， 自己推荐
 *              视频:新版VideoPlayer支持的路径    1、纯Windows反斜杠
 *                                              2、file:/// + 纯一般斜杠             Unity推荐
 *      方法3：网络模式
 *              https:// + 纯一般斜杠        支持的内容：  文字、图片、音频、视频(新版VideoPlayer也支持)
 *
 *
 *
 *   File 类 支持的路径表示
 *
 *      方法1：本地文件
 *             纯Windows反斜杠             支持的内容：  文字、图片、音频、视频
 *             纯一般斜杠                  支持的内容：  文字、图片、音频、视频
 *      方法2：局域网模式
 *             纯Windows反斜杠             支持的内容：  文字、图片、音频、视频
 *             纯一般斜杠                  支持的内容：  文字、图片、音频、视频
 *      方法3：不支持网络
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class PathLearn : MonoBehaviour
{
    public List<Text> Texts;
    public List<RawImage> Imgs;
    public List<AudioSource> Sources;
    public List<VideoPlayer> Players;
    public List<GameObject> VideoImgs;
    void Start()
    {
        #region WWW类

        /* 本地文件 */
        //// 文字
        //string path1 = @"D:\Desktop\编程学习总结\ProgramSummary\Unity总结\Unity_WWW和WWWForm类\WWWLearn\Assets\StreamingAssets\1.txt";
        //string path2 = @"D:/Desktop/编程学习总结/ProgramSummary/Unity总结/Unity_WWW和WWWForm类/WWWLearn/Assets/StreamingAssets/1.txt";
        //string temp = null;
        //temp = path1;
        //StartCoroutine(LoadText(temp, "Windows反斜杠:文字", Texts[0]));
        //temp = @"file://" + path1;
        //StartCoroutine(LoadText(temp, "file://  Windows反斜杠:文字", Texts[1]));
        //temp = @"file:///" + path1;
        //StartCoroutine(LoadText(temp, "file:/// Windows反斜杠:文字", Texts[2]));
        //temp = path2;
        //StartCoroutine(LoadText(temp, "纯一般斜杠 :文字", Texts[3]));
        //temp = @"file://" + path2;
        //StartCoroutine(LoadText(temp, "file:// 纯一般斜杠 :文字", Texts[4]));
        //temp = @"file:///" + path2;                             
        //StartCoroutine(LoadText(temp, "file:/// 纯一般斜杠 :文字", Texts[5]));


        //// 图片
        //string path3 = @"D:\Desktop\常规项目\拍照魔墙\Texture\5.jpg";
        //string path4 = @"D:/Desktop/常规项目/拍照魔墙/Texture/5.jpg";
        //string temp2 = null;
        //temp2 = path3;
        //StartCoroutine(LoadTexture(temp2, "Windows反斜杠 :图片", Imgs[0]));
        //temp2 = @"file://" + path3;
        //StartCoroutine(LoadTexture(temp2, "file:// Windows反斜杠 :图片", Imgs[1]));
        //temp2 = @"file:///" + path3;
        //StartCoroutine(LoadTexture(temp2, "file:/// Windows反斜杠 :图片", Imgs[2]));
        //temp2 = path4;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[3]));
        //temp2 = @"file://" + path4;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[4]));
        //temp2 = @"file:///" + path4;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[5]));


        //// 音频
        //string path5 = @"D:\Desktop\常规项目\魔墙\05_上海站人才魔墙\资料\软件资料\六十佳录音\新建文件夹\薄岗.wav";
        ////string path6 = @"D:/Desktop/常规项目/魔墙/05_上海站人才魔墙/资料/软件资料/六十佳录音/新建文件夹/薄岗.wav";
        //string temp3 = null;
        //temp3 = path5;
        //StartCoroutine(LoadAudioClip(temp3, "Windows反斜杠:音频", Sources[0]));
        //temp3 = @"file://" + path5;
        //StartCoroutine(LoadAudioClip(temp3, "file:// Windows反斜杠 :音频", Sources[1]));
        //temp3 = @"file:///" + path5;
        //StartCoroutine(LoadAudioClip(temp3, "file:/// Windows反斜杠 :音频", Sources[2]));
        //temp3 = path6;
        //StartCoroutine(LoadAudioClip(temp3, "纯一般斜杠 :音频", Sources[3]));
        //temp3 = @"file://" + path6;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[4]));
        //temp3 = @"file:///" + path6;
        //StartCoroutine(LoadAudioClip(temp3, "file:/// 纯一般斜杠 :音频", Sources[5]));


        //视频
        //string path7 = @"D:\Desktop\常规项目\幻影成像\昆明市馆\程序\KMHYCX\Assets\StreamingAssets\Clip\2水草和鱼类\剑鱼.mp4";
        //string path8 = @"D:/Desktop/常规项目/幻影成像/昆明市馆/程序/KMHYCX/Assets/StreamingAssets/Clip/2水草和鱼类/剑鱼.mp4";
        //string temp4 = null;
        //temp4 = path7;
        //StartCoroutine(LoadVideoClip(temp4, "Windows反斜杠:视频", Players[0], VideoImgs[0]));
        //temp4 = @"file://" + path7;
        //StartCoroutine(LoadVideoClip(temp4, "file:// Windows反斜杠 :视频", Players[1], VideoImgs[1]));
        //temp4 = @"file:///" + path7;
        //StartCoroutine(LoadVideoClip(temp4, "file:/// Windows反斜杠 :视频", Players[2], VideoImgs[2]));
        //temp4 = path8;
        //StartCoroutine(LoadVideoClip(temp4, "纯一般斜杠 :视频", Players[3], VideoImgs[3]));
        //temp4 = @"file://" + path8;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :视频", Players[4], VideoImgs[4]));
        //temp4 = @"file:///" + path8;
        //StartCoroutine(LoadVideoClip(temp4, "file:/// 纯一般斜杠 :视频", Players[5], VideoImgs[5]));


        /* 局域网模式 */
        //// 文字
        //string path1 = @"\\192.168.1.132\xxx\1.txt";
        //string path2 = @"//192.168.1.132/xxx/1.txt";
        //string path3 = @"\\192.168.1.132/xxx/1.txt";
        //string path4 = @"192.168.1.132/xxx/1.txt";
        //string path5 = @"192.168.1.132\xxx\1.txt";
        //string temp = null;
        //temp = path1;
        //StartCoroutine(LoadText(temp, "Windows反斜杠:文字", Texts[0]));
        ////temp = @"file://" + path1;
        ////StartCoroutine(LoadText(temp, "file://  Windows反斜杠:文字", Texts[1]));
        //temp = @"file:///" + path1;
        //StartCoroutine(LoadText(temp, "file:/// Windows反斜杠:文字", Texts[2]));
        //temp = path2;
        //StartCoroutine(LoadText(temp, "纯一般斜杠 :文字", Texts[3]));
        //temp = @"file://" + path2;
        //StartCoroutine(LoadText(temp, "file:// 纯一般斜杠 :文字", Texts[4]));
        //temp = @"file:///" + path2;
        //StartCoroutine(LoadText(temp, "file:/// 纯一般斜杠 :文字", Texts[5]));
        //temp = path3;
        //StartCoroutine(LoadText(temp, "纯一般斜杠 :文字", Texts[6]));
        ////temp = @"file://" + path3;
        ////StartCoroutine(LoadText(temp, "file:// 纯一般斜杠 :文字", Texts[7]));
        //temp = @"file:///" + path3;
        //StartCoroutine(LoadText(temp, "file:/// 纯一般斜杠 :文字", Texts[8]));
        ////temp = path4;
        ////StartCoroutine(LoadText(temp, "纯一般斜杠 :文字", Texts[9]));
        //temp = @"file://" + path4;
        //StartCoroutine(LoadText(temp, "file:// 纯一般斜杠 :文字", Texts[10]));
        ////temp = @"file:///" + path4;
        ////StartCoroutine(LoadText(temp, "file:/// 纯一般斜杠 :文字", Texts[11]));
        //temp = path5;
        //StartCoroutine(LoadText(temp, "纯一般斜杠 :文字", Texts[12]));
        //temp = @"file://" + path5;
        //StartCoroutine(LoadText(temp, "file:// 纯一般斜杠 :文字", Texts[13]));
        //temp = @"file:///" + path5;
        //StartCoroutine(LoadText(temp, "file:/// 纯一般斜杠 :文字", Texts[14]));




        //// 图片 \\192.168.1.253\mqPhotos\2018_12_25_13_10_06.jpg     \\192.168.1.132\xxx\1.jpg
        //string path6 = @"\\192.168.1.132\xxx\1.jpg"; // 局域网路径
        //string path7 = @"//192.168.1.132\xxx\1.jpg"; // 局域网路径
        //string path8 = @"192.168.1.132\xxx\1.jpg"; // 局域网路径

        //string path9 = @"\\192.168.1.132/xxx/1.jpg"; // 局域网路径
        //string path10 = @"//192.168.1.132/xxx/1.jpg"; // 局域网路径
        //string path11 = @"192.168.1.132/xxx/1.jpg"; // 局域网路径
        //string temp2 = null;
        //temp2 = path6;
        //StartCoroutine(LoadTexture(temp2, "Windows反斜杠 :图片", Imgs[0]));
        //temp2 = @"file://" + path6;
        //StartCoroutine(LoadTexture(temp2, "file:// Windows反斜杠 :图片", Imgs[1]));
        //temp2 = @"file:///" + path6;
        //StartCoroutine(LoadTexture(temp2, "file:/// Windows反斜杠 :图片", Imgs[2]));

        //temp2 = path7;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[3]));
        //temp2 = @"file://" + path7;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[4]));
        //temp2 = @"file:///" + path7;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[5]));

        //temp2 = path8;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[6]));
        //temp2 = @"file://" + path8;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[7]));
        //temp2 = @"file:///" + path8;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[8]));

        //temp2 = path9;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[9]));
        //temp2 = @"file://" + path9;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[10]));
        //temp2 = @"file:///" + path9;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[11]));

        //temp2 = path10;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[12]));
        //temp2 = @"file://" + path10;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[13]));
        //temp2 = @"file:///" + path10;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[14]));

        //temp2 = path11;
        //StartCoroutine(LoadTexture(temp2, "纯一般斜杠 :图片", Imgs[15]));
        //temp2 = @"file://" + path11;
        //StartCoroutine(LoadTexture(temp2, "file:// 纯一般斜杠 :图片", Imgs[16]));
        //temp2 = @"file:///" + path11;
        //StartCoroutine(LoadTexture(temp2, "file:/// 纯一般斜杠 :图片", Imgs[17]));

        // 音频
        //string path12 = @"\\192.168.1.132\xxx\1.wav";
        //string path13 = @"//192.168.1.132\xxx\1.wav";
        //string path14 = @"192.168.1.132\xxx\1.wav";
        //string path15 = @"//192.168.1.132/xxx/1.wav";
        //string path16 = @"\\192.168.1.132/xxx/1.wav";
        //string path17 = @"192.168.1.132/xxx/1.wav";
        //string temp3 = null;
        //temp3 = path12;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[0]));
        //temp3 = @"file://" + path12;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[1]));
        //temp3 = @"file:///" + path12;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[2]));

        //temp3 = path13;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[3]));
        //temp3 = @"file://" + path13;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[4]));
        //temp3 = @"file:///" + path13;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[5]));

        //temp3 = path14;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[6]));
        //temp3 = @"file://" + path14;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[7]));
        //temp3 = @"file:///" + path14;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[8]));

        //temp3 = path15;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[9]));
        //temp3 = @"file://" + path15;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[10]));
        //temp3 = @"file:///" + path15;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[11]));

        //temp3 = path16;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[12]));
        //temp3 = @"file://" + path16;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[13]));
        //temp3 = @"file:///" + path16;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[14]));

        //temp3 = path17;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[15]));
        //temp3 = @"file://" + path17;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[16]));
        //temp3 = @"file:///" + path17;
        //StartCoroutine(LoadAudioClip(temp3, "file:// 纯一般斜杠 :音频", Sources[17]));


        // 视频
        //string path18 = @"\\192.168.1.132\xxx\1.mp4";
        //string path19 = @"//192.168.1.132\xxx\1.mp4";
        //string path20 = @"192.168.1.132\xxx\1.mp4";
        //string path21 = @"//192.168.1.132/xxx/1.mp4";
        //string path22 = @"\\192.168.1.132/xxx/1.mp4";
        //string path23 = @"192.168.1.132/xxx/1.mp4";
        //string temp4 = null;
        //temp4 = path18;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[0], VideoImgs[0]));
        //temp4 = @"file://" + path18;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[1], VideoImgs[1]));
        //temp4 = @"file:///" + path18;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[2], VideoImgs[2]));

        //temp4 = path19;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[3], VideoImgs[3]));
        //temp4 = @"file://" + path19;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[4], VideoImgs[4]));
        //temp4 = @"file:///" + path19;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[5], VideoImgs[5]));

        //temp4 = path20;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[6], VideoImgs[6]));
        //temp4 = @"file://" + path20;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[7], VideoImgs[7]));
        //temp4 = @"file:///" + path20;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[8], VideoImgs[8]));

        //temp4 = path21;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[9], VideoImgs[9]));
        //temp4 = @"file://" + path21;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[10], VideoImgs[10]));
        //temp4 = @"file:///" + path21;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[11], VideoImgs[11]));

        //temp4 = path22;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[12], VideoImgs[12]));
        //temp4 = @"file://" + path22;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[13], VideoImgs[13]));
        //temp4 = @"file:///" + path22;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[14], VideoImgs[14]));

        //temp4 = path23;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[15], VideoImgs[15]));
        //temp4 = @"file://" + path23;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[16], VideoImgs[16]));
        //temp4 = @"file:///" + path23;
        //StartCoroutine(LoadVideoClip(temp4, "file:// 纯一般斜杠 :图片", Players[17], VideoImgs[17]));


        // 网络
        // 文字
        //string path24 = @"https://www.baidu.com/";
        //string temp5 = null;
        //temp5 = path24;
        //StartCoroutine(LoadText(temp5, "纯一般斜杠 :图片", Texts[0]));
        // 图片
        //string path25 = @"https://www.fyzserver.com/photos/SC_CD_PZMQ/100.jpg";
        //string temp6 = null;
        //temp6 = path25;
        //StartCoroutine(LoadTexture(temp6, "纯一般斜杠 :图片", Imgs[0]));
        //音频
        //string path26 = @"https://www.fyzserver.com/audios/SC_CD_PZMQ/1.wav";
        //string temp7 = null;
        //temp7 = path26;
        //StartCoroutine(LoadAudioClip(temp7, " 纯一般斜杠 :图片", Sources[0]));

        // 视频
        string path27 = @"http://www.fyzserver.com/videos/SC_CD_PZMQ/2.mp4";
        string temp8 = null;
        temp8 = path27;
        StartCoroutine(LoadVideoClip(temp8, "纯一般斜杠 :视频", Players[0], VideoImgs[0]));

        #endregion

        #region File

        /* 本地文件 */
        //// 文字
        //string path1 = @"D:\Desktop\编程学习总结\ProgramSummary\Unity总结\Unity_WWW和WWWForm类\WWWLearn\Assets\StreamingAssets\1.txt";
        //string path2 = @"D:/Desktop/编程学习总结/ProgramSummary/Unity总结/Unity_WWW和WWWForm类/WWWLearn/Assets/StreamingAssets/1.txt";
        //string temp1 = null;
        //temp1 = path1;
        //ReadFile(temp1, "Windows反斜杠:文字", Texts[0]);
        //temp1 = path2;
        //ReadFile(temp1, "纯一般斜杠 :文字", Texts[3]);
        //// 图片
        //string path3 = @"D:\Desktop\常规项目\拍照魔墙\Texture\5.jpg";
        //string path4 = @"D:/Desktop/常规项目/拍照魔墙/Texture/5.jpg";
        //string temp2 = null;
        //temp2 = path3;
        //ReadTexture(temp2, "Windows反斜杠 :图片", Imgs[0]);
        //temp2 = path4;
        //ReadTexture(temp2, "纯一般斜杠 :图片", Imgs[3]);
        // 音频
        //string path5 = @"D:\Desktop\常规项目\魔墙\05_上海站人才魔墙\资料\软件资料\六十佳录音\新建文件夹\薄岗.wav";
        //string path6 = @"D:/Desktop/常规项目/魔墙/05_上海站人才魔墙/资料/软件资料/六十佳录音/新建文件夹/薄岗.wav";
        //string temp3 = null;
        //temp3 = path5;
        //ReadMulti(temp3, "Windows反斜杠:音频");
        //temp3 = path6;
        //ReadMulti(temp3, "纯一般斜杠 :音频");
        //视频
        //string path7 = @"D:\Desktop\常规项目\幻影成像\昆明市馆\程序\KMHYCX\Assets\StreamingAssets\Clip\2水草和鱼类\剑鱼.mp4";
        //string path8 = @"D:/Desktop/常规项目/幻影成像/昆明市馆/程序/KMHYCX/Assets/StreamingAssets/Clip/2水草和鱼类/剑鱼.mp4";
        //string temp4 = null;
        //temp4 = path7;
        //ReadMulti(temp4, "Windows反斜杠:视频");
        //temp4 = path8;
        //ReadMulti(temp4, "纯一般斜杠 :视频");

        /* 局域网模式 */
        // 文字
        //string path1 = @"\\192.168.1.132\xxx\1.txt";
        //string path2 = @"//192.168.1.132/xxx/1.txt";
        //ReadFile(path1, "Windows反斜杠:文字", Texts[0]);
        //ReadFile(path2, "纯一般斜杠 :文字", Texts[1]);

        //// 图片 
        //string path6 = @"\\192.168.1.132\xxx\1.jpg"; // 局域网路径
        //string path10 = @"//192.168.1.132/xxx/1.jpg"; // 局域网路径
        //ReadTexture(path6, "Windows反斜杠 :图片", Imgs[0]);
        //ReadTexture(path10, "纯一般斜杠 :图片", Imgs[1]);

        //音频
        //string path12 = @"\\192.168.1.132\xxx\1.wav";
        //string path15 = @"//192.168.1.132/xxx/1.wav";
        //ReadMulti(path12, "Windows反斜杠  :音频");
        //ReadMulti(path15, "纯一般斜杠 :音频");


        // 视频
        //string path18 = @"\\192.168.1.132\xxx\1.mp4";
        //string path21 = @"//192.168.1.132/xxx/1.mp4";
        //ReadMulti(path18, "Windows反斜杠  : 视频");
        //ReadMulti(path21, "纯一般斜杠 : 视频");

        #endregion

    }

    #region WWW类
    private IEnumerator LoadText(string path, string str, Text text)
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone)
        {
            print(str + "      " + path + "    ");
            print(www.bytes.Length);
            if (www.bytes.Length > 0)
            {
                text.text = www.text;
            }
        }
    }
    private IEnumerator LoadTexture(string path, string str, RawImage img)
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone)
        {
            if (www.texture != null)
            {
                print(str + "      " + path);
                print(www.bytes.Length);
                if (www.bytes.Length > 0)
                {
                    img.texture = www.texture;
                }
            }
        }
    }
    private IEnumerator LoadAudioClip(string path, string str, AudioSource sources)
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone)
        {
            if (www.GetAudioClip())
            {
                print(str + "      " + path);
                if (www.bytes.Length > 0)
                {
                    //string loadpath = @"https://www.fyzserver.com/audios/upload.php?folderName=SC_CD_PZMQ&audio_type=wav";
                    //WWWForm form = new WWWForm();
                    //form.AddField("user", "wujiaze");
                    //form.AddField("password", "fyz123456");
                    //form.AddField("audioName", "1");
                    //form.AddBinaryData("audioData", www.bytes);

                    //WWW ww = new WWW(loadpath, form);
                    //yield return ww;
                    //if (ww.isDone)
                    //{
                    //    print(ww.text);
                    //}
                    AudioClip clip = www.GetAudioClip();
                    sources.clip = clip;
                    sources.Play();
                }
            }
        }
    }
    private IEnumerator LoadVideoClip(string path, string str, VideoPlayer player, GameObject videoImg)
    {
        WWW www = new WWW(path);
        yield return www;
        while (!www.isDone)
        {
            Debug.Log(www.progress);
            yield return null;
        }
        if (www.isDone)
        {
            //print(www.bytes.Length);
            //// 上传
            //if (www.bytes.Length > 0)
            //{
            //    string loadpath = @"http://www.fyzserver.com/videos/upload.php?folderName=SC_CD_PZMQ&video_type=mp4";
            //    WWWForm form = new WWWForm();
            //    form.AddField("user", "wujiaze");
            //    form.AddField("password", "fyz123456");
            //    form.AddField("videoName", "2");
            //    form.AddBinaryData("videoData", www.bytes);
            //    WWW ww = new WWW(loadpath, form);
            //    yield return ww;
            //    if (ww.isDone)
            //    {
            //        print(ww.text);
            //    }
            //}

            // 下载
            print(str + "      " + path);
            player.url = www.url;
            print(player.url);
            player.renderMode = VideoRenderMode.MaterialOverride;
            player.targetMaterialRenderer = videoImg.GetComponent<Renderer>();
            player.targetMaterialProperty = "_MainTex";
            player.Play();
        }
    }


    #endregion

    #region File类
    private void ReadFile(string path, string str, Text text)
    {
        try
        {
            string txt = File.ReadAllText(path);
            text.text = txt;
            print(str + "      " + path);
        }
        catch (Exception e)
        {
            print(str + "      " + path);
        }
    }

    private void ReadTexture(string path, string str, RawImage img)
    {
        try
        {
            Texture2D tex = new Texture2D(1360, 768, TextureFormat.DXT1, false);
            byte[] bytes = File.ReadAllBytes(path);
            tex.LoadImage(bytes);
            tex.Apply();
            img.texture = tex;
            print(str + "      " + path);
        }
        catch (Exception e)
        {
            print(str + "      " + path);
        }
    }

    private void ReadMulti(string path, string str)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            print(bytes.Length);
        }
        catch (Exception e)
        {
            print(str + "      " + path);
        }
    }
    #endregion

}
