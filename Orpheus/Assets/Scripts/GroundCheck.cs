using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private Player player;
	private PlayerAttack playerAttack;

	void Start() {
		player = gameObject.GetComponentInParent<Player>();
		playerAttack = gameObject.GetComponentInParent<PlayerAttack>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		player.grounded = true;
		playerAttack.grounded = true;
	}

	void OnTriggerStay2D(Collider2D col) {
		player.grounded = true;
		playerAttack.grounded = true;

	}

	void OnTriggerExit2D(Collider2D col) {
		player.grounded = false;
		playerAttack.grounded = false;
	}

}
