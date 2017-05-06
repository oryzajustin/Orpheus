using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(!col.isTrigger){
//			if(col.CompareTag("Player")){
//				col.GetComponent<Player>().Damage(10);//take damage?
//			}
			Destroy(gameObject);
		}
	}
}
