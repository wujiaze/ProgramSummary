using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
        // 设置音频
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource); // 为index为0的音轨，设置目标audio


        videoPlayer.clip = videoclip;
        videoPlayer.Prepare();
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
