using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;
using CurvedUI;


public class Player_Main : MonoBehaviour {
	#region baseVar-STD
	public static Player_Main player;

	Rigidbody rb;

	public GameObject leftController, rightController;
	public GameObject leftRModel, rightRModel;

	Camera mainC;

	public float speed;
	#endregion

	#region baseVar-INV
	[HideInInspector]
	public int rockAmount, woodAmount;
	[HideInInspector]
	public Physics_Item leftHandItem, rightHandItem;
	[HideInInspector]
	public Physics_Item playerMelee_INV, playerRanged_INV;
	#endregion

	#region baseVAR-MENU
	public GameObject mainMenuTemplate, leftMenu, rightMenu;
	#endregion

	#region baseVar-SPELLS
	public GameObject gestureSpellTemplate, telekinesisSpellTemplate, levitateSpellTemplate, pushSpellTemplate, summonSwordSpellTemplate;
	GameObject lSpell, rSpell;

	public enum Spell {Gesture, Telekinesis, Levitate, Push, Summon_Sword};
	public Spell leftSpell, rightSpell;
	#endregion

	#region baseVar-ADAPTIVE COLLISIONS
//	[HideInInspector]
//	public CapsuleCollider playerCollider;
	#endregion

	#region baseVar-SOUND
	public AudioSource mainAS;
	#endregion

	#region baseVar-UI
	public Sprite returnImg, testImg;

	public Image topLeftImg, topRightImg;

	public GameObject leftCanvas, rightCanvas;
	List<GameObject> lastLeftC = new List<GameObject>();
	List<GameObject> lastRightC = new List<GameObject>();
	float lCTimer, rCTimer;

	public Button returnToInvButtonL, returnToINVButtonR;

	float gazeTimer;
	GameObject lastGaze;

	#endregion

	#region Standard Functions
	//TODO Invis rendermodels when equip item, reappear when disequip;
	void Awake () {
		player = GetComponent<Player_Main> ();
		rb = GetComponent<Rigidbody> ();
//		playerCollider = GetComponent<CapsuleCollider> ();
		mainC = Camera.main;
	}

	void Start () {
		ToggleLeftMenu (false);
		ToggleRightMenu (false);
		EndRightCast ();
		EndLeftCast ();
	}

	void Update () {
		P_Input ();
		PlayerMenu ();
		GazeUpdate ();
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (!leftMenu.activeSelf) {
			Movement ();
		}
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
	#endregion

	#region Helper Functions
	Vector2 GetLeftPadTouch () {
		return ViveInput.GetPadTouchAxis (HandRole.LeftHand);
	}

	Vector2 GetRightPadTouch () {
		return ViveInput.GetPadTouchAxis (HandRole.RightHand);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (leftController.transform.position, 0.1f);
		Gizmos.DrawWireSphere (rightController.transform.position, 0.1f);
	}
	#endregion

	#region Interaction Functions
	public void PickUpWithLeft () {
		if (leftHandItem != null) {
			leftHandItem.PutDown ();
			SteamVR_Controller.Device d = SteamVR_Controller.Input ((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
			leftHandItem.rb.velocity = d.velocity * 1.2f;
			leftHandItem = null;
			leftRModel.SetActive (true);
		} else {
			Collider[] c = Physics.OverlapSphere (leftController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++) {
//				print (c [i].name);
				if (c[i].gameObject.tag == "Item") {
					Physics_Item it = c [i].GetComponent<Physics_Item> ();
					if (it.equipped && it.equippedHand == HandRole.RightHand) {
						it.PutDown ();
						rightHandItem = null;
						rightRModel.SetActive (true);
						it.PickUp (rightController.gameObject, HandRole.LeftHand);
					} else {
						it.PickUp (rightController.gameObject, HandRole.LeftHand);
					}
					c[i].GetComponent<Physics_Item>().PickUp (leftController.gameObject, HandRole.LeftHand);
					leftHandItem = c [i].GetComponent<Physics_Item> ();
					leftRModel.SetActive (false);
					break;
				}
			}

		}

	}

	public void PickUpWithRight () {
		if (rightHandItem != null) {
			rightHandItem.PutDown ();
			SteamVR_Controller.Device d = SteamVR_Controller.Input ((int)rightController.GetComponent<SteamVR_TrackedObject>().index);
			rightHandItem.rb.velocity = d.velocity * 1.2f;
			rightHandItem = null;
			rightRModel.SetActive (true);
		} else {
			Collider[] c = Physics.OverlapSphere (rightController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++) {
//				print (c [i].name);
				if (c[i].gameObject.tag == "Item") {
					Physics_Item it = c[i].GetComponent<Physics_Item>();
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
					break;
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
		} else {
			if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu)) {
				ToggleRightMenu (!rightMenu.activeSelf);
			}

			if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Menu)) {
				ToggleLeftMenu (!leftMenu.activeSelf);
			}
		}

		if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger) && rightHandItem == null) {
			BeginRightCast ();
		}

		if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Trigger) && leftHandItem == null) {
			BeginLeftCast ();
		}

		if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Trigger) && rightHandItem == null) {
			EndRightCast ();
		}

		if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Trigger) && leftHandItem == null) {
			EndLeftCast ();
		}
	}

	void GazeUpdate () {
		RaycastHit hit;
		Ray ray = new Ray (mainC.transform.position, mainC.transform.forward);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			string t = hit.collider.tag;
			GameObject hitObj = hit.collider.gameObject;
			if (t == "NPC") {
				NPC npc = hit.collider.gameObject.GetComponent<NPC> ();
				npc.gazeTrigger = true;
			} else if (t == "HandMenuL") {
				Button b = hitObj.GetComponent<Button> ();
				b.Select ();
//				EventSystem.current.SetSelectedGameObject (hitObj);
				if (lastGaze == hitObj) {
					gazeTimer += Time.deltaTime;
					if (gazeTimer >= 1) {
						gazeTimer = 0;
						ReturnToInventory (HandRole.LeftHand, leftHandItem);
					}
				} else {
					gazeTimer = 0;
					lastGaze = hitObj;
				}
			} else if (t == "HandMenuR") {
				
				Button b = hitObj.GetComponent<Button> ();
				b.Select ();
				if (lastGaze == hitObj) {
					gazeTimer += Time.deltaTime;
					if (gazeTimer >= 1) {
						gazeTimer = 0;
						ReturnToInventory (HandRole.RightHand, rightHandItem);
					}
				} else {
					gazeTimer = 0;
					lastGaze = hitObj;
				}
			}
		} else {
			gazeTimer = 0;
		}

	}
         
	void ReturnToInventory (HandRole hand, Physics_Item i) {

		switch (hand)
		{
		case HandRole.LeftHand:
			if (leftHandItem == null) {
				break;
			}
			switch (i.itemType)
			{
			case Physics_Item.ItemType.Melee:
				if (playerMelee_INV != null) {
					GameObject g = Instantiate (playerMelee_INV.GetComponent<Physics_Item> ().itemPrefab, leftController.transform.position, Quaternion.identity) as GameObject;
				}
				playerMelee_INV = i.itemPrefab;
				Destroy (i.gameObject);
				break;
			case Physics_Item.ItemType.Ranged:
				if (playerRanged_INV != null) {
					GameObject g = Instantiate (playerRanged_INV.GetComponent<Physics_Item> ().itemPrefab, leftController.transform.position, Quaternion.identity) as GameObject;
				}
				playerRanged_INV = i.itemPrefab;
				Destroy (i.gameObject);
				break;
			}
			break;
		case HandRole.RightHand:
			if (rightHandItem == null) {
				break;
			}
			switch (i.itemType)
			{
			case Physics_Item.ItemType.Melee:
				if (playerMelee_INV != null) {
					GameObject g = Instantiate (playerMelee_INV.GetComponent<Physics_Item> ().itemPrefab, rightController.transform.position, Quaternion.identity) as GameObject;
				}
				playerMelee_INV = i.itemPrefab;
				Destroy (i.gameObject);
				break;
			case Physics_Item.ItemType.Ranged:
				if (playerRanged_INV != null) {
					GameObject g = Instantiate (playerRanged_INV.GetComponent<Physics_Item> ().itemPrefab, rightController.transform.position, Quaternion.identity) as GameObject;
				}
				playerRanged_INV = i.itemPrefab;
				Destroy (i.gameObject);
				break;
			}
			break;
		}
	}
	#endregion

	#region Magic Functions
	void BeginLeftCast () {
//		lSpell = Instantiate (spellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
		switch (leftSpell)
		{
		case Spell.Gesture:
			lSpell = Instantiate (gestureSpellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
			leftRModel.GetComponent<Animator> ().SetBool ("pointing", true);
			break;
		}
	}

	void BeginRightCast () {
//		rSpell = Instantiate (spellTemplate, rightController.transform.position + rightController.transform.forward * 0.075f - rightController.transform.up * 0.05f, Quaternion.identity, rightController.transform);
		switch (rightSpell)
		{
		case Spell.Gesture:
			rSpell = Instantiate (gestureSpellTemplate, rightController.transform.position + rightController.transform.forward * 0.075f - rightController.transform.up * 0.05f, Quaternion.identity, rightController.transform);
			rightRModel.GetComponent<Animator> ().SetBool ("pointing", true);
			break;
		}
		rightRModel.GetComponent<Animator> ().SetBool ("pointing", true);
	}

	void EndLeftCast () {
		Destroy (lSpell);
		leftRModel.GetComponent<Animator> ().SetBool ("pointing", false);
	}

	void EndRightCast () {
		Destroy (rSpell);
		rightRModel.GetComponent<Animator> ().SetBool ("pointing", false);
	}

	void SetLeftSpell (Spell spell) {
		leftSpell = spell;
	}

	void SetRightSpell (Spell spell) {
		rightSpell = spell;
	}
	#endregion

	#region UI Functions
	void CreateMainMenu () {
		Vector3 menuPos = mainC.transform.position + new Vector3 (mainC.transform.forward.x, 0, mainC.transform.forward.z) * 2;
//		Quaternion menuRot = Quaternion.LookRotation (menuPos - transform.position, Vector3.up);
		/*GameObject mainMenu = */
		Instantiate (mainMenuTemplate, menuPos, Quaternion.identity)/* as GameObject*/;
	}

	void ToggleLeftMenu (bool state) {
		leftMenu.SetActive (state);
		if (leftHandItem != null) {
			topLeftImg.sprite = returnImg;
		} else {
			topLeftImg.sprite = testImg;
		}
	}

	void ToggleRightMenu (bool state) {
		rightMenu.SetActive (state);
		if (rightHandItem != null) {
			topRightImg.sprite = returnImg;
		} else {
			topRightImg.sprite = testImg;
		}
	}

	void PlayerMenu () {
		//Manager of Wrist Menu's
		if (leftMenu.activeSelf) {
//			Vector2 padPress = ViveInput.GetPadPressAxis(HandRole.LeftHand);
//			if (padPress.y > 0.1f) {
//				if (leftHandItem != null) {
//					leftHandItem.ReturnToInventory ();
//				} 
//			} else if (padPress.y < 0.1f) {
//				ToggleLeftMenu (false);
//			}
		}

		if (rightMenu.activeSelf) {
//			Vector2 padPress = ViveInput.GetPadPressAxis(HandRole.RightHand);
//			if (padPress.y > 0.1f) {
//				if (rightHandItem != null) {
//					rightHandItem.ReturnToInventory ();
//				} else {
//					//TODO add instantiation of item prefab
//				}
//
//			} else if (padPress.y < 0.1f) {
//				ToggleRightMenu (false);
//			}
		}
	}
	#endregion

}
