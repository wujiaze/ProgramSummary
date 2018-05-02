using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class textdottset : MonoBehaviour
{

    public DOTweenAnimation da;
	void Start ()
	{
        // 在可视化面板设置好之后，就可以用代码操作播放之类的事情了
	    //da.DOPlay();
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
            // 这种方式播放，可以点击来暂停
	        da.DOTogglePause();
	    }
    }
}
