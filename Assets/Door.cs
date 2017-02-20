using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public float activationDistance;

	public float yOffset;

	Vector3 initPos;

	Vector3 doorVel;

	bool doorOpen;
	Coroutine movementRoutine;
	// Use this for initialization
	void Start () {
		initPos = transform.position;
	}
	
	IEnumerator OpenDoor () {
		yield break;
	}

	IEnumerator CloseDoor () {
		yield break;
	}

	public void ToggleDoorState (bool open) {
		
		if (open) {
			if (!doorOpen) {
				if (movementRoutine != null) {
					StopCoroutine (movementRoutine);
					movementRoutine = null;
				}
				StartCoroutine (OpenDoor ());
			}
		} else {
			if (doorOpen) {
				if (movementRoutine != null) {
					StopCoroutine (movementRoutine);
					movementRoutine = null;
				}
				StartCoroutine (CloseDoor ());
			}
		}
	}
}
