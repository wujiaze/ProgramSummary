using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DotweenCreateTween : MonoBehaviour
{
    public Camera camera;
    public Light light;
    public Material material;
    public Rigidbody rigidbody;
    public Rigidbody jump;
    public Rigidbody rotate;
    public Rigidbody lookat;
    public Rigidbody spiral;
    private void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.Default).SetCapacity(200, 50);

    }

    private void Start()
    {
        /*
         *   创建Tween，Tween 分为两类 To 和 From
         *      To:   从当前值 取向 目标值       正常使用都是To方法
         *      From：从目标值 取向 当前值       在正常方法的后面添加From,From(isRelative):参数表示是否为相对值
         *      
         *      方法一：通用方法：DOTween.To( 参数：获取值的Lambda，参数：设置值得Lambda，参数：目标值，参数：持续时间)
         *
         *      方法二：快捷方法
         *
         *      方法三：
         *      方法二和方法三，在内部最终都是转换成方法一
         */

        /* 方法一：以移动X轴为例 */
        // 采用委托方式
        DOTween.To(Getter, Setter, 10f, 2);
        // 采用Lambda表达式
        DOTween.To(() => { return transform.position.x; },
            x => { transform.position = new Vector3(x, transform.position.y, transform.position.z); },
            10f,
            2);
        // 简化Lambda表达式
        DOTween.To(() => transform.position.x,
            value => transform.position = new Vector3(value, transform.position.y, transform.position.z),
            10f,
            2);
        // From
        DOTween.To(Getter, Setter, 10f, 2).From(true);

        /* 方法二：快捷方法 */
        transform.DOMoveX(10, 1);                           // 将 10作为endvalue，当前位置是初始位置
        transform.DOMoveX(transform.position.x + 10, 1);    // 将相对位置（当前位置+10）作为endvalue，当前位置是初始位置
        transform.DOMoveX(10, 1).SetRelative(true);         // 链式设置：与第二种一致
        // From todo

        // 简单的或不常用的官网看
        // 这里解释一些复杂的
        /*
         *  camera.DOShakePosition
         *      strength：震动范围幅度，float 在XY平面 Vector3在任意面，值越大，震动范围越大
         *      vibrato： 震动颤抖幅度，值越大，抖动越大
         *      randomness：震动的随机幅度，0~180 ，值越大，随机度越大
         *      fadeout ：是否渐弱，true：时间快到了，会逐渐变弱。false：时间到，直接停止
         *  camera.DOShakeRotation
         *      参数与上一个方法类似
         */
        //camera.DOShakePosition(5, new Vector3(2, 2, 0), 10, 90, true);
        //camera.DOShakeRotation(5);
        /*
         *  混合Tween Blendable tweens
         *
         *  light灯光例子
         *      DOColor:是单一的Tween，当有多个 DOColor 时，以最后一个为准
         *      DOBlendableColor：是混合Tween，多个 DOBlendableColor 时，颜色最后是混合的结果
         *  注意：当 DOColor 和 DOBlendableColor 一起使用时，以 DOColor 为准，它会覆盖 DOBlendableColor
         *
         */
        //light.DOColor(Color.black, 2);
        //light.DOColor(Color.blue, 2);
        light.DOBlendableColor(Color.red, 2);
        light.DOBlendableColor(Color.blue, 2);

        /*
         *  Material 材质球的DOTween：本质上是对Shader参数的更改
         *
         *  material.DOColor
         *      Color：颜色
         *      Property:颜色变量的名称（当shader只有一个Color时，可以不填；当有多个颜色时，需要指定改变的颜色变量）
         *      duration：持续时间
         */
        material.DOColor(Color.green, 2);
        material.DOColor(Color.red, "_Color", 2);
        // offest tilling todo

        /*
         *  rigidbody （所有的都推荐采用Transform）
         *      DOMove:效果和Transform一致，有刚体的可以使用（不推荐，采用Transform）
         *      endvalue: 最后的位置
         *      snapping: false：顺滑的移动；true：每次移动都是整数（显示的样子就是一格一格的跳动）
         *
         *      DOJump
         *      endvalue：最后的位置
         *      jumpPower：跳起的高度
         *      numJumps: 跳起的次数（每次跳的力量是一样的）
         *      duration：持续时间
         *
         *      DORotate：（不推荐，旋转还是采用Transform）
         *          envalue:目标值
         *          duration：持续时间
         *          RotateMode: Fast           表示每次旋转的角度都是选择锐角的方向 ，比如 270 -- 90  ，不推荐
         *                      FastBeyond360  表示每次旋转超过360之后是最快的，     比如 365 -- 5   ，不推荐
         *                      WorldAxisAdd   旋转的坐标轴是世界坐标系
         *                      LocalAxisAdd   旋转的坐标轴是本地坐标系
         *
         *      DOSpiral:做螺旋运动
         *          duration：持续时间
         *          axis    ：旋转轴
         *          SpiralMode(螺旋模式):Expand             扩展
         *                              ExpandThenContract  扩展然后收缩
         *          speed   : 速度，速度越大，螺旋的半径越大
         *          frequency:频率，频率越小，螺旋的半径越大
         *          depth   ：纵深，根据旋转轴的方向，前进纵深值
         *
         */
        rigidbody.DOMove(Vector3.one * 3, 2);
        rigidbody.DOMoveZ(5, 2, true);  //在Z轴上移动
        jump.DOJump(new Vector3(0, 0, 0), 10, 10, 3);
        rotate.DORotate(new Vector3(0, 45, 0), 2, RotateMode.LocalAxisAdd);
        lookat.DOLookAt(Vector3.one, 2, AxisConstraint.None, Vector3.up); // todo ???
        spiral.DOSpiral(2, Vector3.up, SpiralMode.ExpandThenContract, 1, 1, 10);
    }
    private void Setter(float pnewvalue)
    {
        transform.position = new Vector3(pnewvalue, transform.position.y, transform.position.z);
    }
    private float Getter()
    {
        return transform.position.x;
    }

    private void Update()
    {
        print(camera.aspect);
        //print(camera.pixelRect);
        print(camera.rect);
    }


}
