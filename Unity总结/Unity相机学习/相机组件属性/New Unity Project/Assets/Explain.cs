

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

