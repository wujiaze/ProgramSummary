/*
 *  改变镜头截锥体倾斜度的两种方法
 *  目的：
 *      1、有时平行线有收敛现象，但如果需要减少这个现象，可以采用这个
 *            就像场景中所显示的，当平行线比较长时，正常的旋转相机，会有收敛现象，而采用镜头偏移可以减少这个现象
 *      2、有时赛车需要提供一种速度很快的效果，可以使用这个(镜头的下方尽量靠近地平线)
 *  区别：
 *      1、与旋转相机的区别
 *          虽然视野同样改变，但是 改变镜头的倾斜度，Z轴还是笔直向前，有时候比较方便
 *  方法1：
 *       通过设置 projectionMatrix 属性来改变
 *  方法2：
 *       启用物理相机,改变 Lens Shift 的属性
 *  方法之间的区别：
 *       方法1：只能在运行时看出改变的效果
 *       方法2：可以在编辑时看出效果，推荐
 */
using UnityEngine;

public class ObliqueCamera : MonoBehaviour
{
    public Camera UnityCamera;
    public Camera RealCamera;
    /// <summary>
    /// 横向倾斜角度,一般的取值，当然也是可以超过这个范围的
    /// </summary>
    [Range(-1, 1)]
    public float HorizObl;
    /// <summary>
    /// 纵向倾斜角度,一般的取值，当然也是可以超过这个范围的
    /// </summary>
    [Range(-1, 1)]
    public float VertObl;
    void Start()
    {
        Matrix4x4 mat = UnityCamera.projectionMatrix;
        print("UnityCamera " + mat[0, 2]);
        print("UnityCamera " + mat[1, 2]);
        //提前改变，用来查看
        print("RealCamera " + RealCamera.projectionMatrix[0, 2]);
        print("RealCamera " + RealCamera.projectionMatrix[1, 2]);

        SetObliqueness(0, 0.7f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetObliqueness(HorizObl, VertObl);
        }
    }
    void SetObliqueness(float horizObl, float vertObl)
    {
        Matrix4x4 mat = UnityCamera.projectionMatrix;
        mat[0, 2] = horizObl;
        mat[1, 2] = vertObl;
        UnityCamera.projectionMatrix = mat;
    }
}
