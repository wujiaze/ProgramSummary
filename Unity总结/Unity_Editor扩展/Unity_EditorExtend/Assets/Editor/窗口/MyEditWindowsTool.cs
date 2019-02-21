/*
 *      EditWindows，自定义提示框，适用于长期存在，就像Game，Scene，Inspector之类
 *      属性
 *          静态属性
 *              focusedWindow
 *              mouseOverWindow
 *          一般属性
 *              autoRepaintOnSceneChange
 *              maximized
 *              maxSize
 *              minSize
 *              position
 *              rootVisualElement
 *              titleContent
 *              wantsMouseEnterLeaveWindow
 *              wantsMouseMove
 *      方法
 *          静态方法
 *              FocusWindowIfItsOpen
 *              GetWindow
 *              GetWindowWithRect
 *          一般方法
 *              BeginWindows
 *              Close
 *              EndWindows
 *              Focus
 *              RemoveNotification  立即关闭自定义通知
 *              Repaint
 *              SendEvent
 *              Show
 *              ShowAsDropDown
 *              ShowAuxWindow
 *              ShowModalUtility
 *              ShowNotification    显示自定义通知，持续若干秒，自动消失
 *              ShowPopup
 *              ShowUtility
 *      消息回调
 *              OnFocus
 *              OnGUI
 *              OnHierarchyChange
 *              OnInspectorUpdate
 *              OnLostFocus
 *              OnProjectChange
 *              OnSelectionChange
 *              Awake
 *              OnEnable
 *              OnDisable
 *              Update
 *              OnDestroy
 */

using UnityEditor;
using UnityEngine;

public class MyEditWindowsTool : EditorWindow
{
    // 创建一个提示面板
    [MenuItem("Window/ShowMyEditWindow")]
    private static void CreateEditWindow()
    {
        MyEditWindowsTool myWin = EditorWindow.GetWindow<MyEditWindowsTool>();
        myWin.Show();
        //"创建窗口的标题", "Create 和 关闭 按钮", "Other按钮"
    }

    public float Value = 10;
    private string objNmae = "123";
    private void OnGUI()
    {
        EditorGUILayout.LabelField("这是我的窗口");
        Value = EditorGUILayout.FloatField("Text Field", Value);
        objNmae = EditorGUILayout.TextField("创建游戏物体的名字",objNmae);
        if (GUILayout.Button("创建游戏物体"))
        {
            GameObject go = new GameObject(objNmae);
            Undo.RegisterCreatedObjectUndo(go, "Create" + objNmae); // 编辑器的操作尽量都添加到Undo中，可以撤销
        }
    }

}
