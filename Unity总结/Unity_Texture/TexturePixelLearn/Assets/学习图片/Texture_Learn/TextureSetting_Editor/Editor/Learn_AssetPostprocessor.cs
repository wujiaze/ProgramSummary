/*
 *      AssetPostprocessor  :处理资源导入前/后的类
 *      属性
 *          assetImporter               资源导入器
 *          assetPath                   资源的导入路径
 *          context                     资源导入时所包含的上下文信息
 *      方法
 *          GetPostprocessOrder
 *          GetVersion
 *          LogError                    类似Debug.LogError
 *          LogWarning                  类似Debug.LogWarning
 *      回调
 *          1、图片
 *                  OnPreprocessTexture                                 图片导入器执行前的回调，一般用于设置 TextureImporter 的属性
 *                  OnPostprocessTexture(Texture2D)                     图片导入器执行后的回调
 *             1.1、Sprite相关
 *                    OnPostprocessSprites                              图片设置为Sprite后的回调
 *
 *          2、声音
 *                  OnPreprocessAudio                                   声音导入器执行前的回调，一般用于设置 AudioImporter 的属性
 *                  OnPostprocessAudio(AudioClip)                       声音导入器执行后的回调
 *          
 *          3、模型
 *                  OnPreprocessModel                                   模型导入器执行前的回调，一般用于设置 ModelImporter 的属性
 *                  OnPostprocessModel(GameObject)                      模型导入器执行后的回调
 *             3.1、模型相关
 *                    OnPostprocessMeshHierarchy    todo
 *                    OnAssignMaterialModel             todo
 *                    OnPostprocessGameObjectWithUserProperties todo
 *
 *          4、SpeedTree:树/草模型
 *                  OnPreprocessSpeedTree                               树/草模型导入器执行前的回调，一般用于设置 ModelImporter 的属性
 *                  OnPostprocessSpeedTree                              树/草模型导入器执行后的回调
 *
 *          5、模型动画
 *                  OnPreprocessAnimation                               模型动画导入器执行前的回调，一般用于设置 ModelImporter 的属性
 *                  OnPostprocessAnimation(GameObject,AnimationClip)    模型动画导入器执行后的回调
 *
 *          6、任意资源
 *              OnPreprocessAsset                                       资源导入器执行前的回调，一般用于设置 AssetImporter 的属性
 *              OnPostprocessAllAssets                                  资源导入器执行后的回调
 *          7、其他
 * OnPostprocessAssetbundleNameChanged  todo
 * OnPostprocessCubemap                 todo
 * OnPostprocessGameObjectWithAnimatedUserProperties    todo
 * OnPostprocessMaterial                            todo
 * 
 */
using UnityEditor;
using UnityEngine;

public class Learn_AssetPostprocessor : AssetPostprocessor
{
    // 图片
    private void OnPreprocessTexture()
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;
        if (textureImporter == null)
            return;
        Debug.Log("图片导入前");
    }

    private void OnPostprocessTexture(Texture2D tex)
    {
        if (tex != null)
        {
            Debug.Log("图片导入后" + tex.name);
        }
    }

    // Audio
    private void OnPreprocessAudio()
    {
        AudioImporter audioImporter = assetImporter as AudioImporter;
        if (audioImporter == null)
            return;
        Debug.Log("声音导入前");
    }

    private void OnPostprocessAudio(AudioClip clip)
    {
        if (clip != null)
        {
            Debug.Log("声音导入后" + clip.name);
        }
    }

    // Model
    private void OnPreprocessModel()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        if (modelImporter == null)
            return;
        Debug.Log("模型导入前");
    }

    private void OnPostprocessModel(GameObject rootGo)
    {
        if (rootGo != null)
        {
            Debug.Log("模型导入后 " + rootGo.name);
        }
    }



    // ModelAnimation
    private void OnPreprocessAnimation()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        if (modelImporter == null)
            return;
        Debug.Log("模型动画导入前");
    }

    private void OnPostprocessAnimation(GameObject go, AnimationClip clip)
    {
        if (go != null && clip != null)
        {
            Debug.Log("模型动画导入后 " + go.name + " " + clip.name);
        }
    }

    // SpeedTree
    private void OnPreprocessSpeedTree()
    {
        SpeedTreeImporter speedTreeImporter = assetImporter as SpeedTreeImporter;
        if (speedTreeImporter == null)
            return;
        Debug.Log("树模型导入前");
    }
    private void OnPostprocessSpeedTree(GameObject rootGo)
    {
        if (rootGo != null)
        {
            Debug.Log("树模型导入后 " + rootGo.name);
        }
    }

    // 任何资源
    private void OnPreprocessAsset()
    {
        if (assetImporter == null)
            return;
        Debug.Log("OnPreprocessAsset");
    }
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            Debug.Log("Reimported Asset: " + str);
        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
            Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        }
    }

}
