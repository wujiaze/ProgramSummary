using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    //监听按下
    public void OnPointerDown(PointerEventData eventData)
    {
        //PassEvent(eventdata, ExecuteEvents.pointerDownHandler,false);
    }

    //监听抬起
    public void OnPointerUp(PointerEventData eventData)
    {
        //PassEvent(eventdata, ExecuteEvents.pointerUpHandler);
    }

    //监听点击
    public void OnPointerClick(PointerEventData eventData)
    {
        //PassEvent(eventdata, ExecuteEvents.submitHandler);
        //PassEvent(eventdata, ExecuteEvents.pointerClickHandler,false);
    }


    //把事件透下去
    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function,bool isforward)
        where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;

        //for (int i = 0; i < results.Count; i++)
        //{
        //    if (current != results[i].gameObject)
        //    {
        //        ExecuteEvents.Execute(results[i].gameObject, data, function);
        //        //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
        //    }
        //}
        if (isforward)
        {
            for (int i = 0; i < results.Count; i++)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
                //if (current != results[i].gameObject)
                //{
                //    ExecuteEvents.Execute(results[i].gameObject, data, function);
                //    //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                //}
            }
        }
        else
        {
            for (int i = results.Count - 1; i >= 0; i--)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
                //if (current != results[i].gameObject)
                //{
                //    ExecuteEvents.Execute(results[i].gameObject, data, function);
                //    //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                //}
            }
        }


    }


}