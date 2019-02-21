/*
 *      MenuCommand
 *          context     上下文，一般是当前选择对象或鼠标下方的对象
 *          userData    todo 用户数据，用于传递用户信息的整型数据 如何使用
 *
 *      Inspector面板
 *              方法1、给脚本右键菜单添加选项   需要使用 CONTEXT/某组件/xxx 格式,组件名字不能错，选择了某一个组件，那么只有在这个组件右键菜单中，才有这个选项
 *              方法2、详情见 file://
 */
using UnityEditor;
using UnityEngine;

public class MenuCommandTool
{
    #region Inspector面板
    // 给系统组件 添加右键选项
    [MenuItem("CONTEXT/BoxCollider/MyCollider")] 
    private static void UpdateCollider(MenuCommand cmd)//MenuCommand 可加可不加，加了之后可以 获取/修改 当前选择对象的数据
    {
        BoxCollider box = cmd.context as BoxCollider;
        if (box == null)
        {
            Debug.LogError("BoxCollider null");
            return;
        }
        box.size = new Vector3(10, 10);
    }
    // 给自己的脚本组件 添加右键选项
    [MenuItem("CONTEXT/MyScripts/MyItem")]
    private static void UpdateSelfScripts(MenuCommand cmd)
    {
        MyContextMenuTool box = cmd.context as MyContextMenuTool;
        if (box == null)
        {
            Debug.LogError("MyScripts null");
            return;
        }
        box.X *= 5;
    }

    #endregion
}
