using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    LogHelper.LogDebug(LogPeople.A, "你好！");
	    LogHelper.LogInfo(LogPeople.A, "你好！");
	    LogHelper.LogWarn(LogPeople.B, "你好！");
	    LogHelper.LogError(LogPeople.A, "你好！");
	    LogHelper.LogFatal(LogPeople.B, "你好！");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
