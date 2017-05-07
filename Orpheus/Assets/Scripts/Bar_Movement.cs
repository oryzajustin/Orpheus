using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_Movement : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = UnityEngine.Camera.main.transform.position + new Vector3(7.9f,0,0);
    }
}
