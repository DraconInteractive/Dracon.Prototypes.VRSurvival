using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Player : MonoBehaviour {
	public static Player player;
	Rigidbody rb;

	HandRole dominantHand;
	HandRole subHand;

	Camera mainC;

	public float speed;

	//Inventory
	public int rockAmount;

	public int RockAmount {
		get {
			return rockAmount;
		}
		set {
			rockAmount = value;
		}
	}

	void Awake () {
		player = GetComponent<Player> ();
		rb = GetComponent<Rigidbody> ();
		mainC = Camera.main;
	}
	// Use this for initialization
	void Start () {
		SetDominantHand (HandRole.RightHand);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement ();
	}

	void Update () {
		Input ();
	}

	void Movement () {
		Vector2 subPadTouch = ViveInput.GetPadTouchAxis (subHand);

		if (subPadTouch.magnitude != 0) {
			Vector3 forwardMovement = new Vector3 (mainC.transform.forward.x, transform.position.y, mainC.transform.forward.z);
			Vector3 sidewardMovement = new Vector3 (mainC.transform.right.x, transform.position.y, mainC.transform.right.z);
			rb.MovePosition (transform.position + (forwardMovement + sidewardMovement) * speed * Time.deltaTime);
		}
	}

	void Input () {
		
	}

	void SetDominantHand (HandRole h) {
		dominantHand = h;

		if (h == HandRole.LeftHand) {
			subHand = HandRole.RightHand;
		} else {
			subHand = HandRole.LeftHand;
		}
	}
}
