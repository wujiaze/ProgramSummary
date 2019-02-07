/*
 *
 *   ContextMenu
 *      作用：在脚本的右键菜单添加按钮
 *      对象：非静态方法
 *      参数:
 *          itemName             选项名字
 *          isValidateFunction   是否启用验证方法,默认是 false
 *          priority             优先级，默认是 1000000
 *      用法：
 *          跟 MenuItem 类似
 *      区别：
 *          MenuItem    用于 Editor 文件夹，是静态方法
 *          ContextMenu 用于 一般的脚本   ，是实例方法
 *
 *  ContextMenuItem
 *      作用：在字段上添加一个右键按钮
 *      对象：仅对字段有效
 *      参数：
 *          name：       按钮的名字
 *          function：   调用方法的名字
 */

using UnityEditor;
using UnityEngine;

public class ContextMenuTool : MonoBehaviour
{
    [ContextMenuItem("SetX按钮名称", "SetX")]
    public float X =10;


    [ContextMenu("setX",false)]
    private void SetX()
    {
        X *= 2;
    }
    [ContextMenu("setX",true)]
    private bool IsSetx()
    {
        if (Selection.activeGameObject!=null)
        {
            return true;
        }
        return false;
    }
}
