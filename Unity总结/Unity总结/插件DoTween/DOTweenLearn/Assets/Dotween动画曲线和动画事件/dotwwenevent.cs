using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class dotwwenevent : MonoBehaviour
{
    public RectTransform image;
    private Tweener tweener;

    void Start ()
	{
	    tweener = image.DOLocalMoveX(500, 2);
        // 动画的属性都是 tweener 对象控制的
	   tweener.SetEase(Ease.Linear); //这里选择线性缓冲

        // 动画循环次数 -1：代表无数次
	    tweener.SetLoops(1); 
	    // 动画事件：动画结束事件
	    tweener.OnComplete(TComplete);
        
	}

    private void TComplete()
    {
        Debug.Log("动画结束");
    }



    // Update is called once per frame
    void Update ()
    {
        // 获取动画对象的游戏对象
        if (tweener.target != null)
        {
            Debug.Log(((RectTransform)tweener.target).localPosition.x);
        }
        
    }
}
