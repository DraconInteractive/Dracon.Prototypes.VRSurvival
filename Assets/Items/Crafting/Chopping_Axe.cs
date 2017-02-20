﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopping_Axe : Item {

	public GameObject smoke;

	int axeStrength = 10;

	float timer, tTarget;

	void Start () {
		BaseStart ();
		timer = 0;
		tTarget = 1;
	}

	void Update () {
		BaseUpdate ();
		if (timer <= tTarget) {
			timer += Time.deltaTime;
		}
	}
//	void OnCollisionEnter (Collision col) {
//		if (equipped && col.gameObject.GetComponent<Tree_P>()) {
//			col.gameObject.GetComponent<Tree_P> ().Chop (axeStrength);
//			Quaternion sRot = Quaternion.LookRotation (col.contacts [0].normal);
//			/*GameObject smokeInst = */
//			Instantiate (smoke, col.contacts [0].point, sRot)/* as GameObject*/;
//		}
//	}

	public override void ItemCollision (Collision col) {
		if (equipped && col.gameObject.GetComponent<Tree_P>() && timer >= tTarget) {
			timer = 0;
			col.gameObject.GetComponent<Tree_P> ().Chop (axeStrength);
			Quaternion sRot = Quaternion.LookRotation (col.contacts [0].normal);
			/*GameObject smokeInst = */
			Instantiate (smoke, col.contacts [0].point, sRot)/* as GameObject*/;
		}
	}
}
