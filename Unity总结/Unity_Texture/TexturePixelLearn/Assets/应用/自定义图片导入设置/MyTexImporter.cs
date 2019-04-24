/* 不同的项目，需要不同的设置*/
using UnityEditor;
using UnityEngine;

public enum MaxSize
{
    Size_32 = 32,
    Size_64 = 64,
    Size_128 = 128,
    Size_256 = 256,
    Size_512 = 512,
    Size_1024 = 1024,
    Size_2048 = 2048,
    Size_4096 = 4096,
    Size_8192 = 8192,

}
public class MyTexEditorWindow : EditorWindow
{
    [MenuItem("Window/MyTexImporter")]
    private static void MyWindow()
    {
        GetWindow<MyTexEditorWindow>();
    }
    public static bool IsApply = false;



    public static TextureImporterType Texturetype = TextureImporterType.Default;
    public static TextureImporterShape TexShape = TextureImporterShape.Texture2D;
    public static bool IsSrgb = true;
    public static TextureImporterAlphaSource AlphaSource = TextureImporterAlphaSource.FromInput;
    public static bool IsAlphaIsTransparency = true;
    public static TextureImporterNPOTScale NpotScale = TextureImporterNPOTScale.None;
    public static bool IsRead = false;
    public static bool IsStreamingMipMap = false;
    public static bool IsGenerateMipmap = false;
    public static TextureWrapMode DefalutWarpMode = TextureWrapMode.Repeat;
    public static TextureWrapMode SpriteWarpMode = TextureWrapMode.Clamp;
    public static MaxSize MaxSize = MaxSize.Size_2048;
    public static TextureImporterCompression Compression = TextureImporterCompression.Compressed;


    public static SpriteImportMode SpriteImportMode = SpriteImportMode.Single;
    public static string PackingTag = "";
    private void OnGUI()
    {
        Texturetype = (TextureImporterType)EditorGUILayout.EnumPopup("Texture Type", Texturetype);
        switch (Texturetype)
        {
            case TextureImporterType.Default:
                TexShape = (TextureImporterShape)EditorGUILayout.EnumPopup("Texture Shape", TexShape);
                IsSrgb = EditorGUILayout.Toggle("sRGB(Color Texture)", IsSrgb);
                AlphaSource = (TextureImporterAlphaSource)EditorGUILayout.EnumPopup("Alpha Source", AlphaSource);
                IsAlphaIsTransparency = EditorGUILayout.Toggle("Alpha Is Transparency", IsAlphaIsTransparency);
                NpotScale = (TextureImporterNPOTScale)EditorGUILayout.EnumPopup("Non Power of 2", NpotScale);
                IsRead = EditorGUILayout.Toggle("Read/Write Enabled", IsRead);
                IsStreamingMipMap = EditorGUILayout.Toggle("Streaming Mip Maps", IsStreamingMipMap);
                IsGenerateMipmap = EditorGUILayout.Toggle("Generate Mip Maps", IsGenerateMipmap);
                DefalutWarpMode = (TextureWrapMode)EditorGUILayout.EnumPopup("Wrap Mode", DefalutWarpMode);
                MaxSize = (MaxSize)EditorGUILayout.EnumPopup("Max Size", MaxSize);
                Compression = (TextureImporterCompression)EditorGUILayout.EnumPopup("Compression", Compression);
                break;
            case TextureImporterType.NormalMap:
                break;
            case TextureImporterType.GUI:
                break;
            case TextureImporterType.Sprite:
                SpriteImportMode = (SpriteImportMode)EditorGUILayout.EnumPopup("Sprite Mode", SpriteImportMode);
                PackingTag = EditorGUILayout.TextField("Packing Tag", PackingTag);
                IsSrgb = EditorGUILayout.Toggle("sRGB(Color Texture)", IsSrgb);
                AlphaSource = (TextureImporterAlphaSource)EditorGUILayout.EnumPopup("Alpha Source", AlphaSource);
                IsAlphaIsTransparency = EditorGUILayout.Toggle("Alpha Is Transparency", IsAlphaIsTransparency);
                IsRead = EditorGUILayout.Toggle("Read/Write Enabled", IsRead);
                IsGenerateMipmap = EditorGUILayout.Toggle("Generate Mip Maps", IsGenerateMipmap);
                SpriteWarpMode = (TextureWrapMode)EditorGUILayout.EnumPopup("Wrap Mode", SpriteWarpMode);
                MaxSize = (MaxSize)EditorGUILayout.EnumPopup("Max Size", MaxSize);
                Compression = (TextureImporterCompression)EditorGUILayout.EnumPopup("Compression", Compression);
                break;
            case TextureImporterType.Cursor:
                break;
            case TextureImporterType.Cookie:
                break;
            case TextureImporterType.Lightmap:
                break;
            case TextureImporterType.SingleChannel:
                break;
        }
        if (GUILayout.Button("设置图片属性后、导入或修改前，请点击本按钮！"))
        {
            IsApply = true;
            Debug.Log("已应用设置");
        }
        if (GUILayout.Button("还原设置： 请点击本按钮 或者 直接关闭面板 ！"))
        {
            IsApply = false;
            Debug.Log("已还原设置");
        }
    }
    private void OnDestroy()
    {
        IsApply = false;
        Debug.Log("已还原设置");
    }
}
public class MyTexImporter : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;
        if (textureImporter == null)
            return;
        if (MyTexEditorWindow.IsApply)
        {
            // 第一部分：平台设置类 TextureImporterPlatformSettings
            SetPlatform(textureImporter);
            // 第二部分：纹理设置类 TextureImporterSettings
            SetTexture(textureImporter);
        }
    }
    private void OnPostprocessTexture(Texture2D tex)
    {
        if (tex != null)
        {
            Debug.Log("图片导入后" + tex.name);
        }
    }


    private void SetPlatform(TextureImporter textureImporter)
    {
        // Default
        TextureImporterPlatformSettings defaultSettings = new TextureImporterPlatformSettings
        {
            maxTextureSize = (int)MyTexEditorWindow.MaxSize,
            textureCompression = MyTexEditorWindow.Compression,
        };
        textureImporter.SetPlatformTextureSettings(defaultSettings);
        //// 安卓
        //TextureImporterPlatformSettings androidSettings = new TextureImporterPlatformSettings
        //{
        //    // 没有设置的属性，表示采用默认值
        //    overridden = true,
        //    name = "Android"
        //};
        //textureImporter.SetPlatformTextureSettings(androidSettings);
        //// Iphone 
        //TextureImporterPlatformSettings iosSettings = new TextureImporterPlatformSettings();
        //androidSettings.CopyTo(iosSettings);
        //iosSettings.name = "iPhone";
        //textureImporter.SetPlatformTextureSettings(iosSettings);
    }

    private void SetTexture(TextureImporter textureImporter)
    {
        textureImporter.textureType = MyTexEditorWindow.Texturetype; // 类型
        switch (textureImporter.textureType)
        {
            case TextureImporterType.Default:
                SetDefault(textureImporter);
                break;
            case TextureImporterType.NormalMap:
                break;
            case TextureImporterType.GUI:
                break;
            case TextureImporterType.Sprite:
                SetSprite(textureImporter);
                break;
            case TextureImporterType.Cursor:
                break;
            case TextureImporterType.Cookie:
                break;
            case TextureImporterType.Lightmap:
                break;
            case TextureImporterType.SingleChannel:
                break;
        }
    }

    private void SetDefault(TextureImporter textureImporter)
    {
        textureImporter.textureShape = MyTexEditorWindow.TexShape;
        TextureImporterSettings settings = new TextureImporterSettings();
        textureImporter.ReadTextureSettings(settings);

        settings.sRGBTexture = MyTexEditorWindow.IsSrgb;
        settings.alphaSource = MyTexEditorWindow.AlphaSource;
        settings.alphaIsTransparency = MyTexEditorWindow.IsAlphaIsTransparency;
        settings.npotScale = MyTexEditorWindow.NpotScale;
        settings.readable = MyTexEditorWindow.IsRead;
        settings.streamingMipmaps = MyTexEditorWindow.IsStreamingMipMap;
        settings.mipmapEnabled = MyTexEditorWindow.IsGenerateMipmap;
        settings.wrapMode = MyTexEditorWindow.DefalutWarpMode;

        textureImporter.SetTextureSettings(settings);
    }

    private void SetSprite(TextureImporter textureImporter)
    {
        textureImporter.textureShape = MyTexEditorWindow.TexShape;
        TextureImporterSettings settings = new TextureImporterSettings();
        textureImporter.ReadTextureSettings(settings);

        textureImporter.spriteImportMode = MyTexEditorWindow.SpriteImportMode; //特例
        textureImporter.spritePackingTag = MyTexEditorWindow.PackingTag;    //特例
        settings.sRGBTexture = MyTexEditorWindow.IsSrgb;
        settings.alphaSource = MyTexEditorWindow.AlphaSource;
        settings.alphaIsTransparency = MyTexEditorWindow.IsAlphaIsTransparency;
        settings.readable = MyTexEditorWindow.IsRead;
        settings.mipmapEnabled = MyTexEditorWindow.IsGenerateMipmap;
        settings.wrapMode = MyTexEditorWindow.SpriteWarpMode;

        textureImporter.SetTextureSettings(settings);
    }
}
