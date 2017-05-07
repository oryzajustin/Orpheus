using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	////////////////////
	// Controls
	////////////////////
	private KeyCode Left = KeyCode.A;
	private KeyCode Right = KeyCode.D;
	private KeyCode Jump = KeyCode.Space;
	private KeyCode Down = KeyCode.S;
	private KeyCode Charge = KeyCode.LeftShift;

	////////////////////
	// Player sprite scaling
	////////////////////
	private int scale = 2;
	public int maxHealth = 5;
	public int currentHealth;

	////////////////////
	// Floats
	////////////////////
	public float damage = 20f;
	public float speed = 100f;
	public float maxSpeed = 6f;
	public float jumpPower = 300f;
	public float currentCharge = 0f;
	public float maxCharge = 100f;
	public float chargeAmount = 10f;
	public float chargeSpeed = 1.0f; // Every one second
	public float chargeCounter = 0f;
	public float chargeTolerance = 2.0f; // Amount of movement allowed
	public float friction = 0.7f;
	public float deathTimer = 3f;

	////////////////////
	// Booleans
	////////////////////
	public bool grounded = false;
	public bool charging = false;
	public bool crouched = false;
	public bool knockbacked = false;
	public bool dead = false;

	////////////////////
	// References
	////////////////////
	private Rigidbody2D rb2d;
	private Animator anim;

	void Start() {

		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();

		currentHealth = maxHealth;

	}
	
	void Update() {

		////////////////////
		// Current horizontal speed
		////////////////////
		float currentSpeed = Mathf.Abs(rb2d.velocity.x);

		////////////////////
		// Set animation parameters
		////////////////////
		anim.SetFloat("Speed", currentSpeed);
		anim.SetBool("Grounded", grounded);
		anim.SetBool("Charging", charging);
		anim.SetBool("Crouched", crouched);

		if(!knockbacked) {
			////////////////////
			// Change sprite direction based on key direction
			////////////////////
			if(Input.GetKeyDown(Left)) {
				transform.localScale = new Vector3(-scale, scale, 1); // Vector3 for layer ordering w/ z-axis
			}
			if(Input.GetKeyDown(Right)) {
				transform.localScale = new Vector3(scale, scale, 1);
			}

			////////////////////
			// Jump (limited to one)
			////////////////////
			if(Input.GetKeyDown(Jump) && grounded) {
				rb2d.AddForce(Vector2.up * jumpPower);
			}

			////////////////////
			// Crouch, stop all movement 
			////////////////////
			if(Input.GetKeyDown(Down) && grounded) {
				crouched = true;
				anim.SetBool("Crouched", crouched);
			}
			if(Input.GetKeyUp(Down)) {
				crouched = false;
				anim.SetBool("Crouched", crouched);
			}

			////////////////////
			// Toggle charge mode for combo bar
			////////////////////
			if(Input.GetKeyDown(Charge) && grounded && currentSpeed <= chargeTolerance) {
				charging = true;
				anim.SetBool("Charging", charging);
				anim.Play("player charge"); // Go straight into charge, keep Animator clean
			}
			if(Input.GetKeyUp(Charge) || !grounded || currentSpeed > chargeTolerance) {
				charging = false;
				anim.SetBool("Charging", charging); // Leave animation in Animator
			}

			////////////////////
			// Charging logic
			////////////////////
			if(charging) {
				if(chargeCounter < chargeSpeed) {
					chargeCounter += Time.deltaTime; // Check elapsed time since charge started
				}
				else {
					if(currentCharge < maxCharge) {
						currentCharge += chargeAmount;
						if(currentCharge > maxCharge) {
							currentCharge = maxCharge;
						}
					}
					chargeCounter = 0f; // Reset timer
				}
				
			}
		}

		////////////////////
		// Check player health
		////////////////////
		if(currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		if(currentHealth <= 0) {
			anim.Play("player death");
			dead = true;
		}

	}

	void FixedUpdate() {

		if(dead) {
			deathTimer -= Time.deltaTime;
			rb2d.velocity = Vector3.zero;
		}
		if(deathTimer <= 0f) {
			Die();
		}

		////////////////////
		// Move the player, only if they aren't crouched
		////////////////////
		float direction = Input.GetAxis("Horizontal");
		if(!crouched && !knockbacked) {
			rb2d.AddForce((Vector2.right * speed) * direction);
		}

		////////////////////
		// Limit max speed while retaining jump/fall speed
		////////////////////
		if(rb2d.velocity.x > maxSpeed) { // Limit right-wards movement
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
		}
		if(rb2d.velocity.x < -maxSpeed) { // Limit left-wards movement
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
		}

		////////////////////
		// Simulate friction on ground, doesn't affect fall speed
		////////////////////
		Vector3 currentVelocity = rb2d.velocity;
		if(grounded) {
			currentVelocity.x *= friction;
			currentVelocity.y = rb2d.velocity.y;
			currentVelocity.z = 0f;
			rb2d.velocity = currentVelocity;
		}

		////////////////////
		// Crouch stops all movement
		////////////////////
		if(crouched) {
			rb2d.velocity = Vector3.zero;
		}

	}

	////////////////////
	// Restarts the game, use scene manager later
	////////////////////
	void Die() {

		Application.LoadLevel(Application.loadedLevel);

	}

	////////////////////
	// Allows player to be damaged by other scripts
	////////////////////
	public void Damage(int dmg) {

		currentHealth -= dmg;

	}

	////////////////////
	// Knockback
	////////////////////
	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir) {

		anim.Play("player knockback");
		float timer = 0f;
		while(knockDur > timer) {
			knockbacked = true;
			timer += Time.deltaTime;
			rb2d.AddForce(new Vector3(knockbackDir.x * -500, knockbackDir.y * knockbackPwr, transform.position.z));
		}
		knockbacked = false;

		yield return 0;

	}

}
