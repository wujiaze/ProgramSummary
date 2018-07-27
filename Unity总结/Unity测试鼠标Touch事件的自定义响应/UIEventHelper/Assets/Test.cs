
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    [SerializeField]
    private Button btn;
    public GameObject obj2d;
    public GameObject cube;
    public Image img;
    void Start()
    {
        //EventTriggerListener.Bind(txt.gameObject).onClick = OnClickHandle;
        //EventTriggerListener.Bind(btn.gameObject).onClick = OnClickHandle;
        //EventTriggerListener.Bind(obj2d.gameObject).onClick = OnClickHandle;
        //EventTriggerListener.Bind(cube.gameObject).onClick = OnClickHandle;
        //EventTriggerListener.Bind(img.gameObject).onClick = OnClickHandle;
        //EventTriggerListener.Bind(obj2d.gameObject).onClick = OnClickHandle;
        // 返回全屏支持的分辨率
        //Screen.SetResolution(1920,1080,true);
        //Screen.SetResolution(1920, 1080, false);
        //Screen.SetResolution(640, 480, true);
        //Screen.SetResolution(640, 480, false);
        //MouseSimulater.LeftClick(320, 240);
        //Screen.fullScreen = true;
        //Debug.Log(Screen.fullScreen);
    }

    public void OnClickHandle(GameObject go)
    {
        Debug.Log("点击" + go.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Resolution[] res = Screen.resolutions;
            Screen.SetResolution(res[res.Length-1].width, res[res.Length - 1].height, true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("1111");
            MouseSimulater.LeftClick(960, 540);
        }
    }
    protected Camera m_EventCamera;
    public  Camera eventCamera
    {
        get
        {
            if (m_EventCamera == null)
                m_EventCamera = GetComponent<Camera>();
            return m_EventCamera == Camera.main ? Camera.main : m_EventCamera;
        }
    }
}
