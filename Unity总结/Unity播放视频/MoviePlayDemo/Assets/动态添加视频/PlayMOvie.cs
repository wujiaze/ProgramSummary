using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// 优点：可动态设置分辨率
//      可动态更换视频素材
public class PlayMOvie : MonoBehaviour {

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public RawImage image;
    public VideoClip videoclip;
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }
    IEnumerator playVideo()
    {
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.source = VideoSource.VideoClip;
        // 设置音频 这种方法设置的是Video自带的音频
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource); // 为index为0的音轨，设置目标audio
        // 这种方法是设置我们添加的音频
        //audioSource.clip = xxxx;
        videoPlayer.clip = videoclip; // 这一步可以通过资源加载完成
        videoPlayer.Prepare(); // 等待视频加载完成
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }
        Debug.Log("Done Preparing Video");
        Debug.Log(videoPlayer.texture.width);
        // 一定要准备好才能设置画面
        image.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
    }
}
