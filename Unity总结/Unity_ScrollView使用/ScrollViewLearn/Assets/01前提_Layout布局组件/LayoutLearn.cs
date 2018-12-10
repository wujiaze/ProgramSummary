/*
 *   Auto Layout 自动布局组件  参考地址：1、官网      2、http://www.manew.com/thread-95758-1-1.html
 *
 *      子物体自身：Layout Element
 *      父物体用来控制子物体：Horizontal Layout Group 、Vertical Layout Group 、Grid Layout Group
 *      父物体自身：Content Size Fitter、Aspect Ratio Fitter
 *
 *      一、Layout Element:布局元素
 *              1、原理：一般UI元素有三个布局值，第一个min最小值，第二个perfer首选值，第三个flexible可变化值(可选，一般取值0~1)
 *                 父物体通过不同的布局组件(下述)，分配顺序：
 *                                            1.分配 MinSize 给UI子对象
 *                                            2.如果父物体还有多余空间，就会分配多的Size给UI子对象，直到子对象Size等于PreferredSize
 *                                            3.如果父物体还有多余空间，并且Flexible Size勾选了，就会拉伸UI子物体(Size)，撑满整个父物体，规则是每个子物体通过比较flexible值，按比例分配父物体多余的空间
 *              2、UI元素一般隐藏了一个Layout Element:
 *                                                  MinSize = 0
 *                                                  PreferredSize = 图片原始大小/Text会根据字体自动设置大小(内置 Content Size Fitter来获取值)
 *                                                  Flexible Size = 不勾选
 *                 当无法满足我们的需求时，可以添加一个Layout Element，用来覆盖原来的值(勾选表示覆盖)
 *              3、API：
 *                      minHeight、minWidth、preferredHeight、preferredWidth、flexibleHeight、flexibleWidth
 *                      ignoreLayout：忽略本组件
 *                      layoutPriority：优先级(大于等于0采用本组件，小于0采用原生隐藏组件)
 *
 *      二、父物体的布局控制器(一): Horizontal Layout Group、Vertical Layout Group，这里就以Vertical Layout Group讲解
 *              1、总体理解：1.1、可以嵌套
 *                          1.2、分配子物体的Size，满足Layout Element的分配顺序
 *              Vertical Layout Group
 *                  1、原理：满足 Layout Element 分配规则，高度等于 子物体的高度+边缘高度+间隔高度
 *                  2、API：padding:子物体组相对于父物体边缘的距离
 *                          spacing:子物体之间的间隔
 *                          childAlignment：子物体开始布局的起点，也是对齐标准
 *                          childControlHeight、childControlWidth：由父物体来控制子物体们的Size
 *                                                                 仅满足子物体Layout Element的分配顺序前两条：首先满足MinSize，随后父物体有额外的空间就扩大Size至PreferredSize
 *                                                                 childControlHeight：控制Y轴，childControlWidth控制X轴
 *                          childForceExpandHeight、childForceExpandWidth：由父物体来控制子物体们是否撑满父物体
 *                                                                         仅满足子物体Layout Element的分配顺序的第三条：如果父物体有多余空间(这里指Y轴)，则拉伸UI子物体的布局(不是Size，因为没有满足前两条)
 *                          所以要满足 Layout Element 的分配顺序：Vertical Layout Group需要勾选 childControlHeight 和 childForceExpandHeight，另外两个如要保持分辨率，建议勾选 childControlWidth，这个看情况 childForceExpandWidth
 *                                                              Horizontal Layout Group需要勾选 childControlWidth 和 childForceExpandWidth，另外两个如要保持分辨率，建议勾选 childControlHeight，这个看情况 childForceExpandHeight
 *                 3、总结：分配子物体Size的顺序：MinSize、PreferredSize(childControl)、 Flexible Size(childForceExpand)

 *      三、父物体的布局控制器(二):Grid Layout Group
 *                不同于上面的控制器：Grid Layout Group 布局控制器不采用Layout Element分配规则
 *                1、API:
 *                      cellSize   设置子物体的Size,直接规定子物体的宽高(忽略Layout Element分配规则)
 *                      startCorner 子物体起始的位置
 *                      startAxis   子物体增加时，沿着 startAxis 轴增加
 *                2、一般应用(父物体添加Content Size Fitter，下面讲述):
 *                  2.1、固定高度、宽度灵活(子物体的数量改变，子物体的Size不变）Grid Layout Group Constraint: Fixed Row Count
 *                                                                         Content Size Fitter Horizontal Fit: Preferred Size
 *                                                                         Content Size Fitter Vertical Fit: Preferred Size or Unconstrained(Unconstrained则手动设置高度)
 *                  2.2、固定宽度、高度灵活(子物体的数量改变，子物体的Size不变）Grid Layout Group Constraint: Fixed Column Count
 *                                                                         Content Size Fitter Horizontal Fit: Preferred Size or Unconstrained(Unconstrained则手动设置宽度)
 *                                                                         Content Size Fitter Vertical Fit: Preferred Size
 *                  2.3、高度灵活、宽度灵活(子物体的数量改变，子物体的Size不变）Grid Layout Group Constraint: Flexible
 *                                                                         Content Size Fitter Horizontal Fit: Preferred Size or Unconstrained(Unconstrained则手动设置高度)
 *                                                                         Content Size Fitter Vertical Fit: Preferred Size or Unconstrained(Unconstrained则手动设置高度)
 *                      第三个选项：内部会尽量使子物体在 X轴的数量 和 Y轴的数量一致，并且保证不会超过 startAxis 轴方向的父物体
 *      四、Content Size Fitter
 *            1、定义：根据内容控制父物体的Size
 *            2、内容: Image 、Text 、layout groups、Layout Element 
 *            3、API
 *                  horizontalFit   横向适配：Unconstrained 不改变父物体的大小
 *                                           MinSize       根据子物体的 MinSize 总和 +padding +spacing 来设置父物体的大小
 *                                           PreferredSize 根据子物体的 PreferredSize 总和 +padding +spacing 来设置父物体的大小
 *                  verticalFit     纵向适配：和上述一样
 *              特别注意：根据 pivot 位置适配的起点和方向，比如pivot在center，则朝所有方向等比例适配
 *                                                      比如pivot在左上角，则向右和向下进行适配父物体的大小
 *      五、Aspect Ratio Fitter
 *           1、定义：长宽比过滤器，不考虑子物体的 MinSize PreferredSize Flexible Size
 *           2. 根据pivot的位置，改变自身的Size
 *           2、API
 *                aspectMode    分为3类
 *                              第一类：None：忽略本组件
 *                              第二类：Width Controls Height、Height Controls Width
 *                              Width Controls Height：使用宽度控制高度
 *                                                     设置aspectRatio长宽比
 *                              Height Controls Width：使用高度控制宽度
 *                                                     设置aspectRatio长宽比
 *                              第三类：Fit In Parent、Envelope Parent，这两个需要添加在子物体中
 *                              Fit In Parent：子物体根据aspectRatio，自动适配父物体的Size，不超出父物体，来设置自身的Size
 *                              Envelope Parent：子物体根据aspectRatio，自动适配父物体的Size，完全覆盖父物体，来设置自身的Size
 */
using UnityEngine;
using UnityEngine.UI;

public class LayoutLearn : MonoBehaviour
{
    public Text Imgelement;
    public Image ImgLayoutGroup;
    public Image ImgGridLayout;
    public Image ImgContentSize;
    public Image ImgAspect;
    void Start()
    {
        /*
         *  Layout Element
         *  点开image，属性面板中的最下面，点开可以看到左上角有一个上下的选项，选择LayoutProperties
         *  此时可以看到，UI对象隐藏自带的 Layout Element 属性
         *  当然，我们可以给UI添加  Layout Element 组件，用来精确控制子物体的属性值
         */
        LayoutElement element = Imgelement.GetComponent<LayoutElement>();

        /*
         *  通过拉伸verGroup的高度看出效果
         */
        VerticalLayoutGroup verGroup = ImgLayoutGroup.GetComponent<VerticalLayoutGroup>();

        /*
         *  
         */
        GridLayoutGroup gridGruoup = ImgGridLayout.GetComponent<GridLayoutGroup>();

        ContentSizeFitter sizefitter = ImgContentSize.GetComponent<ContentSizeFitter>();
        sizefitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        sizefitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        sizefitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        AspectRatioFitter aspect = ImgAspect.GetComponent<AspectRatioFitter>();
        aspect.aspectMode = AspectRatioFitter.AspectMode.None;
        aspect.aspectRatio = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
