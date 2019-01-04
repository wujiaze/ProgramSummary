using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 测试表明 可以将不需要的UI 的 Raycast Target 不勾选，name就不会影响UI的触发*/
public class TestDemo : MonoBehaviour
{

    private Image _needImage;
    private Image _CursorImg1;
    private Image _CursorImg2;
    void Start()
    {
        _needImage = GameObject.Find("Canvas").transform.Find("needImg").GetComponent<Image>();
        _CursorImg1 = GameObject.Find("Canvas").transform.Find("Cursor1").GetComponent<Image>();
        _CursorImg2 = GameObject.Find("Canvas").transform.Find("Cursor1/Cursor2").GetComponent<Image>();

        EventTriggerListener.BindPass(_needImage.gameObject).onEnter = OnEnter;
    }

    private void OnEnter(EventTriggerListener.PointerDataStruct obj)
    {
        print("Enter" + obj.Go.name);
    }

    // Update is called once per frame
    void Update()
    {
        _CursorImg1.rectTransform.anchoredPosition = Input.mousePosition;
    }
}
