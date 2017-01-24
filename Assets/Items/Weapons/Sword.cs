﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item {

	public int damage;

	bool recharged;

	void Start () {
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
