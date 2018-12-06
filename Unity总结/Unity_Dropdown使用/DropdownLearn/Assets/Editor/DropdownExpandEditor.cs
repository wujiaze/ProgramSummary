using UnityEditor;
using UnityEditor.UI;
[CustomEditor(typeof(DropdownExpand), true)]
[CanEditMultipleObjects]
public class DropdownExpandEditor : DropdownEditor
{

    SerializedProperty m_AlwaysCallback;
    protected override void OnEnable()
    {
        base.OnEnable();
        m_AlwaysCallback = serializedObject.FindProperty("m_AlwaysCallback");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(m_AlwaysCallback);
        serializedObject.ApplyModifiedProperties();
    }
}
