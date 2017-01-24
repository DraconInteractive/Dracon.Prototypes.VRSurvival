using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;


public class Player_Main : MonoBehaviour {
	public static Player_Main player;

	Rigidbody rb;

	public GameObject leftController, rightController;
	public GameObject leftRModel, rightRModel;

	Camera mainC;

	public float speed;

	//Inventory
	public int rockAmount, woodAmount;

	

	public Item leftHandItem, rightHandItem;

	public GameObject mainMenuTemplate;

	//TODO Invis rendermodels when equip item, reappear when disequip;
	void Awake () {
		player = GetComponent<Player_Main> ();
		rb = GetComponent<Rigidbody> ();
		mainC = Camera.main;
	}

	void Update () {
		P_Input ();
	}
	// Update is called once per frame
	void FixedUpdate () {
		Movement ();
	}

	void Movement () {
		Vector2 leftHandTouch = GetLeftPadTouch ();
		if (leftHandTouch.magnitude != 0) {
			Vector3 forwardMovement = new Vector3 (mainC.transform.forward.x, 0, mainC.transform.forward.z);
			Vector3 sidewardMovement = new Vector3 (mainC.transform.right.x, 0, mainC.transform.right.z);
			float speedF = ViveInput.GetPadTouchAxis (HandRole.LeftHand).y * speed * Time.deltaTime;
			float speedR = ViveInput.GetPadTouchAxis (HandRole.LeftHand).x * speed * Time.deltaTime;

			rb.MovePosition (transform.position + (forwardMovement * speedF * Time.deltaTime) + (sidewardMovement * speedR * Time.deltaTime));
		}
	}

	Vector2 GetLeftPadTouch () {
		return ViveInput.GetPadTouchAxis (HandRole.LeftHand);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (leftController.transform.position, 0.2f);
		Gizmos.DrawWireSphere (rightController.transform.position, 0.2f);
	}

	public void PickUpWithLeft () {
		if (leftHandItem != null) {
			leftHandItem.PutDown ();
			leftHandItem = null;
			leftRModel.SetActive (true);
		} else {
			Collider[] c = Physics.OverlapSphere (leftController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++) {
//				print (c [i].name);
				if (c[i].gameObject.tag == "Item") {
					Item it = c [i].GetComponent<Item> ();
					if (it.equipped && it.equippedHand == HandRole.RightHand) {
						it.PutDown ();
						rightHandItem = null;
						rightRModel.SetActive (true);
						it.PickUp (rightController.gameObject, HandRole.LeftHand);
					} else {
						it.PickUp (rightController.gameObject, HandRole.LeftHand);
					}
					c[i].GetComponent<Item>().PickUp (leftController.gameObject, HandRole.LeftHand);
					leftHandItem = c [i].GetComponent<Item> ();
					leftRModel.SetActive (false);
				}
			}

		}

	}

	public void PickUpWithRight () {
		if (rightHandItem != null) {
			rightHandItem.PutDown ();
			rightHandItem = null;
			rightRModel.SetActive (true);
		} else {
			Collider[] c = Physics.OverlapSphere (rightController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++) {
//				print (c [i].name);
				if (c[i].gameObject.tag == "Item") {
					Item it = c[i].GetComponent<Item>();
					if (it.equipped && it.equippedHand == HandRole.LeftHand) {
						it.PutDown ();
						leftHandItem = null;
						leftRModel.SetActive (true);
						it.PickUp (rightController.gameObject, HandRole.RightHand);
					} else {
						it.PickUp (rightController.gameObject, HandRole.RightHand);
					}

					rightHandItem = it;
					rightRModel.SetActive (false);
				}
			}
		}

	}

	void P_Input () {
		if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Grip)) {
			PickUpWithLeft ();
			print ("left pick up");
		} 

		if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Grip)) {
			PickUpWithRight ();
			print ("right pick up");
		}

		if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu) && ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Menu)) {
			CreateMainMenu ();
		}
	}

	void CreateMainMenu () {
		Vector3 menuPos = mainC.transform.position + new Vector3 (mainC.transform.forward.x, 0, mainC.transform.forward.z) * 2;
		Quaternion menuRot = Quaternion.LookRotation (menuPos - transform.position, Vector3.up);
		/*GameObject mainMenu = */
		Instantiate (mainMenuTemplate, menuPos, menuRot)/* as GameObject*/;
	}
}
