using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo02_gun_01 : MonoBehaviour
{

    private GameObject plane;
    private GameObject Gun;
    private GameObject People;
    void Start()
    {
        plane = GameObject.Find("Plane");
        Gun = GameObject.Find("Gun");
        People = GameObject.Find("People");
        //EventTriggerListener.Bind(plane.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(Gun.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(People.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(plane.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(Gun.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(People.gameObject).onClick = OnClick;
    }

    private void OnClick(EventTriggerListener.PointerDataStruct obj)
    {
        if (Application.isEditor)
            print("点击了 : " + obj.gameObject.name);
        else
            Debug.LogError("点击了 : " + obj.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
