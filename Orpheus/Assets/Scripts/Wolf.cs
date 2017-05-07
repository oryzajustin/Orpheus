using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

	////////////////////
	// Integers
	////////////////////
	public int currentHealth;
	public int maxHealth = 10;

	////////////////////
	// Floats
	////////////////////
	public float distance;
	public float speed = 12f;
	public float maxSpeed = 4f;
	public float leapPower = 100f;
	public float awakeRange = 12f;
	public float attackRange = 5f;
	public float attackWaitCounter = 0f;
	public float attackWaitDuration = 4f;
	public float attackDurationCounter = 0f;
	public float attackDuration = 0.2f;
	public float friction = 0.6f;
	public float deathTimer = 3f;

	////////////////////
	// Booleans
	////////////////////
	public bool awake = false;
	public bool attacking = false;
	public bool leap = false;
	public bool attackReady = false;
	public bool knockbacked = false;
	public bool damaged = false;

	////////////////////
	// References
	////////////////////
	public Rigidbody2D rb2d;
	public Transform target;
	public Transform attackPoint;
	public Animator anim;
	public Collider2D WolfAttack;

	void Awake() {

		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();

	}

	void Start() {

		currentHealth = maxHealth;
 		
	}

	void Update() {

		////////////////////
		// Get wolf speed
		////////////////////
		float velocity = rb2d.velocity.x;
		float speed = Mathf.Abs(velocity);
		distance = Vector2.Distance(transform.position, target.transform.position);

		////////////////////
		// Set animator parameters
		////////////////////
		anim.SetBool("Awake", awake);
		anim.SetFloat("Speed", speed);
		anim.SetBool("Attacking", attacking);

		////////////////////
		// No moving if hit
		////////////////////
		if(!knockbacked) {
			////////////////////
			// Change sprite direction
			////////////////////
			if(velocity < -0.1f) {
				transform.localScale = new Vector3(1, 1, 1);
			}
			if(velocity > 0.1f) {
				transform.localScale = new Vector3(-1, 1, 1);
			}

			////////////////////
			// Update awake
			////////////////////
			AwakeCheck(distance);

			////////////////////
			// Attack
			////////////////////
			if(distance <= attackRange) {
				attackReady = true;
				anim.SetBool("AttackReady", attackReady);
				rb2d.velocity = Vector3.zero; // Stops moving
				if(!attacking) {
					if(attackWaitCounter < attackWaitDuration) {
						attackReady = true;
						attackWaitCounter += Time.deltaTime;
					}
					else {
						attackWaitCounter = 0;
						attackReady = false;
						anim.SetBool("AttackReady", attackReady);
						attacking = true;
						anim.SetBool("Attacking", attacking);

						anim.Play("Wolf_Attack");
					}
				}
				if(attacking) {
					if(attackDurationCounter < attackDuration) {
						attackDurationCounter += Time.deltaTime;
					}
					else {
						attacking = false;
						attackDurationCounter = 0;
					}
				}
			}
			else {
				attackReady = false;
				anim.SetBool("AttackReady", attackReady);
			}
		}

		if(currentHealth <= 0 && !damaged) {
			anim.Play("Wolf_Damaged");
			damaged = true;
		}

	}

	void FixedUpdate() {

		if(awake && !knockbacked && !damaged) {

			////////////////////
			// Get direction of player
			////////////////////
			Vector2 direction = target.transform.position - transform.position;

			////////////////////
			// Move wolf in player direction, stop if ready to attack
			////////////////////
			if(!attackReady) {
				rb2d.AddForce(direction * speed);
			}
			else {
				rb2d.velocity = Vector3.zero;
			}

			////////////////////
			// Limit max speed
			////////////////////
			if(rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
			}
			if(rb2d.velocity.x < -maxSpeed) {
				rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
			}

			////////////////////
			// Make wolf leap forward during attacks
			////////////////////
			if(attacking) {
				rb2d.AddForce(direction * leapPower);
			}

			////////////////////
			// Generate fake friction
			////////////////////
			Vector3 frictionVector = rb2d.velocity;
			frictionVector.x *= friction;
			frictionVector.y = rb2d.velocity.y;
			frictionVector.z = 0f;
			rb2d.velocity = frictionVector;
		}

		if(damaged) {
			deathTimer -= Time.deltaTime;
			rb2d.velocity = Vector3.zero;
		}
		if(deathTimer <= 0f) {
			Die();
		}

	}

	////////////////////
	// Check player distance
	////////////////////
	void AwakeCheck(float distance) {

		if(distance <= awakeRange) {
			awake = true;
		}
		if(distance > awakeRange) {
			awake = false;
		}

	}

	private void Die() {

		Object.Destroy(this.gameObject);

	}

	////////////////////
	// Allows wolf to be damaged by other scripts
	////////////////////
	public void Damage(int dmg) {

		currentHealth -= dmg;
		
	}

	////////////////////
	// Knockback, fall on spot wolf.position.y is frozen
	////////////////////
	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir) {

		if(!damaged); {
			anim.Play("Wolf_Knockback");
		}
		float timer = 0f;
		while(knockDur > timer) {
			knockbacked = true;
			timer += Time.deltaTime;
			// rb2d.AddForce(new Vector3(knockbackDir.x * (knockbackPwr), knockbackDir.y * knockbackPwr, transform.position.z));
			rb2d.velocity = Vector3.zero;
		}
		knockbacked = false;

		yield return 0;

	}

}
