using UnityEngine;
using UnityEngine.UI;

public class Demo2 : MonoBehaviour
{

    private Image ui1;
    private Image ui2;
    private GameObject cube;
    private GameObject sprite;
    private Image img;
    private void Start()
    {
        ui1 = GameObject.Find("Canvas/ui1").GetComponent<Image>();
        ui2 = GameObject.Find("Canvas/ui2").GetComponent<Image>();
        cube = GameObject.Find("cube");
        sprite = GameObject.Find("sprite");
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


    private void OnClickHandler(EventTriggerListener.PointerDataStruct obj)
    {
        Debug.Log("点击了： " + obj.gameObject.name);
        Debug.Log("点击的位置： " + obj.eventData.position);
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
}
