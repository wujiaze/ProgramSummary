using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreatAssstObj {

    //RenderTexture movie;
    [MenuItem("Example/Create Asset")]
    public static void CreateMaterial()
    {
        RenderTexture movie = new RenderTexture(3200, 1200, 24, RenderTextureFormat.ARGB32);
        AssetDatabase.CreateAsset(movie, "Assets/ppp.renderTexture");
    }
}
