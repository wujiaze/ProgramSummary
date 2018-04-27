
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class MovieController : MonoBehaviour {

    public VideoPlayer vp;
    private void Awake()
    {
    }
    private void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vp.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            vp.Pause();
        }

    }
}
