using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTouchTriggerDemo : MonoBehaviour {

    public void PrintMsg(GameObject go)
    {
        if (go == null)
        {
            print("Null");
        }
        else
        {
            print(go.name);
        }
    }

    public void PrintOk()
    {
        print("Ok");
    }
    public void PrintOver()
    {
        print("Over");
    }
}
