using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour {

	//boolean for if the beast is mad
	private bool isMad;
	//check for if grounded
	private bool grounded;
	//the charging speed of the beast
	private float charge;
	//how long the beast flashes for
	private float madTime;
	public bool collide;
	public bool trigger;
	private float initialpos;
	private float currentpos;

	//rigidbody
	public Rigidbody2D beastrb;
	private Animator anim;

	// Use this for initialization
	void Start () {
		grounded = true;
		collide = false;
		trigger = false;
		madTime = 3f;
		isMad = false;
		anim = gameObject.GetComponent<Animator>();
		beastrb = gameObject.GetComponent<Rigidbody2D>();
		initialpos = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		//actually charge
		if(trigger == true){
     		StartCoroutine(WaitB4Charge(madTime));
     	}
		if(grounded && !isMad && !collide){
     		beastrb.AddForce(Vector2.left * charge);
     	}
     	else{
     		beastrb.velocity = Vector2.zero;
     	}
     }

	IEnumerator WaitB4Charge(float time){
 		//do whatever needs to be done before waiting
    	anim.SetBool("isMad", true);
    	anim.SetBool("collide", false);
    	yield return new WaitForSeconds(madTime);
    	//do this after the time is up
    	anim.SetBool("isMad", false);
    	charge = 10f;
    	anim.SetFloat("charge", charge);
    	anim.SetBool("collide", false);
    	collide = false;
    	trigger = false;
    	anim.SetBool("trigger", false);
 	}


    void OnCollisionEnter2D(Collision2D col){
     	anim.SetFloat("charge", 0f);
     	charge = 0f;
     	collide = true;
     	anim.SetBool("collide", true);
     	anim.SetBool("trigger", false);
     	transform.position = new Vector2(initialpos,-3.95f);     	
    }
}
