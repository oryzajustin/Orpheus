using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastFaceCollider : MonoBehaviour {

	private Rigidbody2D beastrb;

	void Start() {
		beastrb = gameObject.GetComponentInParent<Rigidbody2D>();
	}

//	void OnTriggerEnter2D(Collider2D col) {
//		beastrb.AddForce(Vector2.right * 2000f);
//	}

//	void OnTriggerStay2D(Collider2D col) {
// 		beast.grounded = true;
// 	}

// 	void OnTriggerExit2D(Collider2D col) {
// 		beast.grounded = false;
// 	}
}
