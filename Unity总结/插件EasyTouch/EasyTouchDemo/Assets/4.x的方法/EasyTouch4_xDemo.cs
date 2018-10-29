using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class EasyTouch4_xDemo : MonoBehaviour
{

    private void OnEnable()
    {
        EasyTouch.On_TouchStart += OnTouchStart;
        EasyTouch.On_TouchUp += OnTouchEnd;
        EasyTouch.On_Swipe += OnSwipe;
    }

    private void OnDisable()
    {
        EasyTouch.On_TouchStart -= OnTouchStart;
        EasyTouch.On_TouchUp -= OnTouchEnd;
        EasyTouch.On_Swipe -= OnSwipe;
    }

    private void OnDestroy()
    {
        EasyTouch.On_TouchStart -= OnTouchStart;
        EasyTouch.On_TouchUp -= OnTouchEnd;
        EasyTouch.On_Swipe -= OnSwipe;
    }

    
    void OnTouchStart(Gesture gesture)
    {
        Debug.Log("OnTouchStart" + gesture.startPosition);
        Debug.Log("OnTouchStart  "+ gesture.pickedUIElement.name);
    }

    void OnTouchEnd(Gesture gesture)
    {
        //Debug.Log("OnTouchEnd" + gesture.actionTime);
    }

    void OnSwipe(Gesture gesture)
    {
        //Debug.Log("OnSwipe" + gesture.swipeLength);
        //Debug.Log("OnSwipe" + gesture.swipe);
    }
}
