/*
 *    MenuItem:  在主菜单 或者 inspector 面板中添加 菜单/选项
 *      参数：
 *          itemName             选项名字
 *          isValidateFunction   是否启用验证方法，默认是false   
 *          priority             默认是1000，可以是负。优先级规则：同一个层级下（根据名字分层级），priority小，菜单显示在上方，priority相等，则根据方法在代码中的顺序进行排序                 
 *
 *      作用：将静态方法转换成菜单命令，与静态方法的修饰类型无关
 *
 *      用法：
 *      一、主菜单
 *              1、新建一个菜单
 *              2、添加一个选项
 *              3、在 Hierarchy 面板中添加选项
 *              4、在 Project   面板中添加选项
 *          
 *      二、Inspector面板
 *              详见 file://Editor/MenuItem/MenuCommandTool
 *
 *      小提示
 *           1、选项之间插入间隔符    规则：连续两个方法之间的 priority 相差 大于 10。
 *           2、快捷键(hotkey)
 *                 在菜单的路径名 + 空格键 + 下划线 + 键名(必须小写)
 *                      特殊字符 % (Windows上的ctrl, macOS上的cmd)， # (shift)， & (alt)
 *                      会自动检测快捷键是否有冲突
 *           3、验证方法
 *                  条件：1、itemName 需要和 调用方法的itemName 一致
 *                        2、isValidateFunction 为true
 *                        3、priority 无所谓
 *                        4、返回 bool 值，true：激活调用方法， false：不激活调用方法
 * todo 创建游戏对象
 * todo 编辑窗口
 */
using UnityEditor;
using UnityEngine;

public class MenuItemTool
{
    #region 主菜单
    // 自己建立一个新的菜单
    [MenuItem("MyMenuItem/Menu/Test")]
    private static void Test()
    {
        Debug.Log("Test");
    }
    // 在已有 的菜单中，添加自己的选项
    [MenuItem("Window/MyTest")]
    private static void Test2()
    {
        Debug.Log("WindowTest");
    }
    /*---------------设置选项的顺序-----------------*/
    [MenuItem("MyMenuOrder/Order1", false, 2)]
    private static void OrderTest1()
    {
        Debug.Log("OrderTest1");
    }
    [MenuItem("MyMenuOrder/Order2", false, 1)]
    private static void OrderTest2()
    {
        Debug.Log("OrderTest3");
    }

    /*-----------添加选项的间隔符------------------*/
    [MenuItem("ClassifyMenu/ClassifyTest1", false, 1)]
    private static void ClassifyTest1()
    {
        Debug.Log("ClassifyTest1");
    }
    [MenuItem("ClassifyMenu/ClassifyTest2", false, 11)]
    private static void ClassifyTest2()
    {
        Debug.Log("ClassifyTest2");
    }
    /*-----------Hierarchy 面板右键，添加自己的选项，实际就是添加选项到 GameObject 菜单中------------------*/
    // 根据测试：想放在最前面就设置为0，然后根据方法的顺序进行排序，放在最后面从 11 开始设置，然后根据方法的顺序进行排序
    [MenuItem("GameObject/HierarchyTest1", false, 0)]
    private static void HierarchyTest1()
    {
        Debug.Log("HierarchyTest1");
    }
    [MenuItem("GameObject/HierarchyTest2", false, 0)]
    private static void HierarchyTest2()
    {
        Debug.Log("HierarchyTest2");
    }
    [MenuItem("GameObject/HierarchyTest11", false, 11)]
    private static void HierarchyTest11()
    {
        Debug.Log("HierarchyTest1");
    }
    [MenuItem("GameObject/HierarchyTest12", false, 11)]
    private static void HierarchyTest12()
    {
        Debug.Log("HierarchyTest12");
    }
    /*-----------Project 面板右键，添加自己的选项，实际就是添加选项到 Assets 菜单中------------------*/
    // 这个菜单的顺序比较复杂，推荐将自己的方法放置在 0
    [MenuItem("Assets/ProjectTest1", false, 0)]
    private static void ProjectTest1()
    {
        Debug.Log("ProjectTest1");
    }


    #endregion

    #region 验证某功能是否可用
    [MenuItem("MyValidate/Delete", false,10)]
    private static void Delete()
    {
        foreach (GameObject go in Selection.gameObjects)   
        {
            Undo.DestroyObjectImmediate(go);
        }
    }
    [MenuItem("MyValidate/Delete", true,1000)]
    private static bool DeleteValidate()
    {
        if (Selection.gameObjects.Length > 0)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region 快捷键
    [MenuItem("MyHotKey/Normal _g")]
    private static void NormalHotKey()
    {
        Debug.Log("普通的快捷键");
    }
    [MenuItem("MyHotKey/Mul _%g")]
    private static void MulHotKey()
    {
        Debug.Log("普通的组合快捷键");
    }

    #endregion

}
