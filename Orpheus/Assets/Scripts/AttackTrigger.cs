using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int dmg = 20;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.isTrigger != true && col.CompareTag("Enemy")) {
			col.SendMessageUpwards("Damage", dmg);
		}
	}	

	// For enemy
	// public void Damage(int damage) {
	// 	currentHealth -= damage;
	// 	gameObject.GetComponent<Animation>().Play("Player_RedFlash");
	// }
	// Then check current health, if >= 0, Destroy(gameObject);
}
