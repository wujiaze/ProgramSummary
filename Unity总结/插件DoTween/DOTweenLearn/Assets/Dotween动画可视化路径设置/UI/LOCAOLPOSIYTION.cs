using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class LOCAOLPOSIYTION : MonoBehaviour {

    // 测试说明 ：Canvas 的 position和localPosition是同一个，就是面板上显示的一般使用localPosition
    void Start()
    {
        Debug.Log(GameObject.Find("Canvas").transform.position);
        Debug.Log(GameObject.Find("Canvas").transform.localPosition);
        waypoints = new[] { new Vector3(44.41919f, 18.21625f, 0f), new Vector3(416.1866f, 18.21625f, 0f), new Vector3(787.954f, 39.10205f, 0f), new Vector3(1276.682f, 135.1768f, 0f), new Vector3(1585.792f, 277.2003f, 0f), new Vector3(1431.237f, 582.1331f, 0f), new Vector3(1652.626f, 970.6092f, 0f), new Vector3(1911.61f, 1058.329f, 0f) };
        SetTargetMove();
    }
    void Update()
    {
        //Debug.Log(transform.localPosition);
        

    }
    Vector3[] waypoints;
    // 测试说明： UI的Canvas的模式改成相机渲染时
    // 1、不要使用position这个属性
    // 2、只能使用DOLocalPath这个，而设计路径时又是世界坐标，所以有两种办法
    // 方法1、将路径坐标减去父物体的localposition坐标
    // 方法2，将父物体的中心点坐标移到左下角
    // 补充说明：当UI物体锚点和自身放到最大时，还是可以改变中心点位置，不过需要在面板中修改，不能再视图中移动，已经移不了了
    void SetTargetMove()
    {
        //  设置路径属性
        DOTweenPath path = transform.GetComponent<DOTweenPath>();
        path.duration = 5;
        path.easeType = Ease.Linear;
        path.pathType = PathType.CatmullRom;
        path.isLocal = true;
        //TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOPath(waypoints, path.duration);
        TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOLocalPath(waypoints, path.duration);
        transform.DOPlay();
    }
}
