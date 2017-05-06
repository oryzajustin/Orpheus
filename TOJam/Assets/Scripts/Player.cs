using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Floats
	public float maxSpeed = 6;
	public float speed = 100f;
	public float jumpPower = 300f;
	public float friction = 0.7f;
	private int currentScale = 2;

	// Booleans
	public bool grounded;
	// public bool canDoubleJump;


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
		// Set animation parameters
		anim.SetBool("Grounded", grounded);
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

		// Change sprite direction
		if(Input.GetAxis("Horizontal") < -0.1f) {
			transform.localScale = new Vector3(-(currentScale), currentScale, 1);
		}
		if(Input.GetAxis("Horizontal") > 0.1f) {
			transform.localScale = new Vector3(currentScale, currentScale, 1);
		}

		// Jump 
		if(Input.GetButtonDown("Jump")) {
			if(grounded) {
				rb2d.AddForce(Vector2.up * jumpPower);
				// canDoubleJump = true;
			}
			// else {
			// 	if(canDoubleJump) {
			// 		canDoubleJump = false;
			// 		// Reset y velocity so can jump up no matter how much force going down

			// 		rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			// 		rb2d.AddForce(Vector2.up * jumpPower / 1.75f);
			// 	}
			// }
		}
	}

	void FixedUpdate() {
		float horizontal = Input.GetAxis("Horizontal");
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
			easeVelocity.y = rb2d.velocity.y;
			easeVelocity.z = 0.0f;
			easeVelocity.x *= friction;
			rb2d.velocity = easeVelocity;
		}
	}
}
