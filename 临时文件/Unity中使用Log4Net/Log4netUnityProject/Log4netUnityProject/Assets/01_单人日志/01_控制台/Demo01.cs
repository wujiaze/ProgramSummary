using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo01 : MonoBehaviour {

	
	void Start ()
	{
	    LogHelper.SetLogPath(Application.dataPath + "/01_单人日志/01_控制台/log4net.config");
	}
	
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
            LogHelper.LogDebug("你好");
	    }
	}
}
