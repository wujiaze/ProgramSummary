using UnityEngine;
using UnityEditor;
public class MyTexImporter:EditorWindow
{
    [MenuItem("Window/MyTexImporter")]
    private static void CreateTexImporterWindow()
    {
        EditorWindow.GetWindow<MyTexImporter>();
    }
}
