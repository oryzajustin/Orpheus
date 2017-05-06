using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Beat : MonoBehaviour
{

    //Object Physics
    private Rigidbody2D rbdy;
    public Collider2D beat_Collider;

    //Speeds of the beats
    private float speed;
    private float[] bpm = new float[] { 130f, 120f, 141f, 166.76f, 90.15f, 140.01f, 129.95f, 149.81f };
    /* Songs in order
        0.  Destractor
        1.  BossMain
        2.  ChipTune
        3.  Mercury
        4.  Jump to Win
        5.  Pocket Destroyer
        6.  Unending Strike
        7.  Venus
     */
    public int song;

    //Location and orientation of the new beats to be spawned
    private Vector2 loc;
    private Quaternion rot;

    // Use this for initialization
    void Start()
    {
        //Colliders
        rbdy = GetComponent<Rigidbody2D>();
        beat_Collider = GetComponent<Collider2D>();

        //Spead of beats
        song = 7;
        speed = bpm[song] / 15;

        //Location for new beats to be spawned
        loc = new Vector2(10.5f, -3.35f);
        rot = new Quaternion(0, 0, 0, 0);

        //Calls Instantiate function for 0.5s and repeats every 0s
        InvokeRepeating("Instantiate", 60 / bpm[song], 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Setting speed of the beats
        float hor = 0 - speed;
        float vert = 0;
        Vector2 beat = new Vector2(hor, vert);
        rbdy.velocity = beat;
    }

    //Deletes the beats once it hits the end of the music bar
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Music_Bar")
        {
            Destroy(gameObject);
        }
    }

    //Creates new bubbles at the specified location
    private void Instantiate()
    {
        Instantiate(gameObject, loc, rot);
    }
}
