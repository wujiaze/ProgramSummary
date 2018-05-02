using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class POSITIONTEST : MonoBehaviour {

    void Start()
    {
        waypoints = new[] { new Vector3(-3.427917f, 11.61774f, 0f), new Vector3(458.8504f, 40.06561f, 0f), new Vector3(1034.92f, 72.06949f, 0f), new Vector3(1426.079f, 217.865f, 0f), new Vector3(1607.434f, 509.456f, 0f), new Vector3(1717.67f, 825.9388f, 0f), new Vector3(1916.805f, 1071.302f, 0f) };
        SetTargetMove();
    }
    void Update()
    {
        
    }
    Vector3[] waypoints;
    // 测试说明：DotweenPath的UI对象，不要以Canvas为父对象
    void SetTargetMove()
    {
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
