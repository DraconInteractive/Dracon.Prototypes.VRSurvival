using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Main : MonoBehaviour {
	public static Player_Main player;

	Rigidbody rb;

	[HideInInspector]
	public SteamVR_TrackedController leftHand, rightHand;
	[HideInInspector]
	public Controller_Handler leftHandC, rightHandC;
	[HideInInspector]
	public SteamVR_Controller.Device leftDevice, rightDevice;

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

	public Item leftHandItem, rightHandItem;

	void Awake () {
		player = GetComponent<Player_Main> ();
		rb = GetComponent<Rigidbody> ();
		mainC = Camera.main;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement ();
	}

	void Movement () {
		Vector2 leftHandTouch = GetLeftPadTouch ();
		print (leftHandTouch.magnitude);
		if (leftHandTouch.magnitude != 0) {
			Vector3 forwardMovement = new Vector3 (mainC.transform.forward.x, transform.position.y, mainC.transform.forward.z);
			Vector3 sidewardMovement = new Vector3 (mainC.transform.right.x, transform.position.y, mainC.transform.right.z);
			rb.MovePosition (transform.position + (forwardMovement + sidewardMovement) * speed * Time.deltaTime);
		}
	}

	Vector2 GetLeftPadTouch () {
//		return leftDevice.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		if (leftDevice != null) {
			return leftDevice.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		} else {
			print ("Left Device Null");
			return Vector2.zero;
		}
	}

	public void PickUpWithLeft (object sender, ClickedEventArgs e) {
		if (leftHandItem != null) {
			leftHandItem.PutDown ();
		} else {
			Collider[] c = Physics.OverlapSphere (leftHand.transform.position, 0.5f);
			for (int i = 0; i < c.Length; i++) {
				PickAxe item = c [i].gameObject.GetComponent<PickAxe> ();
				if (item != null) {
					item.PickUp (leftHand.gameObject);
				}
			}
		}

	}

	public void PickUpWithRight (object sender, ClickedEventArgs e) {
		if (rightHandItem != null) {
			rightHandItem.PutDown ();
		} else {
			Collider[] c = Physics.OverlapSphere (rightHand.transform.position, 0.5f);
			for (int i = 0; i < c.Length; i++) {
				PickAxe item = c [i].gameObject.GetComponent<PickAxe> ();
				if (item != null) {
					item.PickUp (rightHand.gameObject);
				}
			}
		}

	}
}
