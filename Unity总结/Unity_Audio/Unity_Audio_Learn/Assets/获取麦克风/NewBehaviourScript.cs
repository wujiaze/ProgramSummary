using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource Source;
    void Start()
    {
        //Source.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        //Source.loop = true;
        foreach (var VARIABLE in Microphone.devices)
        {
            print(VARIABLE);
        }
        
        //while (!(Microphone.GetPosition(null) > 0))
        //{
            
        //}
        //Source.Play();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
