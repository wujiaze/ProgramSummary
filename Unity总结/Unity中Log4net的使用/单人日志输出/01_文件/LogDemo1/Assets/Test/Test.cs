using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        LogHelper.LogDebug("你好！");
	        LogHelper.LogInfo("你好！");
	        LogHelper.LogWarn("你好！");
	        LogHelper.LogError("你好！");
	        LogHelper.LogFatal("你好！");
        }
	}
}
