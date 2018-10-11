using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera01_2 : MonoBehaviour {

    public Transform cameraTrans;//渲染的相机
    public Transform target;//目标物体
    private Vector3 offest = new Vector3(7,4,-7); // 目标和相机的偏移量(最终需要达到的)(这里方向是目标指向相机)
    private float speed=2;
    /// <summary>
    /// 整体的效果是：从摄像机当前的位置，逐渐移动到相对目标+自定义偏移量的位置，并且相机始终指向目标
    /// 在整个移动过程中，使用差值计算，辅助时间，就看上去很平滑了
    /// </summary>
    void LateUpdate()
    { 
        cameraTrans.position = Vector3.Lerp(cameraTrans.position, target.position+ offest, speed * Time.deltaTime);//调整相机与玩家之间的距离
        Quaternion angel = Quaternion.LookRotation(target.position - cameraTrans.position);//获取旋转角度
        cameraTrans.rotation = Quaternion.Slerp(cameraTrans.rotation, angel, speed * Time.deltaTime);
    }
}
