using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class myShake : MonoBehaviour {

    public Camera camera;
	void Start () {
        // duration:震动持续时间  strength:震动的强度 vibrato：震动的频率 randomness：随机性 fadeout ：是否渐弱
        camera.DOShakePosition(1, 3, 10, 90, true); // 后面的参数用默认的就可以了

        // strength:表示震动的强度和震动的方向
        camera.DOShakePosition(1, new Vector3(2, 2, 0), 10, 90, true);
       

    }

	// Update is called once per frame
	void Update () {
		
	}
}
