using System;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class SinglePlayerTest : MonoBehaviour
{

    private MediaPlayer player;
    private DisplayUGUI _displayUgui;

    private void Awake()
    {
        player = new GameObject("MediaPlyaer").AddComponent<MediaPlayer>();
        _displayUgui = GameObject.Find("AVPro Video").GetComponent<DisplayUGUI>();
    }

    void Start ()
    {
        string path = "game_startok";
        player.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, path, false);
       

    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
           print(Environment); 
	    }
	}
}
