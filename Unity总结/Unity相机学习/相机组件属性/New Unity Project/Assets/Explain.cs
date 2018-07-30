/*
 *      主题：Camera组件的解释
 *
 *      提示：多个不同的摄像机分别渲染不同的层和用一个摄像机来渲染所有的层的效率是完全一样的
 *
 *      属性：1、Clear Flags  清除标记:  表示每一帧，在空白的地方，用什么来代替
 *                           1、SkyBox ：天空盒
 *                           2、Soild Color：固定颜色  Background：背景颜色
 *                           3、Depth Only： 只清除深度缓冲区(表现出来的形式是：物体显示在最前面，没有深度的感觉)
 *                           4、Don`t Clear：不清除深度和颜色缓冲区                                                   TODO 不太理解,用的也不多
 *                           注意点： 以上效果和相机的Depth值相关。(Depth大的 覆盖 Depth小的)                           todo 是否需要解释
 *
 *           2、Culling Mask 遮罩剔除：表示该相机渲染的Layer（在属性面板的最上层显示），只渲染选择的层级
 *
 *           3、Projection   投影方式：Perspective  透射相机   Field of View : Unity中表示透视相机的 垂直视野角度大小
 *                                                                          然后根据选定的分辨率，可以计算出 横向视野大小
 *                                    Orthographic 正交相机   Size ：表示 相机显示高度的一半（以默认值5为例，表示视野内高度为5*2个单位，而Unity中图片默认是100像素为1个单位）
 *
 *           4、Clipping Planes  裁剪面：表示相机的视野长度范围，就如眼睛不能看到很近和很远的地方
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
 */

/*
 *          使用例子：设置单独的UI摄像机
 *               0、将其他相机的 CullingMask 去掉UI
 *          步骤：1、创建Canvas，将 RenderMode 设置为 ScreenSpace-Camera
 *                2、将UIScaleMode 设置为 Scale With Screen Size
 *                3、根据需要的分辨率 设置 Reference Resolution
 *                4、根据实际需要以及分辨率，设置Match
 *                5、在Canvas中创建 Camera
 *                   5.1、Clear Flags 设置为 Depth only  （使UI显示在最前面）
 *                   5.2、Culling Mask 设置为 仅UI    
 *                   5.3、Projection 设置为 Orthographic
 *                   5.4、Depth 根据其他相机，设置为所有相机中的最大值
 *                   5.5、根据Canvas的 Plane Distance 的100，设置为-100
 *                6、将Camera 拖入 Canvas，reset Canvas的RectTransform
 *                7、去掉 AudioListener
 */
/*
 *         使用例子：设置3D枪模型
 *         步骤：1、添加 Layer -> gun ，将 枪的层级设置为 gun
 *               2、将 Main Camera 的位置调整到合适位置（人的眼睛），去掉 gun 的渲染
 *               3、添加 Gun Camera ，复制 Main Camera 的位置
 *               4、设置 Gun Camera 的 属性
 *                      4.1、Clear Flags 设置为 Depth Only
 *                      4.2、Culling Mask 设置为 gun
 *                      4.3、Depth 值大于 MainCamera
 *               5、将MainCamera 和 GunCamera 放在同一个对象下，这样旋转起来方便
 */
