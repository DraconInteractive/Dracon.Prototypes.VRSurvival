using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopping_Axe : Item {

	public GameObject smoke;

	int axeStrength = 10;
	void OnCollisionEnter (Collision col) {
		if (equipped && col.gameObject.GetComponent<Tree_P>()) {
			col.gameObject.GetComponent<Tree_P> ().Chop (axeStrength);
			Quaternion sRot = Quaternion.LookRotation (col.contacts [0].normal);
			/*GameObject smokeInst = */
			Instantiate (smoke, col.contacts [0].point, sRot)/* as GameObject*/;
		}
	}
}
