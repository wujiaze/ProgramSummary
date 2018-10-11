using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
public class LOCALPOSITION3D : MonoBehaviour {

    void Start()
    {
        waypoints = new[] { new Vector3(-1.034393f, -1.124037f, -5.388822f), new Vector3(-1.836658f, -2.440542f, -7.413503f), new Vector3(2.371508f, -2.781023f, -3.259555f), new Vector3(5.604157f, -2.531336f, 0.3876657f), new Vector3(6.554788f, 0.01088476f, 3.663331f), new Vector3(4.714474f, 1.804057f, 3.31406f) };
        SetTargetMove();
    }
    void Update()
    {

    }
    Vector3[] waypoints;
    // 测试说明：DOPath是世界坐标系
    // DOLocalPath是相对坐标系:所以设置路径时虽然是世界坐标，但还是可以用作本地坐标来变化
    // 特别说明： path.isLocal 的值不管是什么都没什么影响
    void SetTargetMove()
    {
        DOTweenPath path = transform.GetComponent<DOTweenPath>();
        path.duration = 5;
        path.easeType = Ease.Linear;
        path.pathType = PathType.CatmullRom;
        path.isLocal = false;
        //TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOPath(waypoints, path.duration);
        TweenerCore<Vector3, Path, PathOptions> tweenerCore = transform.DOLocalPath(waypoints, path.duration);
        transform.DOPlay();
    }
}
