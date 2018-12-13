/*
 *      主题： 对象的真实尺寸的获取和模型导入时的影响因素
 *
 *      前提：Unity中的基本单位是 1m ,当scale为(1,1,1)时，cube的长宽高是(1m,1m,1m) ,其他形状的长宽高以此类推
 *
 *      真实尺寸的获取
 *      一、3D
 *          方法一、Mesh.bounds         这个表示对象基础网格的范围大小，不会随着 旋转、移动、缩放而改变，即不受 Transform 影响
 *          方法二、MeshRenderer.bounds 这个表示对象渲染器的范围大小    根据 Transform 改变而改变
 *          方法三、Collider.bounds     这个表示对象碰撞器的范围大小    根据 Transform 改变而改变
 *
 *          同时，MeshRenderer 的尺寸可以通过渲染器改变 就像 Collider 的尺寸可以改变一样，这就导致了有时候不准确
 *          所以，推荐使用
 *                 Mesh.bounds.size(不变的范围)* Tranform.scale 来确认大小，对象的位置和旋转就直接采用 Transform
 *          Mesh.bounds.size  -->  x:x轴上的长度 y:y轴上的长度 z:z轴上的长度
 *      二、2D
 *          方法一、meshRenderer.size           这个表示对象渲染器的范围大小    根据 Transform 改变而改变
 *          方法二、meshRenderer.bounds.size    这个表示对象渲染器的范围大小    根据 Transform 改变而改变
 *          方法三、collider.bounds.size        这个表示对象碰撞器的范围大小    根据 Transform 改变而改变，并且不能旋转，这就导致了渲染尺寸和碰撞体尺寸不同
 *
 *          获取真实尺寸，推荐使用
 *                  meshRenderer.size
 *          得到原图的分辨率，可以使用
 *                  meshRenderer.sprite.rect
 *      三、UI
 *          获取Sprite尺寸
 *              sprite.rect  不受打包影响，不随image组件改变
 *          获取Image尺寸
 *              image.rectTransform.rect  不受 Transform 影响 和 锚点影响
 *          所以，要计算真实尺寸时： image.rectTransform.rect.size * Tranform.scale
 *
 *          特别注意：sprite的尺寸和 Image组件的尺寸没有关联，但是一般都让 Image 和 sprite 相匹配
 *
 */
using UnityEngine;
using UnityEngine.UI;

public class TransfornAndScaleTest : MonoBehaviour
{
    // 3D
    public GameObject cube;
    // 2D
    public GameObject _2D;

    public Image image;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Mesh 的大小不受 Transform 的影响(position rotation scale)
            MeshFilter meshFilter = cube.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.mesh;
            print(mesh.bounds.center);
            print(mesh.bounds.extents);
            print(mesh.bounds.max);
            print(mesh.bounds.min);
            print(mesh.bounds.size); // x:x轴上的长度 y:y轴上的长度 z:z轴上的长度

            print("MeshRenderer ");
            MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();
            print(meshRenderer.bounds.center);
            print(meshRenderer.bounds.extents);
            print(meshRenderer.bounds.max);
            print(meshRenderer.bounds.min);
            print(meshRenderer.bounds.size);

            print("Collider ");
            Collider collider = cube.GetComponent<Collider>();
            print(collider.bounds.center);
            print(collider.bounds.extents);
            print(collider.bounds.max);
            print(collider.bounds.min);
            print(collider.bounds.size);

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            print("SpriteRenderer ");
            SpriteRenderer meshRenderer = _2D.GetComponent<SpriteRenderer>();
            print(meshRenderer.bounds.center);
            print(meshRenderer.bounds.extents);
            print(meshRenderer.bounds.max);
            print(meshRenderer.bounds.min);
            print(meshRenderer.bounds.size);
            print(meshRenderer.size);
            // Sprite
            Sprite sprite = meshRenderer.sprite;
            print(sprite.bounds);
            print(sprite.bounds.size);
            print(sprite.border);       // 精灵原图的边框：和上下左右的距离
            print(sprite.rect);

            print("Collider ");
            Collider2D collider = _2D.GetComponent<Collider2D>();
            print(collider.bounds.center);
            print(collider.bounds.extents);
            print(collider.bounds.max);
            print(collider.bounds.min);
            print(collider.bounds.size);

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Image 组件
            print(image.rectTransform.rect);        // 自身矩形的size，不受 Transform(位置、旋转、Sclae)无关
            print(image.rectTransform.rect.size);
            print(image.rectTransform.sizeDelta);   // 不受 Transform(位置、旋转、Scale)无关，但是受锚点影响
            // Sprite
            Sprite sprite = image.sprite;
            print(sprite.bounds);
            print(sprite.bounds.size);
            print(sprite.border);       // 精灵原图的边框：和上下左右的距离
            print(sprite.rect);         // 精灵原图的矩形size

            // texture
            Texture2D tex = sprite.texture;
            // 情况1、单独的一张图
            print(tex.width);
            print(tex.height);
            // 情况2、打包在一个图集中 ,获取的就是打包得到的大图的分辨率了
            print(tex.width);
            print(tex.height);
        }
    }
}
