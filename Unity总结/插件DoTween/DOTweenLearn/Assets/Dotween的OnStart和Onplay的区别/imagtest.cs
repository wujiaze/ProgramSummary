using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class imagtest : MonoBehaviour
{
    private Tweener tweener;

    void Start ()
	{
	    tweener = transform.DOLocalMove(Vector3.zero, 2);
	    tweener.Pause();
	    tweener.OnPlay(onplay);
	    tweener.OnStart(onstart);
	    tweener.SetAutoKill(false);
	    tweener.SetDelay(5);
	    tweener.SetLoops(5);

	}

    private void onstart()
    {
        Debug.Log("start");
    }

    private void onplay()
    {
        Debug.Log("play");
    }

    // 测试说明：
    // 1、OnStart 和 OnPlay都是在延迟结束之后才调用
    // Onstart只有在整个Dotwee动画第一次开始播放时调用
    // OnPlay会在整个Dotwee动画开始时调用，也会在暂停之后重新开始调用
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tweener.Pause();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            tweener.Play();
        }
    }
}
