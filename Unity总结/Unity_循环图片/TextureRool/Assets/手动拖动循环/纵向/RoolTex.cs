using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoolTex : MonoBehaviour
{

    public GameObject ItemPrefab;
    private RectTransform _contentRectTransform;
    private float _contentHeight;
    private List<GameObject> showItem;
    private Queue<GameObject> Itempool;
    void Start ()
	{
	    _contentRectTransform = transform.GetComponent<RectTransform>();
	    _contentHeight = _contentRectTransform.rect.height;
	    showItem = new List<GameObject>();
        

    }
	
	void Update () {
		
	}

    private void CreatItem()
    {
        GameObject newItem = Instantiate(ItemPrefab, transform);
        RectTransform newItemTrans = newItem.GetComponent<RectTransform>();
        newItemTrans.sizeDelta = new Vector2(newItemTrans.sizeDelta.x, Random.Range(80, 200));
    }

    
}
