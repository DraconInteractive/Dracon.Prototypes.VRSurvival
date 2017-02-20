using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public float activationDistance;

	public float yOffset;

	Vector3 initPos;

	Vector3 doorVel;

	Player_Main player;

	public bool doorCurrentlyOpen;
	// Use this for initialization
	void Start () {
		initPos = transform.position;
		player = Player_Main.player;
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (transform.position + Vector3.up * yOffset, Vector3.one * 0.1f);
	}

	IEnumerator OpenDoor () {
		doorCurrentlyOpen = true;
		Vector3 targetPos = initPos + Vector3.up * yOffset;
		while (transform.position != targetPos) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref doorVel, 0.5f);
			yield return null;
		}
		yield break;
	}

	IEnumerator CloseDoor () {
		doorCurrentlyOpen = false;
		Vector3 targetPos = initPos;
		while (transform.position != targetPos) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref doorVel, 0.5f);
			yield return null;
		}
		yield break;
	}

	public void ToggleDoorState (bool open) {
		if (open) {
			if (!doorCurrentlyOpen) {
				StopCoroutine ("CloseDoor");
				StartCoroutine (OpenDoor ());
			}
		} else {
			if (doorCurrentlyOpen) {
				StopCoroutine ("OpenDoor");
				StartCoroutine (CloseDoor ());
			}
		}
	}



}
