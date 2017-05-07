using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPunch3 : MonoBehaviour {

	private Wolf wolf;
	public int damage = 1;
	public float knockbackDuration = 0.02f;
	public float knockbackPower = 500f;

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
