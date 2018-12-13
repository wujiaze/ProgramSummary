/*
 *   创建一个默认的Button
 *   步骤1：给Button添加一个 Horizontal Layout Group 组件(Vertical也可以，对于只有一个子对象效果相同)，勾选 Child Controls Width 和 Height
 *          作用1：控制了子对象 Text 的Size跟随内容的改变而改变大小
 *          作用2：给Button的RectTransform 提供了 perfered Size
 *   步骤2：给Button添加一个 Content Size Fitter 组件，横向和纵向都选择 PreferredSize，这样Button给设置为 Horizontal Layout Group 提供的Size
 */
