/*
 *      ScriptableWizard
 *          属性
 * createButtonName
 *errorString
 *helpString
 * isValid
 * otherButtonName
 * 方法
 * DrawWizardGUI
 * 静态方法
 * DisplayWizard
 * 回调方法
 *OnWizardCreate
 * OnWizardOtherButton
 * OnWizardUpdate
 */
using UnityEditor;
using UnityEngine;

public class WizardTool : ScriptableWizard
{
    [MenuItem("Tools/CreateWizard")]
    static void CreateWizard()
    {
        DisplayWizard<WizardTool>("创建窗口的标题", "创建","Apply");
    }

    public float Value = 10;


    void OnWizardCreate() // CreateButton 按钮点击回调
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            ContextMenuTool tool = gameObject.GetComponent<ContextMenuTool>();
            Undo.RecordObject(tool, "MyName");  // 撤销对象必须明确指定 ，todo 这个名字显示在哪里
            tool.X += Value;
        }
    }

    void OnWizardOtherButton() // OtherButton 按钮点击回调
    {
        Debug.Log("OtherButton");
    }

    void OnWizardUpdate()// 界面中的值有修改时，或者界面打开时 回调
    {
        Debug.Log("OnWizardUpdate");
    }
}
