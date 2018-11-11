/*
 *     锚点和中心点
 *
 *      基础：
 *         一、Canvas
 *            Canvas 的锚点在 左下角(0,0)
 *            Canvas 的中心点在 中心(0.5,0.5)*分辨率
 *         二、RectTransform
 *            Inspector面板上中 RectTransform 中显示的坐标 
 *              anchoredPosition    对象自身中心点 相对于 自身锚点
 *            另外两个坐标
 *              localPosition       对象自身中心点 相对于父物体的中心点
 *              position            对象自身中心点 相对于Canvas左下角(0,0)的坐标
 */

using UnityEngine;

public class Introductions : MonoBehaviour
{
    public RectTransform _rectTrans;
    public RectTransform _rectParent;
    public RectTransform _rectChild;
    private void Start()
    {
        
    }

    private void Update()
    {
        print("anchoredPosition " + _rectTrans.anchoredPosition);// 对象自身中心点 相对于自身锚点

        print("localPosition " + _rectChild.localPosition);      // 对象自身中心点 相对于父物体的中心点
        print("position " + _rectChild.position);                // 对象自身中心点 相对于Canvas左下角(0,0)的坐标
    }



}

