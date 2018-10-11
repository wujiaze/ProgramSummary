using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DotweenInit : MonoBehaviour
{

    private void Awake()
    {
        /* Dotween初始化 建议手动初始化*/
        /*  DOTween.Init
         *      参数 recycleAllByDefault：是否回收全部的Tween,默认为false。当为true时，需要手动控制每一个引用，比较麻烦，建议采用false
         *      可以修改：DOTween.defaultRecyclable
         *
         *      参数 userSafeMode       ：是否采用安全模式。默认为true。比如：当目标销毁dotween能够自行处理不会报错。另外官网给出了一些特殊情况是不能使用的，所以还是要自己注意，建议采用false
         *      可以修改：DOTween.useSafeMode
         *
         *      参数 logBehaviour       ：DOTween信息打印，默认是ErrorsOnly，建议采用 Default：打印错误和警告信息。Verbose：打印所有信息
         *      可以修改：DOTween.logBehaviour
         *
         *  多次使用DOTween.Init，不会出错，都是使用第一次初始化的那个
         */
        /*
         *  唯一一个需要设置在初始化后面的Set：SetCapacity
         *  SetCapacity:设置Tween(动画) 和 Sequences(一系列动画，动画序列) 的个数，默认是200个Tween 和 50个Sequences
         *              当动画数量超过设定值时，Capacity会自动扩容
         *              但是自动扩容有时可能出错，所以还是自己提前定义好
         */
        DOTween.Init(false,true,LogBehaviour.Default).SetCapacity(2000,100);
        
        //DOTween.Clear();


        ///* 设置 */
        //// 全局设置：应用到每一个Tween
        //// 和 DOTween.Init 的设置很像
        //// 自动开始：在全局设置中，单个Tween无法设置 todo
        //// 自动Kill：可以单个设置，也可以在全局中设置
        //// 如果要重用Tween，设置SetAutoKill(false)
        //// 特定设置：为具体的Tween进行设置
        //// 一般采用链式设置 推荐
        //transform.DOMove(new Vector3(2, 2, 2), 2)
        //    // 设置
        //    .SetAutoKill(true)
        //    .SetRelative(true)
        //    .SetDelay(1)
        //    .SetEase(Ease.InBounce)
        //    .SetId(1)
        //    .SetLoops(-1, LoopType.Yoyo)
        //    .SetRecyclable(true)
        //    .SetSpeedBased()
        //    .SetTarget()
        //    .SetUpdate() // todo 默认值以及意义
        //     // 回调
        //    .OnComplete()
        //    .OnKill()
        //    .OnPause()
        //    .OnPlay()
        //    .OnRewind()
        //    .OnStart()
        //    .OnStepComplete()
        //    .OnUpdate()
        //    .OnWaypointChange();

        //// 链式设置中 SetAs : 完全复制额外的设置，同时主体是可以不同的
        //Tween target = transform.DOMove(new Vector3(2, 2, 2), 2)
        //    .SetEase(Ease.InCubic)
        //    .SetLoops(4)
        //    .OnComplete();
        //transform.GetComponent<Material>().DOColor(Color.black, 2)
        //    .SetAs(target); // 相当于设置了SetEase(Ease.InCubic)  SetLoops(4)   OnComplete()

        //// 链式设置中的特殊设置
        //transform.DOMove(new Vector3(2, 2, 2), 2)
        //    .SetOptions();   // 版本问题？ todo 意思 必须在链式设置的第一位


        //// 不采用链式设置，普通设置
        //Tween tween = transform.DOMove(new Vector3(2, 2, 2), 2);
        //tween.SetEase(Ease.Flash);
        //tween.SetAutoKill(false);



    }


    void Start()
    {
      



    }

   

}
