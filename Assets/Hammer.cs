using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Item {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (equipped && col.gameObject.tag == "Anvil" && itemVel.magnitude >= 0.5f) {
			col.gameObject.SendMessage ("AnvilStrike");
		}
	}
}
