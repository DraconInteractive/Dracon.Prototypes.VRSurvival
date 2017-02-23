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
		doorOpen = true;
		Vector3 targetPos = initPos + Vector3.up * yOffset;
		while (transform.position != targetPos) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref doorVel, 0.25f);
			yield return null;
		}
		yield break;
	}

	IEnumerator CloseDoor () {
		doorOpen = false;
		Vector3 targetPos = initPos;
		while (transform.position != targetPos) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref doorVel, 0.25f);
			yield return null;
		}
		yield break;
	}

	public void ToggleDoorState (bool open) {
		
		if (open) {
			if (!doorOpen) {
				if (movementRoutine != null) {
					StopCoroutine (movementRoutine);
				}

				movementRoutine = StartCoroutine (OpenDoor ());
			}
		} else {
			if (doorOpen) {
				if (movementRoutine != null) {
					StopCoroutine (movementRoutine);
				}
				movementRoutine = StartCoroutine (CloseDoor ());
			}
		}
	}
}
