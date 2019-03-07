/*
 *     TextureImporterSettings  纹理导入的设置
 *
 *     一、创建
 *          1、可以 new 一个 TextureImporterSettings，然后设置好属性，使用 TextureImporter 中的 SeSetTextureSettingst 方法赋值 
 *
 *     二、属性（这里根据TextureType分,分类下没有的属性说明都是默认值，且无法修改）
 *          统一属性
 *              textureType                             纹理类型，Default | Bump(Normal Map) | GUI | Sprite | Cursor | Cookie | Lightmap | SingleChannel ,默认Default   
 *         一、Default 
 *              textureShape                            纹理类型,默认 2D
 *                      1、2D
 *                      2、Cube(前提：长宽需要是2的指数倍)
 *                           2.1、generateCubemap       生成 Cubemap 的模式,默认 AutoCubemap , (面板上显示 Mapping )
 *                                  Spheremap           从球形纹理中生成
 *                                  Cylindrical         从柱状纹理中生成
 *                                  FullCubemap         从垂直或水平交叉纹理中生成
 *                                  AutoCubemap         自动选择
 *                           2.2、cubemapConvolution    卷积模式，用于需要预先计算漫反射或镜面反射，默认是 None 
 *                                  None                不采用任何卷积，用于镜面或者天空盒
 *                                  Specular            高光卷积，环境光映射
 *                                  Diffuse             漫反射卷积，用于光辐射
 *                           2.3、seamlessCubemap       无缝，用于玻璃反射，默认是false，(面板上显示 Fixup Edge Seams)
 *              sRGBTexture                             颜色空间,颜色是否存储在sRGB颜色空间，默认true，false表示颜色存储在线性空间
 *              alphaSource                             alpha 数据的来源，默认 Frominput
 *                  1、None                             不使用alpha数据
 *                  2、FromInput                        如果纹理自带alpha数据，则使用
 *                  3、FromGrayScale                    从灰度图中生成alpha数据
 *              alphaIsTransparency                     alpha是否代表透明,默认 false,推荐 true
 *              alphaTestReferenceValue                 alpha 的阈值,默认 0.5,即 小于0.5判断为透明
 *              npotScale                               非2次方的缩放模式,默认是ToNearest，推荐 None
 *                  1、None                             不采用缩放模式
 *                  2、ToNearest                        长宽分别缩放到最近的大小
 *                  3、ToLarger                         长宽全部放大
 *                  4、ToSmaller                        长宽全部缩小
 *              readable                                是否可读，默认false
 *              mipmap总体
 *                      streamingMipmaps                是否启用 mipmap streaming 系统，true：Unity只加载当前相机中用到的mipmap，并且随距离改变，false：加载全部的mipmap，默认是false
 *                      streamingMipmapsPriority        mipmap streaming优先级，用于内存预算，值越大，存储的空间越大
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps),默认true
 *                      borderMipmap                    是否保持和texture相同的边界，这里是 false，一般用于Cookie时为true
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel ，默认1
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel ，默认3
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认Repeat  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *         二、Normal Map (Bump)
 *              textureShape                            纹理类型,默认 2D
 *                      1、2D
 *                      2、Cube(前提：长宽需要是2的指数倍)
 *                            2.1、generateCubemap      生成 Cubemap 的模式,默认 AutoCubemap ,(面板上显示 Mapping )
 *                                  Spheremap           从球形纹理中生成
 *                                  Cylindrical         从柱状纹理中生成
 *                                  FullCubemap         从垂直或水平交叉纹理中生成
 *                                  AutoCubemap         自动选择
 *                            2.2、seamlessCubemap      无缝，用于玻璃反射，默认是false,(面板上显示 Fixup Edge Seams)
 *              convertToNormalmap                      是否将高度图转换成法线图，默认是false，(面板上是Create from Grayscale)
 *                      1、false
 *                      2、true
 *                          2.1、heightmapScale         高度图中的颠簸程度[0,0.3],默认是0.25，(面板上是Bumpiness)
 *                          2.2、normalmapFilter        法线图的过滤模式，默认是 Standard,(面板上是 Filtering )
 *                                 2.2.1、Standard      标准模式   (面板上是 Sharp )
 *                                 2.2.2、Sobel         Sobel模式  (面板上是 Smooth )
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      streamingMipmaps                是否启用 mipmap streaming 系统，true：Unity只加载当前相机中用到的mipmap，并且随距离改变，false：加载全部的mipmap，默认是false
 *                      streamingMipmapsPriority        mipmap streaming优先级，用于内存预算，值越大，存储的空间越大,默认是0
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)
 *                      borderMipmap                    是否保持和texture相同的边界，这里是 false，一般用于Cookie时为true
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认Repeat  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认 Trilinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *         三、GUI
 *              alphaSource                             alpha 数据的来源，默认 Frominput
 *              alphaIsTransparency                     alpha是否代表透明,默认 true
 *              alphaTestReferenceValue                 alpha 的阈值,默认 0.5
 *              npotScale                               NPOT,默认是 None
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)
 *                      borderMipmap                    是否保持和texture相同的边界，这里是 false，一般用于Cookie时为true
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认 Clamp  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *          四、Sprite
 *              spriteMode                              精灵导入模式，默认 1(这个属性详见TextureImporter的spriteImportMode)
 *                      1、Single                                    单个精灵
 *                          1.1、spriteMeshType                      精灵网格类型，默认 Tight
 *                                  1.1.1、FullRect                  矩形网格根据用户指定的精灵格式
 *                                  1.1.2、Tight                     根据alpha值，来生成精灵网格
 *                          1.2、spriteExtrude                       图片的边缘和生成的精灵网格之间的空白像素数量，默认是1
 *                          1.3、spritePivot                         精灵图片的轴点
 *                               1.3.1、spriteAlignment              精灵图片的对齐方式，即一些预定义的轴点
 *                          1.4、spriteGenerateFallbackPhysicsShape  是否生成默认的物理形状，当用户没有指定具体的物理形状，默认是true
 *                      2、Multiple                                  多个精灵
 *                          2.1、spriteMeshType                      精灵网格类型，默认 Tight
 *                                  2.1.1、FullRect                  矩形网格根据用户指定的精灵格式
 *                                  2.1.2、Tight                     根据alpha值，来生成精灵网格
 *                          2.2、spriteExtrude                       图片的边缘和生成的精灵网格之间的空白像素数量，默认是1
 *                          2.3、spriteGenerateFallbackPhysicsShape  是否生成默认的物理形状，当用户没有指定具体的物理形状，默认是true
 *                      3、Polygon                                   多边形精灵
 *                          3.1、spriteExtrude                       图片的边缘和生成的精灵网格之间的空白像素数量，默认是1
 *              
 *              spritePixelsPerUnit                     精灵图集的像素单位,默认 100
 *              Sprite编辑界面属性
 *                  spriteBorder                            精灵图片的边界，一个精灵具有蓝框和绿框，蓝色表示:整个精灵的大小，绿色表示：可以拉伸的精灵边界,默认是 （0，0，0，0）表示两者重合
 *                  可以自定义物理形状
 *              sRGBTexture                             颜色空间,颜色是否存储在sRGB颜色空间，默认true，false表示颜色存储在线性空间
 *              alphaSource                             alpha 数据的来源，默认 Frominput
 *              alphaIsTransparency                     alpha是否代表透明,默认 true
 *              alphaTestReferenceValue                 alpha 的阈值,默认 0.5
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)，默认false
 *                      borderMipmap                    是否保持和texture相同的边界，默认 false
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认 Clamp  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *          五、Cursor
 *              alphaSource                             alpha 数据的来源，默认 Frominput
 *              alphaIsTransparency                     alpha是否代表透明,默认 true
 *              alphaTestReferenceValue                 alpha 的阈值,默认 0.5
 *              isReadable                              是否可读，默认 true
 *              mipmap总体
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)，默认是false
 *                      borderMipmap                    是否保持和texture相同的边界，默认 false
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认 Clamp  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *          六、Cookie
 *              Light Type                              todo 官网没有解释，默认是 Spotlight
 *              alphaSource                             alpha 数据的来源，默认 Frominput
 *              alphaIsTransparency                     alpha是否代表透明,默认 false
 *              alphaTestReferenceValue                 alpha 的阈值,默认 0.5
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)，默认是true
 *                      borderMipmap                    是否保持和texture相同的边界，默认 true
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认 Clamp  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *          七、Lightmap
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      streamingMipmaps                是否启用 mipmap streaming 系统，true：Unity只加载当前相机中用到的mipmap，并且随距离改变，false：加载全部的mipmap，默认是false
 *                      streamingMipmapsPriority        mipmap streaming优先级，用于内存预算，值越大，存储的空间越大
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps),默认true
 *                      borderMipmap                    是否保持和texture相同的边界，这里是 false
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel ，默认1
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel ，默认3
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认 Clamp  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *          八、SingleChannel
 *              textureShape                            纹理类型,默认 2D
 *                      1、2D
 *                      2、Cube(前提：长宽需要是2的指数倍)
 *                            2.1、generateCubemap      生成 Cubemap 的模式,默认 AutoCubemap ,(面板上显示 Mapping )
 *                                  Spheremap           从球形纹理中生成
 *                                  Cylindrical         从柱状纹理中生成
 *                                  FullCubemap         从垂直或水平交叉纹理中生成
 *                                  AutoCubemap         自动选择
 *                            2.2、seamlessCubemap      无缝，用于玻璃反射，默认是false,(面板上显示 Fixup Edge Seams)
 *              singleChannelComponent                  单通道选择，默认是 alpha
 *                          1、Alpha                    alpha 通道
 *                              1.1、alphaSource                             alpha 数据的来源，默认 Frominput
 *                              1.2、alphaIsTransparency                     alpha是否代表透明,默认 false
 *                              1.3、alphaTestReferenceValue                 alpha 的阈值,默认 0.5
 *                          2、Red                      Red 通道
 *              isReadable                              是否可读，默认false
 *              mipmap总体
 *                      streamingMipmaps                是否启用 mipmap streaming 系统，true：Unity只加载当前相机中用到的mipmap，并且随距离改变，false：加载全部的mipmap，默认是false
 *                      streamingMipmapsPriority        mipmap streaming优先级，用于内存预算，值越大，存储的空间越大
 *                      mipmapEnabled                   是否生成mipmap,(面板显示 Generate Mip Maps)，默认是true
 *                      borderMipmap                    是否保持和texture相同的边界，这里是 false
 *                      mipmapFilter                    mipmap过滤模式，默认box
 *                             1、BoxFilter             Box过滤
 *                             2、KaiserFilter          Kaiser过滤
 *                      mipMapsPreserveCoverage         保留覆盖率，默认是false, （面板上的 Alpha Cutoff Value 就是 alphaTestReferenceValue 的值）
 *                      fadeout                         是否开启渐隐，默认 false
 *                      mipmapFadeDistanceStart         mipmap开始渐隐的 miplevel 
 *                      mipmapFadeDistanceEnd           mipmap完全隐藏的 miplevel 
 *                      aniso                           各向异性等级，默认是1
 *                      mipMapBias                      mipmap的偏差，正偏差模糊，负偏差锐化，默认-100，即无效，（面板上不显示）
 *              wrapMode                                循环模式，默认Repeat  详见 file://学习图片/Texture_Learn/WrapMode/WrapModeLearn
 *              filterMode                              过滤模式，默认Billinear
 *                      1、Point                        纹理像素块状处理
 *                      2、Bilinear                     纹理像素平均采样
 *                      3、Trilinear                    当有mipmap时，相当于 Bilinear + mipmap level，当没有 mipmap时，相当于 Bilinear
 *
 *      方法：
 *          ApplyTextureType                            用指定的类型参数来设置TextureImporterSettings 
 *          CopyTo                                      复制
 *
 *spriteTessellationDetail  todo 未知：
 */