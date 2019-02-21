/*
 *      TextureImporterPlatformSettings 纹理导入的平台设置 (显示在图片Inspector的最下面)
 *      
 *      一、创建
 *          1、TextureImporter 中的 GetPlatformTextureSettings 方法
 *          2、可以 new 一个 TextureImporterPlatformSettings，然后设置好属性，使用 TextureImporter 中的 SetPlatformTextureSettings 方法赋值
 *          说明：显示在图片Inspector的最下面每一个TextureImporterPlatformSettings，都是一个个单独的个体
 *
 *      二、属性
 *          maxTextureSize                      最大的纹理尺寸，超过这个尺寸的图片会按比例缩小，默认是 2048
 *          resizeAlgorithm                     压缩算法，当图片尺寸大于 maxTextureSize ，采用的压缩算法，默认是Mitchell
 *                  1、Mitchell                  高质量压缩算法
 *                  2、Bilinear                  在噪音纹理上，可能比 Mitchell 保留更多的细节
 *          format                               纹理的格式，默认是 Automatic，各类格式详见 https://docs.unity3d.com/2019.1/Documentation/ScriptReference/TextureImporterFormat.html
 *                  1、textureCompression        当 format 为 Automatic 时,根据 设置条件、平台、图片参数 选择适合的压缩格式，默认是  Compressed
 *                      1.1、Uncompressed        不采用任何压缩格式
 *                      1.2、CompressedLQ        低质量、高度压缩、高性能，适用于低端机
 *                      1.3、Compressed          标准压缩，适用于大部分机器
 *                      1.4、CompressedHQ        高质量， 适用于高端机
 *          crunchedCompression                  是否使用 有损压缩格式，解压速度快，压缩速度慢，压缩包小，默认 false
 *          compressionQuality                   压缩质量，默认为50，Normal 
 *              与TextureCompressionQuality对应
 *                      Fast = 0                 低质量，快速
 *                      Normal = 1~99            一般质量
 *                      Best = 100               高质量
 *          allowsAlphaSplitting                 是否从纹理中分离alpha，默认false，适用于特殊情况（比如ETC1）
 *
 *          overridden                           是否覆盖Defalut设置
 *          name                                 需要覆盖的平台名字："Standalone", "Web", "iPhone", "Android", "WebGL", "Windows Store Apps", "PS4", "PSM", "XboxOne", "Nintendo 3DS", "tvOS"
 *                                               可以通过多次设置SetPlatformTextureSettings，来覆盖多个平台
 *          特例：安卓设置
 *          androidETC2FallbackOverride	         当安卓设备不支持ETC2时，使用的方法，在BuildSettng中可以设置，默认是 Quality32Bit
 *                                   
 *      三、方法
 *          CopyTo                               将当前的 设置拷贝到 目标 TextureImporterPlatformSettings
 */
