using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	////////////////////
	// References
	////////////////////
	private Player player;
	private PlayerAttack playerAttack;
	public Collider2D playerGround;

	void Start() {

		player = gameObject.GetComponentInParent<Player>();
		playerAttack = gameObject.GetComponentInParent<PlayerAttack>();

		////////////////////
		// Find ground for player, prevent collision w/ hitboxes, and other 2D triggers
		////////////////////
		GameObject objGround = GameObject.FindWithTag("Ground");		
		if(objGround != null) {
			playerGround = objGround.GetComponent<Collider2D>();
		}

	}

	void OnTriggerEnter2D(Collider2D col) {

		if(col == playerGround) {
			player.grounded = true;
			playerAttack.grounded = true;
		}

	}

	void OnTriggerStay2D(Collider2D col) {

		if(col == playerGround) {
			player.grounded = true;
			playerAttack.grounded = true;
		}

	}

	void OnTriggerExit2D(Collider2D col) {

		if(col == playerGround) {
			player.grounded = false;
			playerAttack.grounded = false;
		}
		
	}

}
