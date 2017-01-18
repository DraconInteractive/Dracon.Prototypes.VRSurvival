using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	public bool equipped;
	public GameObject targetHand;
	public Controller_Handler handController;
	Rigidbody rb;
	public Vector3 itemVel;

	void Awake () {
		rb = GetComponent<Rigidbody> ();

	}
	// Use this for initialization
	void Start () {
		rb.useGravity = false;
		rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (equipped) {
			rb.MovePosition (Vector3.SmoothDamp (transform.position, targetHand.transform.position, ref itemVel, 0.6f));
		}
	}

	public virtual void PickUp (GameObject hand) {
		handController = hand.GetComponent<Controller_Handler> ();
		equipped = true;
		targetHand = hand;
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	public virtual void PutDown () {
		handController = null;
		equipped = false;
		targetHand = null;
		rb.useGravity = true;
		rb.isKinematic = false;
	}
}
