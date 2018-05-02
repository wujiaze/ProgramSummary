using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class withoutAudio : MonoBehaviour {

    public VideoPlayer vp;
    public AudioSource audio;
    //RenderTexture movierender;
    public RawImage rawimage;
    private void Awake()
    {

    }
    private void Start()
    {
        //vp.Prepare();
        StartCoroutine(play());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vp.Play();
            audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            vp.Pause();
            audio.Pause();
        }
    }
    IEnumerator play() {
        while (!vp.isPrepared)
        {
            yield return null;
        }
        // 必须等vp准备完成，才能将图片赋给rawimage，不然是空的
        rawimage.texture = vp.texture;
    }
}
