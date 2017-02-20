using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Physics_Item {

	public int damage;

	bool recharged;

	void Start () {
		BaseStart ();
		recharged = true;
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.GetComponent<Enemy>()) {
			if (recharged) {
				col.gameObject.GetComponent<Enemy> ().Damage (damage);
				recharged = false;
				Invoke ("Recharge", 1);
			}
		}
	}

	void Recharge () {
		recharged = true;
	}
}
