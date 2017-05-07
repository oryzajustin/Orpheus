using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	//ints 
	public int currHealth;
	public int maxHealth;

	//floats
	public float distance;
	public float wakeRange = 8;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	//booleans
	public bool awake = false;

	//refs
	public GameObject shot;
	public Transform target;
	public Animator anim;
	public Transform shootPointLeft;

	void Awake(){
		anim = gameObject.GetComponent<Animator>();
	}

	void Start(){
		currHealth = maxHealth;
	}

	void Update(){
		anim.SetBool("Awake", awake);

		RangeCheck();

		if(awake) {
			// Attack();
			
		}
	}

	void RangeCheck(){
		distance = Vector3.Distance(transform.position, target.transform.position);
		if(distance < wakeRange){
			awake = true;
		}
		else{
			awake = false;
		}
	}

	public void Attack(bool attackingLeft){
		bulletTimer += Time.deltaTime;
		if(bulletTimer >= shootInterval){
			Vector2 direction; 
			direction = target.transform.position - transform.position;
			direction.Normalize();
			if(attackingLeft){
				GameObject bulletClone = Instantiate(shot, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			}
		}
	}


}
