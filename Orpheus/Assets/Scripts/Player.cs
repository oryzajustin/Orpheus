using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Floats
	public float maxSpeed = 6;
	public float speed = 100f;
	public float jumpPower = 300f;
	public float friction = 0.7f;
	public float gravity = 0.5f;
	private int currentScale = 2;

	// Stats
	public float health = 100f;
	public float damage = 20f;
	public float maxCharge = 100f;
	public float currentCharge = 0f;
	public float chargeAmount = 10f;
	// One second, 10 sec for max charge
	public float chargeTimer = 1.0f;
	public float chargeTolerance = 2.0f;

	// Booleans
	public bool grounded;
	public bool charging;
	public bool crouching;

	// References
	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start() {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() {
		// Current horizontal speed
		float currentSpeed = Mathf.Abs(rb2d.velocity.x);

		// Set animation parameters
		anim.SetBool("Grounded", grounded);
		anim.SetBool("Charge", charging);
		anim.SetFloat("Speed", currentSpeed);

		// Change sprite direction
		if(Input.GetAxis("Horizontal") < -0.1f) {
			transform.localScale = new Vector3(-(currentScale), currentScale, 1);
		}
		if(Input.GetAxis("Horizontal") > 0.1f) {
			transform.localScale = new Vector3(currentScale, currentScale, 1);
		}

		// Jump (single only)
		if(Input.GetButtonDown("Jump") && grounded) {
			rb2d.AddForce(Vector2.up * jumpPower);
		}

		// Crouch, stop movement 
		if(Input.GetKeyDown("s") && grounded) {
			crouching = true;
			anim.SetBool("Crouch", crouching);
		}

		if(Input.GetKeyUp("s")) {
			crouching = false;
			anim.SetBool("Crouch", crouching);
		}

		// Energy charge (left shift)
		if(Input.GetKeyDown(KeyCode.LeftShift) && grounded && currentSpeed <= chargeTolerance) {

			charging = true;
			anim.SetBool("Charge", charging);

			anim.Play("player charge");
		}

		if(Input.GetKeyUp(KeyCode.LeftShift) || !grounded || currentSpeed > chargeTolerance) {

			charging = false;
			anim.SetBool("Charge", charging);
		}

		// Charging check
		if(charging) {
			if(chargeTimer > 0) {
				chargeTimer -= Time.deltaTime;
			}
			else {
				if(currentCharge < 100) {
					currentCharge += chargeAmount;
				}
				// Reset timer
				chargeTimer = 1.0f;
			}
		}
				
	}

	void FixedUpdate() {
		float horizontal = Input.GetAxis("Horizontal");
		// Gravity and friction
		Vector3 easeVelocity = rb2d.velocity;

		// Moving the player
		rb2d.AddForce((Vector2.right * speed) * horizontal);

		// Limit max speed
		if(rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
		}

		if(rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
		}

		// Fake friction
		if(grounded) {
			// Doesn't affect fall speed, z axis isn't used, x is 75% of speed
			easeVelocity.z = 0.0f;
			easeVelocity.y = rb2d.velocity.y;
			easeVelocity.x *= friction;
			rb2d.velocity = easeVelocity;
		}

		if(crouching) {
			rb2d.velocity = Vector3.zero;
		}
	}
}
