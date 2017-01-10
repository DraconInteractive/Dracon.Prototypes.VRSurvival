using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour {
	public int pickStrength;
	PickAxe thisAxe;

	void Awake () {
		thisAxe = GetComponent<PickAxe> ();
	}

	void OnCollisionEnter (Collision col) {
		GameObject hitObj = col.collider.gameObject;
		if (hitObj.tag == "Rock") {
			hitObj.GetComponent<Rock> ().PickAt (thisAxe);
		}
	}
}
