using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multweenertest : MonoBehaviour {

    private Tweener tweener;
    private Tweener tweener2;
    private Tweener tweener3;
    private Tweener tweener4;
    void Start()
    {
        //tweener = transform.DOLocalMove(new Vector3(-500, 100, 0), 10);
        //tweener.SetEase(Ease.Linear);
        //tweener2 = transform.DOLocalMove(Vector3.zero, 5);
        //tweener3 = transform.DOLocalMove(new Vector3(200, 500, 0), 2);
        tweener4.SetAutoKill(false);
        tweener4.SetLoops(3);
        tweener4.Pause();
    }
    // 测试说明：transform相当于有三个动画序列，越后设置的越先播放，然后若前面的动画还未执行完毕，这执行前面的
    // 测试说明：只有当Dotween值存在时，设置的属性有效果
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            tweener4 = transform.DOLocalMove(new Vector3(200, 500, 0), 2);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            tweener4.Play();
        }
    }
}
