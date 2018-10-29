using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class EasyTouch5_xDemo : MonoBehaviour
{
    // 不需要 Easytouch 游戏物体
    private void Update()
    {

        Gesture currentGesture = EasyTouch.current; // 单个手势？ 多个人 多个手势呢
        if (currentGesture != null)
        {
            if (EasyTouch.EvtType.On_TouchStart == currentGesture.type)
            {
                OnTouchStart(currentGesture);
            }
            if (EasyTouch.EvtType.On_TouchUp == currentGesture.type)
            {
                OnTouchEnd(currentGesture);
            }
            if (EasyTouch.EvtType.On_Swipe == currentGesture.type)
            {
                OnSwipe(currentGesture);
            }
        }
        
    }

    void OnTouchStart(Gesture gesture)
    {
        //Debug.Log("OnTouchStart" + gesture.startPosition);

    }

    void OnTouchEnd(Gesture gesture)
    {
        //Debug.Log("OnTouchEnd" + gesture.actionTime);
    }

    void OnSwipe(Gesture gesture)
    {
        //Debug.Log("OnSwipe" + gesture.swipeLength);
        Debug.Log("OnSwipe" + gesture.swipe);
    }

}
