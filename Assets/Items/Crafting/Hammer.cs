using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Item {

	void OnCollisionEnter (Collision col) {
		if (equipped && col.gameObject.tag == "AnvilPiece") {
			col.gameObject.GetComponent<Anvil_Piece> ().Smite ();
		}
		print ("Check");
	}
}
