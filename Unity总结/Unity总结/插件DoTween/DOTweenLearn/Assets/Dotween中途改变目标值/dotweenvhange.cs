using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class dotweenvhange : MonoBehaviour
{
    private Tweener tweener;
	void Start ()
	{
	    //tweener = transform.DOLocalMove(new Vector3(500, 100, 0), 5);

	    tweener = transform.DOLocalMove(transform.localPosition + new Vector3(10, 0, 0), 5);
	}
	
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Y))
	    {
	        tweener.ChangeEndValue(new Vector3(-500, -100, 0));
	    }
	}
}
