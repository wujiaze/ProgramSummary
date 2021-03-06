﻿/*
 *      文字垂直滚动(ScrollView组件方法)
 *          添加 ScrollView，注意 ViewPort 和 Content 的pivot在父物体的左上角
 *          步骤1：删除不需要的滑动条
 *          步骤2：将 ScrollRect 的 ViewPort 设为None,将 ViewPort 设置为撑满 ScrollRect，再重新拖入ScrollRect
 *          步骤3：在 Content 中添加Text组件,选定 FontSize，不能选择 Best Fit，然后添加内容
 *          步骤4：Content添加 VerticalLayoutGroup，勾选 ControlWidth ControlHeight ,其余不用勾选
 *          步骤5：Content添加 ContentSizeFitter, VerticalFit 选择 PreferredFit
 *          之后有两种方法：
 *          方法1：content的宽度手动设置为viewport的宽度,PosY设置为0(推荐)
 *          方法2：ViewPort添加 VerticalLayoutGroup，这样就保证了PosY为0(只能从0开始)，只勾选 ControlWidth和ForceExpandWidth,保证了content的宽度为Viewport的宽度，以及不会影响子物体中的ContentSizeFitter在Y轴上的设置(取巧)
 */
using UnityEngine;
using UnityEngine.UI;

public class TxtRoll : MonoBehaviour
{
    public ScrollRect rect;
    public RectTransform viewport;
    public RectTransform content;
    public Text txt;
    void Start()
    {
        txt.text =
            "ashdfashdlfijsadlfjasdjfiasjd asdjf oasiodasjf iojasldkifh ailohawlokisd hoiasjd oiasdki fajhsdf haskildhfaisdjhfpoawejr0-wj ofajhdsoif jhaeo fjhaoeihflkdashfi oa";
        // 强制更新Layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        // 设置 ScrollRect 的高度
        float height = Mathf.Clamp(content.GetComponent<VerticalLayoutGroup>().preferredHeight, 0, 200);
        rect.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        // 设置 content 的位置
        rect.verticalNormalizedPosition = 1;
    }

    private void Update()
    {
        // 正常使用
        print(rect.normalizedPosition);
    }

}
