using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchMoveUI : MonoBehaviour {

    // 第一
    // Canvas 的 Render Mode 改成 Camera 
    // Canvas 的 Canvas Scaler 的UIScale Mode改成Scale With Screen Size，Reference Resolution跟 Game视图的分辨率 保持一致
    // 在Hierarchy面板创建LeanTouch   属性Record Limit：表示最多同时识别的touch数
    // 第二：这里需求：按住移动
    // 在LeanTouch对象上添加 Lean Finger Down 和 Lean Select 组件
    // Lean Select 组件 的Select Using 模式改为 Canvas UI, Deselect On Up 勾选上，其他默认
    // Lean Fineger Down 组件的OnFingerDown事件添加Lean Select组件中的LeanSelect.SelectScreenPosition方法
    // 第三
    // 选择需要移动的对象，添加 Lean Translate Smooth组件
    // 选择触摸的对象，添加 Lean Selectable组件
    // 触摸的对象和移动的对象一般是同一个，但也可以是不同的对象
    // 触摸对象上的Lean Selectable组件可以决定移动哪些物体，在OnSelect和OnDeselect添加移动对象那个身上的Lean Translate Smooth组件，不需要方法
    // 移动对象上的Lean Selectable组件还可以添加自己的脚本事件
    // 注意：补充说明
    // 若对象只添加Lean Translate Smooth组件，而没有其他Lean Selectable组件来控制这个对象，那么触摸屏幕上任意一点，都会移动这个物体，相对运动
    public void TouchOnSelf() {
        Debug.Log("按住了对象，触发事件");
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
    }
    public void UpSelf() {
        Debug.Log("从对象身上移开，触发事件");
        Debug.Log(transform.position);
    }
    public void DeSelectSelf() {
        Debug.Log("将对象从选择列表中删除了，触发事件");
        Debug.Log(transform.position);
    }
}
