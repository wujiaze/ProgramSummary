using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo02_UI_02 : MonoBehaviour
{

    private Image ui1;
    private Image ui2;
    private GameObject cube1;
    private GameObject cube2;
    private GameObject spt1;
    private GameObject spt2;
    //
    private Text Restext;
    private InputField posX;
    private InputField posY;
    private void Awake()
    {
        ui1 = GameObject.Find("Canvas/ui1").GetComponent<Image>();
        ui2 = GameObject.Find("Canvas/ui2").GetComponent<Image>();
        cube1 = GameObject.Find("MainCamera/cube1");
        cube2 = GameObject.Find("MainCamera/cube2");
        spt1 = GameObject.Find("MainCamera/spt1");
        spt2 = GameObject.Find("MainCamera/spt2");
        //
        Restext = GameObject.Find("Canvas/Restext").GetComponent<Text>();
        posX = GameObject.Find("Canvas/posX").GetComponent<InputField>();
        posY = GameObject.Find("Canvas/posY").GetComponent<InputField>();
        posY.onEndEdit.AddListener(PosCallBack);
    }

    void Start()
    {
        //EventTriggerListener.Bind(ui1.Go).onClick = OnClick;
        //EventTriggerListener.Bind(ui2.Go).onClick = OnClick;
        //EventTriggerListener.Bind(cube1.Go).onClick = OnClick;
        //EventTriggerListener.Bind(cube2.Go).onClick = OnClick;
        //EventTriggerListener.Bind(spt1.Go).onClick = OnClick;
        //EventTriggerListener.Bind(spt2.Go).onClick = OnClick;


        EventTriggerListener.BindPass(ui1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(ui2.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(cube1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(cube2.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(spt1.gameObject).onClick = OnClick;
        EventTriggerListener.BindPass(spt2.gameObject).onClick = OnClick;
    }

    private void OnClick(EventTriggerListener.PointerDataStruct obj)
    {
        if (Application.isEditor)
            print("点击了： " + obj.Go.name);
        else
            Debug.LogError("点击了： " + obj.Go.name);
    }
    private void PosCallBack(string arg0)
    {
        if (posX.text == "" | posY.text == "") return;
        MouseSimulater.LeftClick(float.Parse(posX.text), float.Parse(posY.text));
        Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height + "当前点击：" + posX.text + " / " + posY.text;
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
