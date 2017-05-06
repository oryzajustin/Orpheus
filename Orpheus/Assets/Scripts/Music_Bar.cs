using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Bar : MonoBehaviour
{

    public Collider2D bar_Collider;
    public CircleCollider2D cir_Collider;

    private static double multiplier = 1; //Current Multiplier
    private const double MISSMULT = 0.5; //Miss multiplier
    private const double HITMULT = 2; //Hit Multiplier
    private int combo = 0;

    private bool hit; //Checks to see if the note has been hit
    private bool onbeat = false;
    private bool firsthit = false;

    // Use this for initialization
    void Start()
    {
        bar_Collider = GetComponent<Collider2D>();
        cir_Collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !onbeat)
        {
            combo = 0;
            multiplier *= MISSMULT;
            Debug.Log(combo);
        }
    }

    //Detect if the space key is pressed at the correct time
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Detect space key
        if (Input.GetKeyDown("space") && !firsthit)
        { //The Beat has been hit
            combo++;
            if (combo % 4 == 0)
            { //Doubles the multiplier for every bar the player fully completes
                multiplier *= HITMULT;
            }
            Debug.Log(combo);
            hit = true;
            onbeat = true;
            firsthit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //The note has not been hit
        if (!hit)
        {
            combo = 0; //Reset the combo
            if (multiplier > 1)
            {
                multiplier *= MISSMULT;
            }
            Debug.Log(combo);
        }
        hit = false;
        onbeat = false;
        firsthit = false;
    }
}
