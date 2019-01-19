using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Camera camera;
	void Start () {
	   

    }
	
	// Update is called once per frame
	void Update () {
	    print(camera.aspect);
	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        camera.aspect = 1;
            //camera.ResetAspect();
	    }
    }
}
