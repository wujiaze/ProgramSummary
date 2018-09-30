using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo02_UI_01 : MonoBehaviour
{
    private Image ui1;
    private Image ui2;
    private GameObject cube1;
    private GameObject cube2;
    private GameObject spt1;
    private GameObject spt2;
    private void Awake()
    {
        ui1 = GameObject.Find("Canvas/ui1").GetComponent<Image>();
        ui2 = GameObject.Find("Canvas/ui2").GetComponent<Image>();
        cube1 = GameObject.Find("MainCamera/cube1");
        cube2 = GameObject.Find("MainCamera/cube2");
        spt1 = GameObject.Find("MainCamera/spt1");
        spt2 = GameObject.Find("MainCamera/spt2");
    }
    
    void Start () {
        //EventTriggerListener.Bind(ui1.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(ui2.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(cube1.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(cube2.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(spt1.gameObject).onClick = OnClick;
        //EventTriggerListener.Bind(spt2.gameObject).onClick = OnClick;
        EventTriggerListener.Bind(ui1.gameObject).onEnter = OnEnter;
        EventTriggerListener.BindPass(ui1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(ui2.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(cube1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(cube2.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(spt1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(spt2.gameObject).onClick = OnClick;
    }

    private void OnEnter(EventTriggerListener.PointerDataStruct obj)
    {
        print("enter");
    }

    private void OnClick(EventTriggerListener.PointerDataStruct obj)
    {
        print("点击了： "+obj.gameObject.name);
    }

}
