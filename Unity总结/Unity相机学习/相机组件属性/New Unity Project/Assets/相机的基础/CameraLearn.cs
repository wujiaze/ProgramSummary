/*
 *      主题：Camera组件
 *      渲染规则：
 *              注意点： 以上效果和相机的Depth值相关。(Depth大的 覆盖 Depth小的)                           todo 是否需要解释
 *
 *      属性：1、Clear Flags  清除标记:  表示每一帧，在空白的地方，用什么来代替
 *                           1、SkyBox ：    使用  天空盒         填充没有游戏对象的空位置
 *                           2、Soild Color：使用  固定颜色       填充没有游戏对象的空位置  
 *                           3、Depth Only： 使用  空            填充没有游戏对象的空位置， todo 同时清除颜色的深度缓冲区
 *                           4、Don`t Clear：使用  上一帧的图像   填充没有游戏对象的空位置， todo 不清除深度和颜色缓冲区 
 *                          
 *           2、Culling Mask (遮罩剔除)：表示该相机渲染的层（游戏物体可以选择自身的渲染层）
 *
 *           3、Projection   投影方式：Perspective  透视相机
 *                                                          Field of View(FOV): Unity中表示透视相机的垂直视野角度大小(查看：使Camera的Y轴向上Z轴向前，垂直看过去的那个角度) 详细理解截锥体见 file://本工程--相机的基础--倾斜相机的截锥体
 *                                                          相机的显示框的长宽比：Game的分辨率的比
 *                                    Orthographic 正交相机   
 *                                                          Size ：表示 相机显示高度的一半（以默认值5为例，表示视野内高度为5*2个单位，而Unity中图片默认是100像素为1个单位）
 *
 *           4、Physical Camera 物理相机(跟现实一样) ：只支持透视相机，相当于根据现实的相机来设置，经过计算，最后改变了 透视相机的Field of View属性
 *                     Focal Length：焦距(单位毫米)，焦距越小，视角越大，Unity中的表现为  Field of View 越大 ，通过FOV同样可以改变焦距                  
 *                     Sensor Type ：传感器类型，预先设置了几种现实相机的真实数据，进而设置了  Sensor Size
 *                     Sensor Size ：传感器尺寸(单位毫米)，可以通过预定义，也可以是手动设置，X/Y 就是相机的视框的横纵比，具体见 todo 
 *                     Lens Shift  ：偏移镜头，功能和通过脚本改变镜头的截锥体一样，具体见  file://本工程--相机的应用--倾斜相机的截锥体
 *
 * 4、Clipping Planes  裁剪面：表示相机的视野长度范围，就如眼睛不能看到很近和很远的地方
 *
 *           5、Viewport Rect  相机画面显示位置   X:表示相机视图绘制的水平位置起点   Y:表示相机视图绘制的垂直位置起点
 *                                              W:表示整个相机视图在屏幕中的宽度   H：表示整个相机视图在屏幕中的高度
 *
 *           6、Depth   相机深度 ,Depth值大的会覆盖Depth小的渲染画面 （一般配合1/2/5）
 *
 *           7、Rendering Path : 相机使用的渲染方法                                    todo 不是太懂
 *
 *           8、Target Texture ：将相机渲染的画面写入图片，不在屏幕中显示
 *
 *           9、Occlusion Culling / Allow HDR / Allow MSAA / Allow Dynamic Resolution TODO 这些属性都不是太懂
 *
 *           10、Target Display ： 相机渲染画面显示的屏幕序号
 *
 *
 *      提示：多个不同的摄像机分别渲染不同的层和用一个摄像机来渲染所有的层的效率是完全一样的
 *
 *          遮挡剔除   todo 以后
 *          动态分辨率：todo 以后
 */
/*
 *     Rendering Path
 *
 */