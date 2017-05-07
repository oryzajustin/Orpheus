using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbutt : MonoBehaviour {

	private Wolf wolf;
	public int damage = 2;
	public float knockbackDuration = 0.04f;
	public float knockbackPower = 750f;

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
