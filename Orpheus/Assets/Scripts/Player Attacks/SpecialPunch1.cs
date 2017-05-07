using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPunch1 : MonoBehaviour {

	private Wolf wolf;
	public int damage = 3;
	public float knockbackDuration = 0.04f;
	public float knockbackPower = 1000f;

	void Start () {

		wolf = GameObject.FindGameObjectWithTag("Wolf").GetComponent<Wolf>();
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {

		if(col.CompareTag("EnemyHitbox")) {
			wolf.Damage(damage);
			StartCoroutine(wolf.Knockback(knockbackDuration, knockbackPower, wolf.transform.position));

		}

	}
	
}
