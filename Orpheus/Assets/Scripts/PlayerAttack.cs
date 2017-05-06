using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	// Controls
	private string primaryAttackKey = "j";
	private string secondaryAttackKey = "k";

	// Check active combo and next move
	public bool grounded;
	private bool attacking = false;
	private int primaryComboNum = 1;

	// Controls duration of attacks
	private float attackTimer;
	private float attackCd = 0.1f;

	// Attack hitboxes
	public Collider2D MainPunch1;
	public Collider2D MainPunch2;
	public Collider2D MainPunch3;
	public Collider2D Headbutt;
	public Collider2D SpecialPunch1;
	public Collider2D AirKick;

	private Animator anim;

	void Awake() {
		anim = gameObject.GetComponent<Animator>();

		// Get hitboxes
		GameObject objMainPunch1 = GameObject.FindWithTag("MainPunch1");
 		if(objMainPunch1 != null) {
 			MainPunch1 = objMainPunch1.GetComponent<Collider2D>();
 		}

		GameObject objMainPunch2 = GameObject.FindWithTag("MainPunch2");
 		if(objMainPunch2 != null) {
 			MainPunch2 = objMainPunch2.GetComponent<Collider2D>();
 		}

		GameObject objMainPunch3 = GameObject.FindWithTag("MainPunch3");
 		if(objMainPunch3 != null) {
 			MainPunch3 = objMainPunch3.GetComponent<Collider2D>();
 		}

		GameObject objHeadbutt = GameObject.FindWithTag("Headbutt");
 		if(objHeadbutt != null) {
 			Headbutt = objHeadbutt.GetComponent<Collider2D>();
 		}

		GameObject objSpecialPunch1 = GameObject.FindWithTag("SpecialPunch1");
 		if(objSpecialPunch1 != null) {
 			SpecialPunch1 = objSpecialPunch1.GetComponent<Collider2D>();
 		}

		GameObject objAirKick = GameObject.FindWithTag("AirKick");
 		if(objAirKick != null) {
 			AirKick = objAirKick.GetComponent<Collider2D>();
 		}

		// Disable hitbox colliders
		MainPunch1.enabled = false;
		MainPunch2.enabled = false;
		MainPunch3.enabled = false;
		Headbutt.enabled = false;
		SpecialPunch1.enabled = false;
		AirKick.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool("Grounded", grounded);

		// beat = getBeat();

		if(grounded) {
			// Primary combo - Headbutt, this must be at top to check comboNum
			if(Input.GetKeyDown(primaryAttackKey) && primaryComboNum == 4) {
				attacking = true;

				// Reset combo, this is the last move
				primaryComboNum = 1;
				attackTimer = attackCd;	

				// Play anim
				anim.Play("player headbutt");

				// Current hitbox
				Headbutt.enabled = true;
				anim.SetBool("Headbutt", attacking);
			}

			// Primary combo - Punch 3
			if(Input.GetKeyDown(primaryAttackKey) && primaryComboNum == 3) {
				attacking = true;

				// Go to next move
				primaryComboNum++;
				attackTimer = attackCd;	

				anim.Play("player punch 3");

				MainPunch3.enabled = true;
				anim.SetBool("MainPunch3", attacking);
			}

			// Primary combo - Punch 2
			if(Input.GetKeyDown(primaryAttackKey) && primaryComboNum == 2) {
				attacking = true;

				primaryComboNum++;
				attackTimer = attackCd;	

				anim.Play("player punch 2");

				MainPunch2.enabled = true;
				anim.SetBool("MainPunch2", attacking);
			}

			// Primary attack - punch 1
			if(Input.GetKeyDown(primaryAttackKey) && !attacking) {
				attacking = true;

				primaryComboNum++;
				attackTimer = attackCd;	

				anim.Play("player punch");

				MainPunch1.enabled = true;
				anim.SetBool("MainPunch1", attacking);
			}


			// Secondary attack - 'Heavy' punch
			if(Input.GetKeyDown(secondaryAttackKey) && !attacking) {
				attacking = true;

				// No combo moves, but set hitbox duration
				attackTimer = attackCd + 0.3f;	

				anim.Play("player special punch");

				SpecialPunch1.enabled = true;
				anim.SetBool("SpecialPunch1", attacking);
			}
		}

		// Not grounded
		else {
			// Air kick
			if(Input.GetKeyDown("j") && !attacking) {
				attacking = true;

				// No combo moves, but set hitbox duration
				attackTimer = attackCd + 1f;	

				anim.Play("player airkick");

				AirKick.enabled = true;
				anim.SetBool("AirKick", attacking);
			}
		}



		if(attacking) {
			if(attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			}
			else {
				attacking = false;

				// Turn off all hitboxes
				MainPunch1.enabled = false;
				anim.SetBool("MainPunch1", attacking);

				MainPunch2.enabled = false;
				anim.SetBool("MainPunch2", attacking);

				MainPunch3.enabled = false;
				anim.SetBool("MainPunch3", attacking);

				Headbutt.enabled = false;
				anim.SetBool("Headbutt", attacking);

				SpecialPunch1.enabled = false;
				anim.SetBool("SpecialPunch1", attacking);

				AirKick.enabled = false;
				anim.SetBool("AirKick", attacking);
			}
		}

		anim.SetBool("Attacking", attacking);

		}

}
