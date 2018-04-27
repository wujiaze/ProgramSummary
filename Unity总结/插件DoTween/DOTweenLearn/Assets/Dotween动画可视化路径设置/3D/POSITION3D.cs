using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
public class POSITION3D : MonoBehaviour
{

    void Start()
    {
        waypoints = new[] { new Vector3(-3.907633f, -4.353351f, -8.024292f), new Vector3(2.671068f, -4.353361f, -1.055365f), new Vector3(5.666064f, -4.782058f, 1.734725f), new Vector3(6.415496f, -1.138091f, 5.78044f), new Vector3(4.532152f, 1.782446f, 6.391632f) };
        SetTargetMove();
    }
    void Update()
    {

    }
    Vector3[] waypoints;
    // 测试说明：DOPath是世界坐标系
    // DOLocalPath是相对坐标系
    // 特别说明： path.isLocal 的值不管是什么都没什么影响
    void SetTargetMove()
    {
        DOTweenPath path = transform.GetComponent<DOTweenPath>();
        path.duration = 5;
        path.easeType = Ease.Linear;
        path.pathType = PathType.CatmullRom;
        path.isLocal = true;
        TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOPath(waypoints, path.duration);
        //TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOLocalPath(waypoints, path.duration);
        transform.DOPlay();
    }
}
