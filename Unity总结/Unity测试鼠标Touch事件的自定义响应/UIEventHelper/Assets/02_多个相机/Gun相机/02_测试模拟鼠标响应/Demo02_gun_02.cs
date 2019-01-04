using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo02_gun_02 : MonoBehaviour
{

    private GameObject plane;
    private GameObject Gun;
    private GameObject People;

    private Text Restext;
    private InputField posX;
    private InputField posY;
    void Start()
    {
        Restext = GameObject.Find("Canvas/Restext").GetComponent<Text>();
        posX = GameObject.Find("Canvas/posX").GetComponent<InputField>();
        posY = GameObject.Find("Canvas/posY").GetComponent<InputField>();
        posY.onEndEdit.AddListener(PosCallBack);

        plane = GameObject.Find("Plane");
        Gun = GameObject.Find("Gun");
        People = GameObject.Find("People");
        //EventTriggerListener.Bind(plane.Go).onClick = OnClick;
        //EventTriggerListener.Bind(Gun.Go).onClick = OnClick;
        //EventTriggerListener.Bind(People.Go).onClick = OnClick;
        EventTriggerListener.BindPass(plane.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(Gun.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(People.gameObject).onClick = OnClick;
    }
    private void PosCallBack(string arg0)
    {
        if (posX.text == "" | posY.text == "") return;
        MouseSimulater.LeftClick(float.Parse(posX.text), float.Parse(posY.text));
        Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height + "当前点击：" + posX.text + " / " + posY.text;
    }
    private void OnClick(EventTriggerListener.PointerDataStruct obj)
    {
        if (Application.isEditor)
            print("点击了 : " + obj.Go.name);
        else
            Debug.LogError("点击了 : " + obj.Go.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Screen.SetResolution(800, 600, true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Screen.SetResolution(1920, 1080, true);
        }
        Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height;
    }


}
