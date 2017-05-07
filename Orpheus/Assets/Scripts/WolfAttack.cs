using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour {

	private Player player;
	public int damage = 1;
	public float knockbackDuration = 0.02f;
	public float knockbackPower = 200f;


	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {

		if(col.CompareTag("PlayerHitbox")) {

			player.Damage(damage);
			StartCoroutine(player.Knockback(knockbackDuration, knockbackPower, player.transform.position));

		}

	}

}
