using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Tweener tweener;
    void Start()
    {
        Debug.Log("isBackwards" + tweener.isBackwards);
        Debug.Log("IsActive" + tweener.IsActive());
        Debug.Log("IsComplete" + tweener.IsComplete());
        Debug.Log("IsInitialized" + tweener.IsInitialized());
        Debug.Log("IsBackwards" + tweener.IsBackwards());
        Debug.Log("IsPlaying" + tweener.IsPlaying());
        tweener.Kill();
        tweener = transform.DOMove(new Vector3(5, 5, 5), 3);
        tweener.Kill();
    }


    void Update()
    {
        if (tweener != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("isBackwards" + tweener.isBackwards);
                Debug.Log("IsActive" + tweener.IsActive());
                Debug.Log("IsComplete" + tweener.IsComplete());
                Debug.Log("IsInitialized" + tweener.IsInitialized());
                Debug.Log("IsBackwards" + tweener.IsBackwards());
                Debug.Log("IsPlaying" + tweener.IsPlaying());
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            tweener.Kill();
        }
    }
}
