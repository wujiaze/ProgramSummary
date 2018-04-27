using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
public class POSITION2D : MonoBehaviour {
    void Start()
    {
       waypoints = new[] { new Vector3(-31.40759f, -0.5586576f, 91.9856f), new Vector3(-31.07926f, -0.5559214f, 91.9856f), new Vector3(-30.70167f, -0.5559214f, 91.9856f), new Vector3(-30.35145f, -0.5559214f, 91.9856f), new Vector3(-30.09151f, -0.5586576f, 91.9856f), new Vector3(-29.74676f, -0.3862813f, 91.9856f), new Vector3(-29.67562f, -0.09898754f, 91.9856f), new Vector3(-29.62637f, 0.08433323f, 91.9856f), new Vector3(-29.62637f, 0.141792f, 91.9856f), new Vector3(-29.92188f, 0.1910424f, 91.9856f) };
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
