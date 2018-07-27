
using System;
using UnityEngine;
using UnityEngine.UI;

public class Demo3 : MonoBehaviour
{

    private Image ui1;
    private Image ui2;
    private GameObject cube;
    private GameObject sprite;
    private Image img;


    private Text Restext;
    private InputField posX;
    private InputField posY;

    private void Start()
    {

        Restext = GameObject.Find("Restext").GetComponent<Text>();
        posX = GameObject.Find("posX").GetComponent<InputField>();
        posY = GameObject.Find("posY").GetComponent<InputField>();


        ui1 = GameObject.Find("Canvas/ui1").GetComponent<Image>();
        ui2 = GameObject.Find("Canvas/ui2").GetComponent<Image>();
        cube = GameObject.Find("cube");
        sprite = GameObject.Find("sprite");

        posY.onEndEdit.AddListener(PosCallBack);
    }

    private void PosCallBack(string arg0)
    {
        if(posX.text=="" | posY.text=="")return;
        MouseSimulater.MoveTo(float.Parse(posX.text), float.Parse(posY.text));
        //MouseSimulater.LeftClick(float.Parse(posX.text), float.Parse(posY.text));
        Restext.text = "当前Unity分辨率：" +Screen.width + " / " +Screen.height + "当前点击：" + posX.text + " / " + posY.text;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Screen.SetResolution(800, 600, true);
            Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Screen.SetResolution(8000, 6000, true);
            Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Screen.SetResolution(1920, 1080, true);
            Restext.text = "当前Unity分辨率：" + Screen.width + " / " + Screen.height;
        }
    }

    



}
