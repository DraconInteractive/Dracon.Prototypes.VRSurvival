using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blade : MonoBehaviour {

	public Enemy thisEnemy;
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Item") {
			thisEnemy.anim.SetTrigger ("Recoil");
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Blade") {
			thisEnemy.anim.SetTrigger ("Recoil");
		}
	}
}
