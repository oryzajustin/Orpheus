using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	////////////////////
	// Controls
	////////////////////
	private KeyCode PrimaryAttack = KeyCode.J;
	private KeyCode SecondaryAttack = KeyCode.K;

	////////////////////
	// Consider using switch case statement
	////////////////////
	private int primaryComboNum = 1;

	////////////////////
	// Floats
	////////////////////
	private float attackTimeCounter = 0f;
	private float attackCd = 0.3f;

	////////////////////
	// Booleans
	////////////////////
	private bool primaryComboActive = false;
	public bool attacking = false;
	public bool grounded = false;

	////////////////////
	// References
	////////////////////
	private Animator anim;

	void Awake() {

		anim = gameObject.GetComponent<Animator>();

	}
	
	void Update () {

		////////////////////
		// Ground only melee moves
		////////////////////
		if(grounded) {

			////////////////////
			// Primary attack - light punch and combo
			////////////////////
			if(Input.GetKeyDown(PrimaryAttack) && !attacking) {
				attacking = true;
		    	attackTimeCounter = 0;
		    	anim.Play("player punch");	

		    	primaryComboActive = true;
		    	primaryComboNum++;

		    	// Sound test
		    	gameObject.GetComponent<AudioSource>().Play();

			}

			////////////////////
			// Primary combo - no CD
			////////////////////
		    else if(Input.GetKeyDown(PrimaryAttack) && primaryComboActive && attacking) {
		    	switch(primaryComboNum) {
		    		case 1: 
				    	primaryComboNum++;
				    	anim.Play("player punch");	
		    			break;
		    		case 2:
						primaryComboNum++;
						anim.Play("player punch 2");
						break;
					case 3:
						primaryComboNum++;
						anim.Play("player punch 3");
						break;
					case 4: // Reset combo and finish attacking
		    			primaryComboActive = false;
						primaryComboNum = 1; 
						anim.Play("player headbutt");
						break;
					default:
						if(primaryComboNum > 4) {
							primaryComboNum = 1;
						}
						else {
					    	primaryComboNum++;
						}
				    	anim.Play("player punch");	
						break;
		    	}
				attackTimeCounter = 0;	
		    }

			////////////////////
			// Secondary attack - heavy punch
			////////////////////
			if(Input.GetKeyDown(SecondaryAttack) && (!attacking || primaryComboActive)) {
				attacking = true;
				primaryComboActive = false;
				attackTimeCounter = 0;	
				anim.Play("player special punch");
			}
		}

		////////////////////
		// Not grounded moves
		////////////////////
		else {

			////////////////////
			// Air kick
			////////////////////
			if(Input.GetKeyDown(PrimaryAttack) && !attacking) {
				attacking = true;
				attackTimeCounter = 0;	
				anim.Play("player airkick");
			}
		}

		////////////////////
		// Check if still attacking, else reset all counters/states
		////////////////////
		if(attacking) {
			if(attackTimeCounter < attackCd) {
				attackTimeCounter += Time.deltaTime;
			}
			else {
				attacking = false;
				primaryComboNum = 1;
				primaryComboActive = false;
				attackTimeCounter = 0;
			}
		}

		////////////////////
		// Set attacking to last move used
		////////////////////
		anim.SetBool("Attacking", attacking);

	}	

}