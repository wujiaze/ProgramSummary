/*
 *      Selection： 是否是激活状态都选择
 *
 *      属性
 *          Hierarchy面板
 *              activeGameObject        选择的第一个对象的GameObject： 1、Hierarchy面板中的游戏对象  2、Project面板中的预制体 3、不可更改的对象（todo？）
 *              gameObjects             选择的所有对象的GameObject  ： 1、Hierarchy面板中的游戏对象  2、Project面板中的预制体 3、不可更改的对象
 *
 *              activeTransform         选择的第一个对象的Transform ：Hierarchy面板中的游戏对象
 *              transforms              选择的所有对象的Transform   ：Hierarchy面板中的游戏对象
 *
 *             
 *
 *          Hierarchy 和 Project 面板
 *              activeObject            选择的第一个对象
 *              objects                 选择的所有对象
 *
 *              activeInstanceID        选择对象的实例Id       ，id永远是独一无二的
 *              instanceIDs             选择的所有对象的实例Id  ，id永远是独一无二的
 *
 *              selectionChanged        选择的对象发生改变时，触发的委托事件
 *              
 *              activeContext           todo 
 *          Project 面板
 *              assetGUIDs              资源的GUID
 *      方法
 *          Contains                    判断某个Object是否被选择
 *          GetFiltered                 todo 
 *          GetTransforms               todo 
 *          GetTransforms               todo 
 *          SetActiveObjectWithContext  todo
 */
using UnityEngine;
using UnityEditor;

public class SelectionTool
{
    [MenuItem("MySelection/CurrentSelect")]
    private static void CurrentSelect()
    {
        Debug.Log(Selection.selectionChanged);
        //Debug.Log(Selection.assetGUIDs);

        ////Debug.Log(Selection.instanceIDs.Length);
        //foreach (string id in Selection.assetGUIDs)
        //{
        //    Debug.Log(id);
        //}
        //Debug.Log(Selection.instanceIDs);
    }
}
