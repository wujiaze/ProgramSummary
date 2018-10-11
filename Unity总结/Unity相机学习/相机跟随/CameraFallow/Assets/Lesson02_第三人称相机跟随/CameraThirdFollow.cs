using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdFollow : MonoBehaviour
{
    public Transform cameraTrans;//渲染的相机
    private Transform target; //目标物体
    private string tagstr = "target";


    private Vector3 CameraOffestPos; //相机相对于target的位置，一开始自己设置
    private float CameraOffestDis; // 相加到target的距离
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(tagstr).transform;
        if (target==null)
        {
            Debug.LogError("没有目标");
        }
        CameraOffestPos = cameraTrans.position - target.position;
        CameraOffestDis = CameraOffestPos.magnitude;
    }

    // 需要用到射线，所以在FixedUpdate中
    // 用来存储可能的相机点
    Vector3[] CameraProPoints =new Vector3[5];
    private float smoothspeed = 5;
    void FixedUpdate()
    {
        Vector3 standardPos = target.position + CameraOffestPos;//
        Vector2 topPos = target.position + Vector3.up * CameraOffestDis;//
        //
        CameraProPoints[0] = Vector3.Lerp(standardPos, topPos, 0.25f * 0);
        CameraProPoints[1] = Vector3.Lerp(standardPos, topPos, 0.25f * 1);
        CameraProPoints[2] = Vector3.Lerp(standardPos, topPos, 0.25f * 2);
        CameraProPoints[3] = Vector3.Lerp(standardPos, topPos, 0.25f * 3);
        CameraProPoints[4] = Vector3.Lerp(standardPos, topPos, 0.25f * 4);
        for (int i = 0; i < CameraProPoints.Length; i++)
        {
            if (CameraSeeTarget(CameraProPoints[i]))
            {
                cameraTrans.position = Vector3.Lerp(cameraTrans.position, CameraProPoints[i],Time.deltaTime* smoothspeed);
                break;
            }
        }
        // 相机指向
        CameraLookDir();
    }

    private bool CameraSeeTarget(Vector3 CheckPos)
    {
        // 从当前位置向目标发射射线 ,目标需要有碰撞体
        Ray direction = new Ray(CheckPos,target.position - CheckPos);
        RaycastHit info;
        // 返回射线检测到的第一个碰撞体
        //true 说明射线检测到碰撞体
        if (Physics.Raycast(direction, out info, CameraOffestDis))
        {
            if (info.collider.tag != tagstr)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void CameraLookDir()
    {
        Quaternion currentRotation = Quaternion.LookRotation(target.position - cameraTrans.position, Vector3.up);
        cameraTrans.rotation = Quaternion.Lerp(cameraTrans.rotation, currentRotation, Time.deltaTime * smoothspeed);
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
        if (Input.GetKey(KeyCode.Space))
        {
            target.Rotate(new Vector3(0, 1, 0), 10);
        }
    }

}