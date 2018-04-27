using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class dotweenCircle : MonoBehaviour
{

    public RectTransform image;
    private Tweener tweener;

    public RectTransform i2;
    private Tweener t2;
    void Start ()
	{
	    
        // DoLocalMove 方法其实是产生了一个 动画对象 tweener 
        tweener =  image.DOLocalMove(new Vector3(500, 100, 0), 2f);
	    tweener.SetAutoKill(false); // 动画创建出来默认是播放一次就销毁
	    tweener.Pause(); // 动画创建出来就会自动播放

        // 另外一种算不上倒放的倒放
        // 加了From（false）或From（） 表示从X =400 ，运动到当前位置
	    //t2 = i2.DOLocalMoveX(400, 5).From(false);
        // From(true) 表示是相对坐标，从相对于当前坐标x+200的位置，运动到当前位置；
        t2 = i2.DOLocalMoveX(400, 5).From(true);
    }
	
	// Update is called once per frame
	void Update () { 
	    if (Input.GetKeyDown(KeyCode.A))
	    {
            //tweener.Play(); // play方法默认播放一次就销毁对象
            tweener.PlayForward(); // 正常播放，但不销毁动画对象
            // 当然还可以通过
            //image.DOPlay(); // 但这种也只能执行一次，并且会执行image上的所有动画对象
            
        }
	    if (Input.GetKeyDown(KeyCode.B))
	    {
	        tweener.PlayBackwards();//倒放，不销毁动画对象
        }
	}
}
