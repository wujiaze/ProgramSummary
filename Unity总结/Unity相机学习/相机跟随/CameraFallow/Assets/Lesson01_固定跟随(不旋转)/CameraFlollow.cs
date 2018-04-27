using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlollow : MonoBehaviour {

    public Transform cameraTrans;//渲染的相机
    public Transform target;//目标物体
    private Vector3 offest; // 目标和相机的偏移量
    void Start()
    {
        offest = target.position - cameraTrans.position;
    }
    void LateUpdate()
    {
        //当前摄像机的位置 = 当前物体的位置 - 偏移量
        Vector3 targetPos = target.position - offest;
        cameraTrans.transform.position = targetPos;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            target.position = target.position + new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            target.position = target.position + new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            target.position = target.position + new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            target.position = target.position + new Vector3(1, 0, 0);
        }
    }
}
