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

	//rigidbody
	public Rigidbody2D beastrb;
	private Animator anim;

	// Use this for initialization
	void Start () {
		grounded = true;
		madTime = 3f;
		isMad = false;
		anim = gameObject.GetComponent<Animator>();
		beastrb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//wait out the charge time before actually charging
		StartCoroutine(WaitB4Charge(madTime));
		//actually charge
		if(grounded && !isMad){
     		beastrb.AddForce(Vector2.left * charge);
     	}
     }
     
	IEnumerator WaitB4Charge(float time){
 		//do whatever needs to be done before waiting
    	anim.SetBool("isMad", true);
    	yield return new WaitForSeconds(madTime);
    	//do this after the time is up
    	anim.SetBool("isMad", false);
    	charge = 7f;
    	anim.SetFloat("charge", charge);
 	}

}
