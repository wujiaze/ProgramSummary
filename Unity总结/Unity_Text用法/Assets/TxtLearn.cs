/*
 *      Text
 *
 *      属性:
 *             Font Size : 字体的大小
 *             Alignment : 字体的对齐方式
 *             Align By Geometry :字体的横向对齐通过几何，一般用于图文混排
 *             Horizontal Overflow : 文字内容横向溢出Text矩形时，采用的方式
 *                                   1、Warp:        换行
 *                                   2、Overflow:    横向溢出    (溢出的方向,根据 Alignment 的纵向对齐位置)
 *             Vertical Overflow :  文字内容纵向溢出Text矩形时，采用的方式
 *                                  1、Truncate:     截断:（即溢出的文字看不见）
 *                                  2、Overflow：    纵向溢出     (溢出的方向,根据 Alignment 的纵向对齐位置)
 *             Best Fit : 使字体大小忽略  Font Size 的设置，自动适应Text矩形的大小
 *                        Min Size  最小的字体大小(一般为1)
 *                        Max Size  最大的字体大小
 *   字体大小的调整方法：
 *          一、不使用Best Fit
 *                  Horizontal Warp     当横向换行时
 *                      Vertical Truncate  纵向是截断                             见Text1
 *                      Vertical Overflow  纵向是溢出                             见Text2
 *
 *                  Horizontal Overflow 当横向溢出时，说明横向是一行
 *                      Vertical Truncate  纵向是截断(当字体的高度超过Text的高度)   见Text3
 *                      Vertical Overflow  纵向是溢出                             见Text4
 *
 *          二、使用 Best Fit
 *                  Horizontal Warp     当横向是换行时，矩形框 横向 改变大小 会   调整字体的大小，使字体不会自动换行，使其全部处于矩形中
 *                                      
 *                  Horizontal Overflow 当横向是溢出时，矩形框 横向 改变大小 不会 调整字体的大小，使字体不会自动换行
 *
 *                  Vertical Truncate   当纵向是截断时，矩形框 纵向 改变大小 会   调整字体的大小，使字体不会自动换列，使其全部处于矩形中    
 *                                      
 *                  Vertical Overflow   当纵向是溢出时，矩形框 纵向 改变大小 不会 调整字体的大小，使字体不会自动换行
 *
 *                  Horizontal Warp
 *                          Vertical Truncate                                   见Text5
 *                          Vertical Overflow                                   见Text6
 *                  Horizontal Overflow
 *                          Vertical Truncate                                   见Text7
 *                          Vertical Overflow                                   见Text8
 *          三、使用ContenSizeFitter
 *              一、不使用Best Fit       见 Text9
 *                      1、当内容 和 FontSize 设置好之后:(Text 的Layout Properties 的 Perferred Width 就已经固定下来了)
 *                         此时，选择 ContenSizeFitter 的 Horizontal Fit 为Perferred Size时，就会使用 Perferred Width
 *                         这个时候，字体内容就被拉成了一条直线,致使 Horizontal Warp 或者 Horizontal Overflow 都是同一种效果
 *
 *                      2、当横向确定以后，纵向的 Perferred Height 也是根据 内容和FontSize 确定
 *                         此时，选择 ContenSizeFitter 的 Vertical Fit 为Perferred Size时，就会使用 Perferred Height
 *                         这个时候，Vertical Truncate 或者 Vertical Overflow 都是同一种效果
 * 
 *               二、使用 Best Fit     
 *                      跟 不使用Best Fit  效果一样   见 Text10
 *
 *         重要提示： 1、对比  ContenSizeFitter 和   Best Fit 发现
 *                      Best Fit：固定的大小，改变字体适应
 *                      ContenSizeFitter：固定的内容，改变矩形来适应字体
 *                   2、当 Font Size 固定 ，Horizontal Warp 时
 *                         当文字内容中的任何地方添加了任意个 " " 后，整体的文字内容超过了矩形的宽度，就会根据空格自动换行，而不是添加任意个空格
 *                         原因：选择了 Horizontal Warp，当行的长度超过了矩形的宽度，那么就会换行
 *                  
 *                  解决方法：1、加空格的地方 使用 "\u3000" 代替
 *                  
 */
