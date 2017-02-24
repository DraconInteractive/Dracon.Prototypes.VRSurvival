using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Button : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "PlayerHandL" || col.gameObject.tag == "PlayerHandR") {
			ButtonFunction (col.gameObject);
		}
	}

	public virtual void ButtonFunction (GameObject hand) {
		
	}
}
