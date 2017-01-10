using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	bool equipped;
	public GameObject targetHand;
	Rigidbody rb;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.isKinematic = true;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (equipped) {
			Vector3 targetVector = targetHand.transform.position - transform.position;
			Vector3 vel = Vector3.zero;
			rb.MovePosition (Vector3.SmoothDamp (transform.position, targetHand.transform.position, ref vel, 0.6f));
		}
	}

	void PickUp (GameObject hand) {
		equipped = true;
		targetHand = hand;
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	void PutDown () {
		equipped = false;
		targetHand = null;
		rb.useGravity = true;
		rb.isKinematic = false;
	}
}
