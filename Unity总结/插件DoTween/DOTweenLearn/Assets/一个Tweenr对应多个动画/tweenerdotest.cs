using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class tweenerdotest : MonoBehaviour
{
    private Tweener tweener;

    void Start()
    {
        // 测试说明：后一个动画会覆盖前一个动画，但是如果后一个动画运行结束，前一个动画还在持续的话，会继续执行前一个动画剩余的部分
        // 所以这样直接覆盖不可取
        // 1、
        //tweener = transform.DOLocalMove(new Vector3(500, -100, 0), 2);
        //tweener = transform.DOLocalMove(Vector3.zero, 10);
        // 2、
        //tweener = transform.DOLocalMove(Vector3.zero, 10);
        //tweener = transform.DOLocalMove(new Vector3(500, -100, 0), 2);


        tweener = transform.DOLocalMove(new Vector3(-500, 100, 0), 10);
        tweener.SetEase(Ease.Linear);
    }

    // 测试说明：一个tweener只能执行一个动画
    // 但可以在这个未结束时，kill，然后添加新的动画
    void Update()
    {
        if (Time.time > 5)
        {
            if (tweener != null)
            {
                tweener.Kill();
                tweener = transform.DOLocalMove(new Vector3(500, -100, 0), 2);
                tweener.SetEase(Ease.Linear);
            }
        }
        // 方法二 ：使用ChangeEndValue方法
        if (Input.GetKeyDown(KeyCode.Y))
        {
            tweener.ChangeEndValue(new Vector3(-500, -100, 0),2);
        }
    }
}
