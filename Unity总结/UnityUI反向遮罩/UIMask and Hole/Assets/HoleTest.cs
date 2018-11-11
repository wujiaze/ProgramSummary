/*
 *     遮罩和反向遮罩
 *
 *     遮罩：父物体添加遮罩，可以遮住子物体,反过来是不行的，因此就出现了反向遮罩
 *     反向遮罩：通过改变shader内部，来达到目的，使用方法和遮罩是一样的
 *
 *     遮罩做法：
 *          1、父物体添加遮罩图像和Mask组件        (添加顺序不能反)
 *          2、子物体任意添加图像(用于测试)
 *          3、此时子物体只有在父物体的遮罩内部才能显示，其余就如何不存在(看上去不显示，但实际上是存在物体的)
 *
 *     反向遮罩：添加Tool文件夹中的两个文件
 *          1、父物体添加遮罩图像(Image和HoleImage都行)和Hole组件   (添加顺序不能反)
 *          2、子物体添加任意图像(HoleImage)
 *          3、此时，子物体在父物体的遮罩内部是不能显示的，
 *             若将父物体的Hole组件的 Show Mask Graphic 取消勾选，不显示遮罩图像，那么就好像子物体的某一部分被"挖"走了,如同不存在一样
 *      
 *
 */
using UnityEngine;

public class HoleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
