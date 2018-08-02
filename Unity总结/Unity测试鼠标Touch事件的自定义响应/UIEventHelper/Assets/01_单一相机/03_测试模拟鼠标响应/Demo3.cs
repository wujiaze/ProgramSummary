
using System;
using UnityEngine;
using UnityEngine.UI;

public class Demo3 : MonoBehaviour
{

    private Image ui1;
    private Image ui2;
    private GameObject cube;
    private GameObject sprite;


    private Text Restext;
    private InputField posX;
    private InputField posY;

    private void Start()
    {

        Restext = GameObject.Find("Canvas/Restext").GetComponent<Text>();
        posX = GameObject.Find("Canvas/posX").GetComponent<InputField>();
        posY = GameObject.Find("Canvas/posY").GetComponent<InputField>();
        ui1 = GameObject.Find("Canvas/ui1").GetComponent<Image>();
        ui2 = GameObject.Find("Canvas/ui2").GetComponent<Image>();
        cube = GameObject.Find("Canvas/cube");
        sprite = GameObject.Find("Canvas/sprite");
        posY.onEndEdit.AddListener(PosCallBack);

        //// 绑定自定义委托
        //EventTriggerListener.Bind(ui1.gameObject).onClick = OnClickHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onClick = OnClickHandler;
        //EventTriggerListener.Bind(cube.gameObject).onClick = OnClickHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onClick = OnClickHandler;
        //// 拖动
        //EventTriggerListener.Bind(ui1.gameObject).onDrag = OnDragHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onDrag = OnDragHandler;
        //EventTriggerListener.Bind(cube.gameObject).onDrag = OnDragHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onDrag = OnDragHandler;
        //// 进入
        //EventTriggerListener.Bind(ui1.gameObject).onEnter = OnEnterHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onEnter = OnEnterHandler;
        //EventTriggerListener.Bind(cube.gameObject).onEnter = OnEnterHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onEnter = OnEnterHandler;
        //// 离开
        //EventTriggerListener.Bind(ui1.gameObject).onExit = OnExitHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onExit = OnExitHandler;
        //EventTriggerListener.Bind(cube.gameObject).onExit = OnExitHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onExit = OnExitHandler;
        //// 按下
        //EventTriggerListener.Bind(ui1.gameObject).onDown = OnDownHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onDown = OnDownHandler;
        //EventTriggerListener.Bind(cube.gameObject).onDown = OnDownHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onDown = OnDownHandler;
        //// 弹起
        //EventTriggerListener.Bind(ui1.gameObject).onUp = OnUpHandler;
        //EventTriggerListener.Bind(ui2.gameObject).onUp = OnUpHandler;
        //EventTriggerListener.Bind(cube.gameObject).onUp = OnUpHandler;
        //EventTriggerListener.Bind(sprite.gameObject).onUp = OnUpHandler;
        //// 绑定自定义委托
        // 点击
        EventTriggerListener.BindPass(ui1.gameObject).onClick = OnClickHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onClick = OnClickHandler;
        EventTriggerListener.BindPass(cube.gameObject).onClick = OnClickHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onClick = OnClickHandler;
        // 拖动
        EventTriggerListener.BindPass(ui1.gameObject).onDrag = OnDragHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onDrag = OnDragHandler;
        EventTriggerListener.BindPass(cube.gameObject).onDrag = OnDragHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onDrag = OnDragHandler;
        // 进入
        EventTriggerListener.BindPass(ui1.gameObject).onEnter = OnEnterHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onEnter = OnEnterHandler;
        EventTriggerListener.BindPass(cube.gameObject).onEnter = OnEnterHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onEnter = OnEnterHandler;
        // 离开
        EventTriggerListener.BindPass(ui1.gameObject).onExit = OnExitHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onExit = OnExitHandler;
        EventTriggerListener.BindPass(cube.gameObject).onExit = OnExitHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onExit = OnExitHandler;
        // 按下
        EventTriggerListener.BindPass(ui1.gameObject).onDown = OnDownHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onDown = OnDownHandler;
        EventTriggerListener.BindPass(cube.gameObject).onDown = OnDownHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onDown = OnDownHandler;
        // 弹起
        EventTriggerListener.BindPass(ui1.gameObject).onUp = OnUpHandler;
        EventTriggerListener.BindPass(ui2.gameObject).onUp = OnUpHandler;
        EventTriggerListener.BindPass(cube.gameObject).onUp = OnUpHandler;
        EventTriggerListener.BindPass(sprite.gameObject).onUp = OnUpHandler;
    }
    private void PosCallBack(string arg0)
    {
        if (posX.text == "" | posY.text == "") return;
        MouseSimulater.LeftClick(float.Parse(posX.text), float.Parse(posY.text));
        Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height + "当前点击：" + posX.text + " / " + posY.text;
    }

    private void OnClickHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.LogError("点击了： " + obj.gameObject.name);
        Debug.LogError("点击的位置： " + obj.eventData.position);
    }
    private void OnDragHandler(EventTriggerListener.PointerDataStruct obj)
    {
        print(obj.eventData.position);
        if (obj.IsUI)
            obj.rectTransform.anchoredPosition = obj.eventData.position;
        else
            obj.Move3D2DonDistance(Camera.main);// 屏幕的 X,Y 坐标，以及物体相对相机的距离
    }
    private void OnEnterHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.Log("进入了： " + obj.gameObject.name);
    }
    private void OnExitHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.Log("离开了： " + obj.gameObject.name);
    }
    private void OnDownHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.Log("按下了： " + obj.gameObject.name);
    }
    private void OnUpHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.Log("弹起了： " + obj.gameObject.name);
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
