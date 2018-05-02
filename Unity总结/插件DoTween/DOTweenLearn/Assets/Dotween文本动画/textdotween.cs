using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class textdotween : MonoBehaviour
{

    public Text text;
    private Tweener tweener;
    void Start()
    {
        // dotween 的效果不仅仅用在坐标变换，文字显示也能用，不过很单一，也可以调节焕驰能够缓冲函数什么的
        tweener =  text.DOText("// dotween 的效果不仅仅用在坐标变换，文字显示也能用，不过很单一，也可以调节焕驰能够缓冲函数什么的", 4);
        tweener.SetAutoKill(false);
        tweener.Pause();
    }

    // Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        tweener.Play();

	    }	
	}
}
