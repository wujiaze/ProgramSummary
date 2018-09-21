using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionTest : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Screen.SetResolution(Consts.Width, Consts.Height, false);
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            canvasScaler.referenceResolution = new Vector2(Consts.Width, Consts.Height);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError(Screen.width + " " + Screen.height);
            Debug.LogError(canvasScaler.referenceResolution);
        }
    }
}
