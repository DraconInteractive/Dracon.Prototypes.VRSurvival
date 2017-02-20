using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	public Door myDoor;
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			myDoor.ToggleDoorState (true);
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			myDoor.ToggleDoorState (false);
		}
	}
}
