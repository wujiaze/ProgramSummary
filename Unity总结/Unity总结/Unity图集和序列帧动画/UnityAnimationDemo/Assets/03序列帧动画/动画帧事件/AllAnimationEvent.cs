using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAnimationEvent : MonoBehaviour {

    public GameObject cube;
    void Set() {
        cube.transform.position = new Vector3(1,1,1);
        Debug.Log("触发事件");
    }
    void SettFloat(float a)
    {
        Debug.Log("触发事件" + a);
    }
    void SetInt(int b)
    {
        Debug.Log("触发事件" + b);
    }
    
    void SetString(string c)
    {
        Debug.Log("触发事件" + c);
    }
    public enum MyEnum
    {
        dddddd
    }
    // 这个Object是UnityEngine中的 
    void SetObject(Object o)
    {
        //Debug.Log((GameObject)o);
        //string name = ((AnimationEventScripts)o).gameObject.transform.Find("Cube").name;
        //Debug.Log("触发事件" + name);
    }
}
