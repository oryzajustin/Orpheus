using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

	////////////////////
	// Integers
	////////////////////
	public int currentHealth;
	public int maxHealth = 50;

	////////////////////
	// Floats
	////////////////////
	public float distance;
	public float awakeRange = 10f;
	public float attackRange = 3f;
	public float attackInterval = 2f;
	public float attackCd;
	public float maxSpeed = 4f;
	public float leapCd;

	////////////////////
	// Booleans
	////////////////////
	public bool awake = false;
	public bool attacking = false;
	public bool leap = false;
	public bool attackReady = false;

	////////////////////
	// References
	////////////////////
	private Rigidbody2D rb2d;
	public Transform target;
	public Transform attackPoint;
	public Animator anim;
	public Collider2D WolfAttack;

	void Awake() {

		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();

		GameObject objWolfAttack = GameObject.FindWithTag("AttackHitbox");
 		if(objWolfAttack != null) {
 			WolfAttack = objWolfAttack.GetComponent<Collider2D>();
 		}

 		WolfAttack.enabled = false;

	}

	void Start() {
		currentHealth = maxHealth;
 		
	}

	void Update() {
		float velocity = rb2d.velocity.x;
		float speed = Mathf.Abs(velocity);
		distance = Vector2.Distance(transform.position, target.transform.position);

		// Set animator parameters
		anim.SetBool("Awake", awake);
		anim.SetFloat("Speed", speed);
		anim.SetBool("Attacking", attacking);

		// Change sprite direction
		if(velocity < -0.1f) {
			transform.localScale = new Vector3(1, 1, 1);
		}
		if(velocity > 0.1f) {
			transform.localScale = new Vector3(-1, 1, 1);
		}

		AwakeCheck(distance);

		// Reset last hitbox
		WolfAttack.enabled = false;

		if(distance < attackRange && !attacking) {
			anim.SetBool("AttackReady", true);
			attackReady = true;
			// Stop movement
			rb2d.velocity = Vector3.zero;
			Attack();
		}

		else {
			anim.SetBool("AttackReady", false);
			attackReady = false;
		}
	}

	void FixedUpdate() {



		if(awake) {
			Vector2 direction = target.transform.position - transform.position;

			// Move wolf in player direction
			if(!attackReady) {
				rb2d.AddForce(direction * 5);
			}

			// Limit max speed
			if(rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
			}

			if(rb2d.velocity.x < -maxSpeed) {
				rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
			}

			if(leap) {

				direction.Normalize();
				// Debug.Log(direction.x);
				// Facing left
				if(direction.x < -0.1) {
					rb2d.AddForce(Vector2.left * 100);
				}
				else {
					rb2d.AddForce(Vector2.right * 100);
				}

			}

					Vector3 easeVelocity = rb2d.velocity;
		// Fake friction
		easeVelocity.z = 0.0f;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.x *= 0.6f;
		rb2d.velocity = easeVelocity;
		}
	}

	void AwakeCheck(float distance) {
		
		// In range
		if(distance < awakeRange) {
			awake = true;
		}

		// Not in range
		if(distance > awakeRange) {
			awake = false;
		}

	}

	public void Attack() {

		attackCd += Time.deltaTime;
		leapCd += Time.deltaTime;

		if(attackCd >= attackInterval) {
			anim.SetBool("AttackReady", false);
			attackReady = false;
			anim.SetBool("Attacking", true);

			leap = true;

			Debug.Log("Wolf attack");

			anim.Play("Wolf_Attack");
			WolfAttack.enabled = true;
			// Can't attack multiple times in a row
			attackCd = 0f;

		}
		anim.SetBool("Attacking", false);

		// Need leap timer
		if(leapCd >= 3) {
			leap = false;
			leapCd = 0;
			
		}
		// attacking = false;
		// WolfAttack.enabled = false;

	}

}
