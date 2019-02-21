/*
 *      TextureImporter
 *          分为两大类：
 *              一、平台设置类  TextureImporterPlatformSettings  详见 file://学习图片/Texture_Learn/TextureSetting_Editor/Learn_PlatformSettings
 *                      方法：
 *                          ClearPlatformTextureSettings        清空设置(适用于 "Web", "Standalone", "iPhone" and "Android")
 *                          GetDefaultPlatformTextureSettings	获取默认平台纹理设置
 *                          GetPlatformTextureSettings          获取指定平台纹理设置，true：说明有override，false：说明没有override 
 *                          SetPlatformTextureSettings          设置指定平台纹理设置
 *                          GetAutomaticFormat                  根据指定平台，判断自动选择的格式
 *              二、纹理设置类     TextureImporterSettings
 *                      方法：
 *                          ReadTextureSettings                 读取当前 TextureImporter 的设置，将内容复制到新的 TextureImporterSettings
 *                          SetTextureSettings                  对当前 TextureImporter 赋值一个 TextureImporterSettings
 *      其他方法：
 *          DoesSourceTextureHaveAlpha                          导入的图片是否有alpha通道
 *      属性：
 *          平台属性：详见
 *          纹理属性：详见
 *              spritePackingTag                        精灵图集的Tag，默认 ""
 *
 *
 * todo 未知
 *spritesheet                             包含这张Sprite的图集
 *qualifiesForSpritePacking               默认false，将压缩后的Texture打包成Sprite，true表示将未压缩的texture打包成Sprite
 *ReadTextureImportInstructions
 */