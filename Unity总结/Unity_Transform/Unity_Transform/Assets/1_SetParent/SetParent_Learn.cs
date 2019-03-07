/*
 *  前提： 相对坐标和绝对坐标之间的转换  绝对坐标 = 参考值*相对坐标
 *                                    相对坐标 = 参考值/相对坐标
 *  Scale
 *      两个概念
 *          localScale
 *              1、localScale      直接改变
 *              2、SetParent(true) 间接改变         
 *          lossyScale
 *              1、任何一个对象的默认初始为 1
 *              2、通过改变 localScale 间接改变
 *      一个计算公式
 *          自身当前 lossyScale = 默认初始值1 * 自身localScale * 父物体1 localScale * 父物体2 localScale ...
 *              注意：1、每一次物体的改变(比如：生成、改变父物体)，都会计算一遍整个式子
 *                    2、这个方法会导致失真，比如：某一父物体localScale=0.1125，但被近似为0.1，导致最后结果失真
 *      一个面板显示
 *          属性面板上看到的 Scale 都是 localScale
 *
 *
 *
 *   SetParent (Transform parent, bool worldPositionStays);
 *              参数1：parent  新父物体
 *              参数2：worldPositionStays
 *                      true(默认)  不改变方法执行前子物体的 lossyScale
 *                      false       不改变方法执行前子物体的 localScale
 *              具体分析：
 *                          子物体执行前的 lossyScaleA = 默认初始值1 * 子物体执行前的 localScaleA * 父物体 localScaleA  * 父物体的父物体 localScaleA...
 *                          子物体执行后的 lossyScaleB = 默认初始值1 * 子物体执行后的 localScaleB * 新父物体 localScaleB  * 新父物体的父物体 localScaleB...
 *                  true：  执行后的 lossyScaleB = 执行前的 lossyScaleA
 *                          求得：子物体执行后的 localScaleB = 子物体执行前的 lossyScaleA /  新父物体 localScaleB / 新父物体的父物体 localScaleB / ...
 *                  false： 执行后的 localScaleB = 执行前的 localScaleA
 *                          求得：子物体执行后的 lossyScaleB = 默认初始值1 * 执行前的 localScaleA  *  新父物体 localScaleB * 新父物体的父物体 localScaleB ...
 *
 *              特殊情况：当某个父物体 localScale = 0 
 *                          true :  localScale = 0
 *                                  lossyScale = 0（看不到了）
 *                          false:  localScale = 原来的scale
 *                                  lossyScale = 0（看不到了）
 *                      解决方法:
 *                              1、针对第二次SetParent后，物体需要时原来的样子，那么可以是第一次采用 false ，第二次也采用false
 *              总结： lossyScale = 默认值1 * 每个子物体的 localScale
 *                    localScale = 默认值1  / 每个子物体的 localScale
 *  
 */
using UnityEngine;

public class SetParent_Learn : MonoBehaviour
{
    private GameObject uiPrefab;
    private GameObject _2dPrefab;
    private GameObject _3dPrefab;

    public Transform UiParent;
    public Transform UiParentZero;
    void Start()
    {
        uiPrefab = Resources.Load<GameObject>("uiPrefab");
        _2dPrefab = Resources.Load<GameObject>("2dPrefab");
        _3dPrefab = Resources.Load<GameObject>("3dPrefab");

        // 步骤1 ：测试预制体状态

        Debug.Log("lossyScale " + uiPrefab.transform.lossyScale);
        Debug.Log("localScale " + uiPrefab.transform.localScale);
    }

    private GameObject uigo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            uigo = Instantiate(uiPrefab);
            Debug.Log("lossyScale " + uigo.transform.lossyScale);
            Debug.Log("localScale " + uigo.transform.localScale);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            uigo.transform.SetParent(UiParent, false);
            Debug.Log("lossyScale " + uigo.transform.lossyScale);
            Debug.Log("localScale " + uigo.transform.localScale);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            uigo.transform.SetParent(UiParent, true);
            Debug.Log("lossyScale " + uigo.transform.lossyScale);
            Debug.Log("localScale " + uigo.transform.localScale);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            uigo.transform.SetParent(UiParentZero, false);
            Debug.Log("lossyScale " + uigo.transform.lossyScale);
            Debug.Log("localScale " + uigo.transform.localScale);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            uigo.transform.SetParent(UiParentZero, true);
            Debug.Log("lossyScale " + uigo.transform.lossyScale);
            Debug.Log("localScale " + uigo.transform.localScale);
        }

    }
}
