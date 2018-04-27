using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenLearn : MonoBehaviour {
    #region 改变变量值
    public Vector3 startpos = new Vector3(0,0,0);
    private Vector3 endpos = new Vector3(300, 300, 10);
    public float value = 0;
    public float endvalue = 10;
    #endregion

    #region 改变3D/UI物体位置
    public Transform cube;
    public RectTransform ui;
    #endregion

    #region 方法2

    public RectTransform taskui;
    public Transform cube2;

    #endregion

    void Start()
    {
        #region 线性位移，无特效   方法 DOTween.To()
        // 将startpos从 0 0 0 变化到 10 10 10 ，有一种插值的变化
        // () => startpos ：这一步表示输入起始的位置
        // x => startpos = x: 这一步将插值的值赋给自己，在这步中还可以添加自己的方法 Debug.Log("ssss"); x是一种代指，可以替换
        // endpos : 目标值
        // 2 ：持续时间
        // 这是一种委托，用协程的方式在进行，所以不会卡主程序
        DOTween.To(() => startpos, x => { startpos = x; Debug.Log("ssss"); }, endpos, 2);
        // 更改float类型
        DOTween.To(() => value, x => value = x, endvalue, 2);
        // 改变cube位置
        DOTween.To(() => cube.position, x => cube.position = x, endpos, 2);
        #endregion

        #region 线性位移，无特效 但更简单 方法2  dotween插件扩展了一些方法,比如RectTransform和Transform的实例方法

        cube2.DOMove(new Vector3(10, 10, 10), 2);
        //taskui.DOMove(new Vector3(10,10,10),2f); // 在两秒内，改变目标从原始位置到指定位置   ，世界坐标
        taskui.DOLocalMove(new Vector3(10, 10, 10), 1f); // 在两秒内，改变目标从原始位置到指定位置 ，本地坐标
        #endregion
    }


    // Update is called once per frame
    void Update ()
    {
        // 改变UI位置 换一种方式
        ui.localPosition = startpos;
    }
}
