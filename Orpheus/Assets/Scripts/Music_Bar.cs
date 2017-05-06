using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Bar : MonoBehaviour
{

    public Collider2D bar_Collider;
    public CircleCollider2D cir_Collider;

    // Use this for initialization
    void Start()
    {
        bar_Collider = GetComponent<Collider2D>();
        cir_Collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Detect if the space key is pressed at the correct time
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown("space"))
        { //Detect space key
            //Debug.Log("boi boi boi");
            //Enter code for combo here
        }
    }
}
