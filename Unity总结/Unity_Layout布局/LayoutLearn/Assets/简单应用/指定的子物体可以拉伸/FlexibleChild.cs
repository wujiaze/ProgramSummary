/*
 *   指定子物体可以拉升
 *   步骤1：创建有一个父物体，添加 Vertical Layout Group 组件，勾选 Child Controls Size 的 Width 和 Height
 *         不能勾选 Child Force Expand，这会使每一个子物体都被拉升
 *   步骤2：在父物体下增加子物体，子物体可以添加 Layout Element,这样 布局控制组件就会根据 布局元素组件来计算
 *          在需要拉伸的子物体的  Layout Element 上设置 Flexible ，还可以设置各自拉伸的比例
 *   步骤3：可以给父物体不能添加 Content Size Fitter ，会强制根据子物体 的 perferedSize ，而不会拉伸了
 *
 */
