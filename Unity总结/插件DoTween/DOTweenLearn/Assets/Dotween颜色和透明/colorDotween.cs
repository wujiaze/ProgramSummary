using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class colorDotween : MonoBehaviour
{

    public Image image;
    public GameObject cube;

	void Start ()
	{
        // 改变颜色
	    Tweener tweener = image.DOColor(Color.blue, 2);
        // 设置延时
	    tweener.SetDelay(0.5f);
        // 改变颜色的透明度
        image.DOFade(0, 2);

	    cube.GetComponent<MeshRenderer>().materials[0].DOColor(Color.red, 2);
	    cube.GetComponent<MeshRenderer>().materials[0].DOFade(0, 2);

        // 还可以播放暂停，暂停播放
	    //tweener.TogglePause();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
