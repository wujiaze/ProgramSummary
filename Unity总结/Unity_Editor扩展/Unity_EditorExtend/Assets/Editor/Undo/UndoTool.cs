/*
 *      Undo  撤销系统
 *
 *      静态属性
 *          undoRedoPerformed               todo 
 *          willFlushUndoRecord             todo 
 *      回调                                 
 *          UndoRedoCallback                todo 
 *          WillFlushUndoRecord             todo 
 *      方法
 *          AddComponent                    todo 
 *          ClearAll                        todo 
 *          ClearUndo                       todo 
 *          CollapseUndoOperations          todo 
 *          DestroyObjectImmediate          删除对象（Hierarchy和Project）
 *          FlushUndoRecordObjects          todo 
 *          GetCurrentGroup                 todo 
 *          GetCurrentGroupName             todo 
 *          IncrementCurrentGroup           todo 
 *          MoveGameObjectToScene           todo 
 *          PerformRedo                     todo 
 *          PerformUndo                     todo 
 *          RecordObject                    记录对象当前状态，之后改变的状态可以撤销，对 1、改变父物体 2、添加组件 3、对象销毁 无效 
 *          RecordObjects                   todo 
 *          RegisterCompleteObjectUndo      todo 
 *          RegisterCreatedObjectUndo       todo 
 *          RegisterFullObjectHierarchyUndo todo 
 *          RevertAllDownToGroup            todo 
 *          RevertAllInCurrentGroup         todo 
 *          SetCurrentGroupName             todo 
 *          SetTransformParent              todo 
 */                                          
using UnityEngine;                           
using UnityEditor;                           
public class UndoTool
{
    [MenuItem("UnDoMenu/DoDelete")]
    private static void UnDoDelete()// 采用本方法进行删除对象，可以撤销
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.DestroyObjectImmediate(gameObject);    // 采用ctrl + Z 撤销
        }
    }

}
