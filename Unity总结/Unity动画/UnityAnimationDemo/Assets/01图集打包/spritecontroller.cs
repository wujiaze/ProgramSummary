using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class spritecontroller : MonoBehaviour {

    SpriteAtlas sa;
    Image image;
    void Start () {
        sa = Resources.Load<SpriteAtlas>("Normal");
        image = GameObject.Find("Image").GetComponent<Image>();
	}
	
	
	void Update () {
        // 代码动态修改2D图片：基于Sprites Atlas
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.GetComponent<SpriteRenderer>().sprite = sa.GetSprite("box_1");
        }
        // 代码动态修改UI图片：基于Sprites Atlas
        if (Input.GetKeyDown(KeyCode.B))
        {
            image.sprite = sa.GetSprite("box_2");
        }
    }
}
