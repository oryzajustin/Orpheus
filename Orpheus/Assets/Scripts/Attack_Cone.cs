using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {
	public Boss boss;
	public bool isLeft = false;
	void Awake(){
		boss = gameObject.GetComponentInParent<Boss>();
	}
	
	void OnTriggerStay2D(Collider2D col){
		if(col.CompareTag("Player")){
			if(isLeft){
				boss.Attack(true);
			}
		}
	}
}
