using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{

    public MediaPlayer MediaPlayer;

    public Toggle TogPlay;

    public Toggle TogLoop;

    public Dropdown DropSpeed;

    public Slider SliProcess;

    public Slider SliVolume;
    private void Awake()
    {
        TogPlay.onValueChanged.AddListener(TogPlayCallBack);
        TogLoop.onValueChanged.AddListener(TogLoopCallBack);
        DropSpeed.onValueChanged.AddListener(AddSpeed);
        SliProcess.onValueChanged.AddListener(SliProcessCallBack);
        SliVolume.onValueChanged.AddListener(VolumeCallBack);
        MediaPlayer.Events.AddListener(EventCallBack);
    }



    private void Start()
    {
        LoadMovie();
        SliVolume.value = MediaPlayer.Control.GetVolume();
    }

    private void Update()
    {
        SetTimerStyle();
        SliProcess.value = MediaPlayer.Control.GetCurrentTimeMs() / MediaPlayer.Info.GetDurationMs();
    }

    // 加载视频
    private void LoadMovie()
    {
        MediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "test.mp4", false);
    }

    /* 播放暂停 */
    private void TogPlayCallBack(bool arg0)
    {
        if (arg0)
        {
            MediaPlayer.Control.Play();
            TogPlay.GetComponentInChildren<Text>().text = "暂停";
        }
        else
        {
            MediaPlayer.Control.Pause();
            TogPlay.GetComponentInChildren<Text>().text = "播放";
        }
    }

    /* 是否循环 */
    private void TogLoopCallBack(bool arg0)
    {
        if (arg0)
        {
            MediaPlayer.Control.SetLooping(true);
            TogLoop.GetComponentInChildren<Text>().text = "不循环";
        }
        else
        {
            MediaPlayer.Control.SetLooping(false);
            TogLoop.GetComponentInChildren<Text>().text = "循环";
        }
    }

    /* 改变播放速率 */
    private void AddSpeed(int value)
    {
        switch (value)
        {
            case 0:
                MediaPlayer.Control.SetPlaybackRate(1);
                break;
            case 1:
                MediaPlayer.Control.SetPlaybackRate(2);
                break;
            case 2:
                MediaPlayer.Control.SetPlaybackRate(0.5f);
                break;
        }
    }

    /* 拖动进度条 */
    private void SliProcessCallBack(float arg0)
    {
        MediaPlayer.Control.Seek(arg0 * MediaPlayer.Info.GetDurationMs());
    }

    public void SliProcessBeginDragCallBack()
    {
        MediaPlayer.Control.Pause();
    }

    public void SliProcessEndDragCallBack()
    {
        MediaPlayer.Control.Play();
    }
    // 显示的格式
    private void SetTimerStyle()
    {
        int scends = (int)(MediaPlayer.Control.GetCurrentTimeMs() / 1000);
        int currentMin = scends / 60;
        int currentScends = scends % 60;
        string currentScend = currentScends < 10 ? "0" + currentScends : currentScends.ToString();

        int totalScends = (int)(MediaPlayer.Info.GetDurationMs() / 1000);
        int totalMin = totalScends / 60;
        int scends3 = totalScends % 60;
        string toatls = scends3 < 10 ? "0" + scends3 : scends3.ToString();

        SliProcess.GetComponentInChildren<Text>().text =currentMin + ":" + currentScend + " / " + totalMin + ":" + toatls;
    }
    // 清晰度切换：切换文件 seek 到当前时间  
    // 回退和快进 ： seek 时间 即可

    /* 音量控制 */
    private void VolumeCallBack(float arg0)
    {
        MediaPlayer.Control.SetVolume(arg0);
        //MediaPlayer.Control.MuteAudio(true);    //静音
       
    }
    /* 事件 */
    private void EventCallBack(MediaPlayer arg0, MediaPlayerEvent.EventType arg1, ErrorCode arg2)
    {
        switch (arg1)
        {
            case MediaPlayerEvent.EventType.Closing:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                break;
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("播放结束");                  // 循环播放时，不会触发
                break;
            case MediaPlayerEvent.EventType.Error:
                break;
            case MediaPlayerEvent.EventType.SubtitleChange:
                Debug.Log("字幕改变");
                break;
            case MediaPlayerEvent.EventType.Stalled:
                break;
            case MediaPlayerEvent.EventType.Unstalled:
                break;
            default:
                break;
        }
    }

}
