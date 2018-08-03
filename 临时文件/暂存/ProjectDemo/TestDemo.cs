using System;
using Framework;
using UnityEngine;

public class TestDemo : MonoBehaviour {

	
	void Start ()
	{
	    string log4netPath = Application.dataPath + "/ProjectDemo/log4net.xml";
	    Log4netHelper.SetLogPath(log4netPath);
	    int x = 10;
	    int y = 0;
	    try
	    {
	        x = x / y;
	    }
	    catch (Exception e)
	    {
	        Debug.Log(e.ToString());
	        Log4netHelper.LogFatal(People.A,"我");
	        Log4netHelper.LogDebug(People.B, e.ToString());
	    }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
