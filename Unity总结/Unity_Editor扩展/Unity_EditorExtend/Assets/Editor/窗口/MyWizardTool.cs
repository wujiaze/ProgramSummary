/*
 *    ScriptableWizard   提示框,继承 EditorWindow，是Unity提供的一种模板,适用于短期修改数据的窗口
 *          属性
 *              createButtonName    创建按钮的名字，点击该按钮触发 OnWizardCreate 回调
 *              otherButtonName     另一个按钮的名字，点击该按钮触发 OnWizardOtherButton 回调
 *              errorString         错误信息
 *              helpString          帮助信息
 *              isValid
 *          方法
 *            一般方法
 *              DrawWizardGUI
 *            静态方法
 *              DisplayWizard       打开编辑界面
 *          消息回调
 *              OnWizardCreate      Create按钮点击时触发
 *              OnWizardOtherButton Other按钮点击触发
 *              OnWizardUpdate      界面中的值有修改时，或者界面打开时 回调
 */
using UnityEditor;
using UnityEngine;

public class MyWizardTool : ScriptableWizard
{
    // 创建一个提示面板
    [MenuItem("MyWindows/CreateWizard")]
    private static void CreateWizard()
    {
        DisplayWizard<MyWizardTool>("创建窗口的标题", "Create 并且 关闭 按钮", "Other按钮");
    }

    // 该面板中的属性字段
    public float Value = 10;

    private void OnEnable()
    {
        Value = EditorPrefs.GetFloat("MyWizardTool.Value", Value);
    }


    void OnWizardOtherButton() // OtherButton 按钮点击回调
    {
        OnWizardCreate();
        ShowNotification(new GUIContent(Selection.objects.Length + "个对象的值被修改了 "));


    }

    void OnWizardCreate() // CreateButton 按钮点击回调
    {
        if(Selection.gameObjects.Length==0)
            return;
        int count = 0;
        EditorUtility.DisplayProgressBar("修改值的进度条", "当前值进度", (float)count / Selection.gameObjects.Length);
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            MyContextMenuTool tool = gameObject.GetComponent<MyContextMenuTool>();
            // 指定撤销对象，指定这一步撤销的名字，在Undo列表中可见
            Undo.RecordObject(tool, "MyNameUndo");
            tool.X += Value;
            count++;
            EditorUtility.DisplayProgressBar("修改值的进度条", "当前值进度", (float)count / Selection.gameObjects.Length);
        }
        //EditorUtility.ClearProgressBar(); // 太快了，就用注释看一下，正常需要关闭
        EditorPrefs.SetFloat("MyWizardTool.Value", Value);
    }



    void OnWizardUpdate()// 界面中的值有修改时，或者界面打开时 回调
    {
        Debug.Log("OnWizardUpdate");
    }

    private void OnSelectionChange() // 对象选择修改时，显示提示信息
    {
        helpString = null;
        errorString = null;
        if (Selection.objects.Length > 0)
        {
            helpString = "当前选择了" + Selection.objects.Length + "对象";
        }
        else
        {
            errorString = "请选择至少一个对象";
        }
    }


}
