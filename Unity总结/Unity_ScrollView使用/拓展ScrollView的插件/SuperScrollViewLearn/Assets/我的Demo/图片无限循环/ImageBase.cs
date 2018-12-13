using System;
using System.Collections;
using System.Collections.Generic;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

public class ImageBase : MonoBehaviour
{

    public Button btn;
    public Image img;
    public Text _txt;
    public string _content { get; private set; }

    internal void Init()
    {
        btn.onClick.AddListener(BtnCall);
    }

    private void BtnCall()
    {
        print("点击触发 "+ _content);
    }

    internal void SetImage(LoopListViewItem2 item, Sprite sprite, string content)
    {
        // 赋值内容
        _content = content;
        _txt.text = _content;
        // 赋值图片，可以设置Size
        img.sprite = sprite;
        float radio = sprite.rect.width / sprite.rect.height;
        img.rectTransform.sizeDelta = new Vector2(100, 100 / radio);
        // 设置当前Item的填充大小
        item.Padding = 10;
    }
}
