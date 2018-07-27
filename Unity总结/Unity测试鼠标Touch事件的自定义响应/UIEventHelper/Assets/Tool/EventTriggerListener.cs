/*
 *      EventTriggerListener  工具类使用方法：
 *      适用环境：
 *               1、UGUI   -- UI事件           条件：Canvas组件上添加 GraphicRaycaster 组件
 *               2、2D/3D  -- 射线检测封装事件  条件：1、渲染3D/2D 的Camera,添加 PhysicsRaycaster/PhysicsRaycaster2D 组件，需要去掉UI选项
 *                                                  2、3D/2D 对象，需要添加碰撞体/触发器  
 *      使用方法：
 *            
 *            需要触发 鼠标/touch 事件的对象，使用Bind方法，然后调用触发事件，添加自定义委托
 *
 *      注意点：
 *            一：单一相机：
 *            1.1、若需要优先响应UI事件： 将Canvas对象的Canvas组件的OrderInLayer设置为1(大于所有3D/2D)
 *            产生的效果：1、当UI和2D/3D模型重叠的部分，始终只响应UI
 *                       2、当UI之间重叠的部分，只响应显示在最前面的UI(层级视图的最下面)
 *
 *            1.2、若需要优先响应3D/2D事件：将Canvas对象的Canvas组件的OrderInLayer设置为-1(小于所有3D/2D)
 *                                        2D优先于3D，需要将2D的 SpriteRenderer 的 OrderInLayer设置为大于0(因为3D这个值始终是0)
 *            产生的效果： 1、当UI和2D/3D模型重叠的部分，始终只响应2D/3D模型
 *                        2、当2D/3D之间重叠的部分，响应层级大的模型
 *
 *            1.3、穿透触发效果: 对需要进行穿透的对象，使用 BindPass 方法，触发的顺序，从最上面向下穿透
 *
 *          二：多个相机
 *              当UI和2D/3D ,不同相机时，
 *
 *        提示：eventData 的坐标，都是相对于Unity屏幕的左下角
 *            
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
    public struct PointerDataStruct
    {
        private GameObject gameobject;
        private PointerEventData eventdata;
        private bool isUI;
        private RectTransform recttransform;
        public GameObject gameObject { get { return gameobject; } }
        public PointerEventData eventData { get { return eventdata; } }
        public bool IsUI { get { return isUI; } }
        public RectTransform rectTransform { get { return recttransform; } }

        public PointerDataStruct(GameObject gameobject, PointerEventData eventdata)
        {
            this.gameobject = gameobject;
            this.eventdata = eventdata;
            this.recttransform = gameobject.GetComponent<RectTransform>();
            this.isUI = this.recttransform != null ? true : false;
        }

        public void Move3D2DonDistance(Camera camera, float distance = -1f)
        {
            if (Mathf.Approximately(distance, -1f))
                distance = Mathf.Abs(camera.transform.position.z - gameObject.transform.position.z);
            Vector3 pos = camera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, distance));
            gameObject.transform.position = pos;
        }
    }
    public struct BaseDataStruct
    {
        private GameObject gameobject;
        private BaseEventData eventdata;
        private bool isUI;
        private RectTransform recttransform;
        public GameObject gameObject { get { return gameobject; } }
        public BaseEventData eventData { get { return eventdata; } }
        public bool IsUI { get { return isUI; } }
        public RectTransform rectTransform { get { return recttransform; } }
        public BaseDataStruct(GameObject gameobject, BaseEventData eventdata)
        {
            this.gameobject = gameobject;
            this.eventdata = eventdata;
            this.recttransform = gameobject.GetComponent<RectTransform>();
            this.isUI = this.recttransform != null ? true : false;
        }
    }

    public Action<PointerDataStruct> onClick;
    public Action<PointerDataStruct> onEnter;
    public Action<PointerDataStruct> onExit;
    public Action<PointerDataStruct> onDown;
    public Action<PointerDataStruct> onUp;
    public Action<PointerDataStruct> onDrag;

    public Action<BaseDataStruct> onSelect;
    public Action<BaseDataStruct> onUpdateSelect;

    private bool isPass;

    /// <summary>
    /// 给游戏对象添上 “监听器”
    /// </summary>
    /// <param name="go">监听的游戏对象</param>
    public static EventTriggerListener Bind(GameObject go)
    {
        EventTriggerListener triggerListener = go.GetComponent<EventTriggerListener>();
        if (triggerListener == null)
            triggerListener = go.AddComponent<EventTriggerListener>();
        triggerListener.isPass = false;
        return triggerListener;
    }
    /// <summary>
    /// 给游戏对象添上 穿透 “监听器”
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static EventTriggerListener BindPass(GameObject go)
    {
        EventTriggerListener triggerListener = go.GetComponent<EventTriggerListener>();
        if (triggerListener == null)
            triggerListener = go.AddComponent<EventTriggerListener>();
        triggerListener.isPass = true;
        return triggerListener;
    }

    /// <summary>
    /// 穿透方法
    /// </summary>
    public void PassEvent<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        GameObject currentGo = eventData.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            if (EventCache.Instance.ResponseGoList.Count == results.Count)
                break;
            if (EventCache.Instance.ResponseGoList.Contains(results[i].gameObject))
                continue;
            EventCache.Instance.ResponseGoList.Add(results[i].gameObject);
            if (currentGo != results[i].gameObject)
                ExecuteEvents.Execute(results[i].gameObject, eventData, function);
        }
        if (currentGo!=null && this.name == currentGo.name)
            EventCache.Instance.ResponseGoList.Clear();
    }


    // 点击事件
    public override void OnPointerClick(PointerEventData eventData)
    {
        DoPointerHandle(onClick, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }
    // 进入事件
    public override void OnPointerEnter(PointerEventData eventData)
    {
        DoPointerHandle(onEnter, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.pointerEnterHandler);
    }
    // 离开事件
    public override void OnPointerExit(PointerEventData eventData)
    {
        DoPointerHandle(onExit, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.pointerExitHandler);
    }
    // 按下事件
    public override void OnPointerDown(PointerEventData eventData)
    {
        DoPointerHandle(onDown, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
    }
    // 弹起事件
    public override void OnPointerUp(PointerEventData eventData)
    {
        DoPointerHandle(onUp, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
    }

    /// <summary>
    /// 拖动事件
    ///     一般用于UI事件，UI对象需要以左下角为原点(锚点和中心点都可以)
    ///     若要获取2D/3D的坐标，则需要根据相机，转换坐标,见 PointerDataStruct构造函数
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDrag(PointerEventData eventData)
    {
        DoPointerHandle(onDrag, eventData);
        if (isPass)
            PassEvent(eventData, ExecuteEvents.dragHandler);
    }

    // 选择事件
    public override void OnSelect(BaseEventData eventData)
    {
        DoBaseHandle(onSelect, eventData);
       
    }

    // 更新选泽事件
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        DoBaseHandle(onUpdateSelect, eventData);
    }


    /* 辅助方法 */
    private void DoPointerHandle(Action<PointerDataStruct> onAction, PointerEventData eventData)
    {
        if (onAction == null)
            return;
        PointerDataStruct temp = new PointerDataStruct(gameObject, eventData);
        onAction(temp);
    }
    private void DoBaseHandle(Action<BaseDataStruct> onAction, BaseEventData eventData)
    {
        if (onAction == null)
            return;
        BaseDataStruct temp = new BaseDataStruct(gameObject, eventData);
        onAction(temp);
    }

}
public class EventCache
{
    private static EventCache _instance;
    private static readonly object _lockobj;

    public List<GameObject> ResponseGoList;
    static EventCache()
    {
        _lockobj = new object();
    }

    private EventCache()
    {
        ResponseGoList = new List<GameObject>();
    }

    public static EventCache Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lockobj)
                {
                    if (_instance == null)
                    {
                        _instance = new EventCache();
                    }
                }
            }
            return _instance;
        }
    }

    
}