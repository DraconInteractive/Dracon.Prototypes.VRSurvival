using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Physics_Item : Base_Item {
	public bool equipped;
	[HideInInspector]
	public GameObject controllerObj;
	public HandRole equippedHand;
	[HideInInspector]
	public Rigidbody rb;
	[HideInInspector]
	public Vector3 itemVel;
	[HideInInspector]
	public Vector3 initPos;
	[HideInInspector]
	public bool initStage;
	[HideInInspector]
	public Player_Main player;

	public enum ItemType {Melee, Ranged, Tool};
	public ItemType itemType;

	public bool dropOnAwake;

	public GameObject itemPrefab;

	void Awake () {
		SetupFunc ();
	}
		
	void Start () {
		BaseStart ();
	}

	public void BaseStart () {
		player = Player_Main.player;
	}
	// Update is called once per frame
	void Update () {
		BaseUpdate ();
	}

	public void BaseUpdate () {
		if (equipped) {

//			rb.MovePosition (Vector3.Lerp (transform.position, controllerObj.transform.position, 0.8f));
//			rb.MoveRotation (Quaternion.Lerp (transform.rotation, controllerObj.transform.rotation, 0.8f));
			transform.position = Vector3.SmoothDamp(transform.position, controllerObj.transform.position, ref itemVel, 0.025f);
			transform.rotation = Quaternion.Lerp (transform.rotation, controllerObj.transform.rotation, 0.8f);

		} else if (initStage) {
			transform.position = initPos;
		}
	}

	public virtual void PickUp (GameObject hand, HandRole handType) {
		initStage = false;
		equipped = true;
		controllerObj = hand;
		equippedHand = handType;
		rb.useGravity = false;

		print (name + " picked up");
	}

	public virtual void PutDown () {
		initStage = false;
		equipped = false;
		controllerObj = null;
		rb.useGravity = true;
		switch (itemType) 
		{
		case ItemType.Melee:
			if (player.playerMelee_INV == GetComponent<Physics_Item> ()) {
				player.playerMelee_INV = null;
			}
			break;
		case ItemType.Ranged:
			if (player.playerRanged_INV == GetComponent<Physics_Item> ()) {
				player.playerRanged_INV = null;
			}
			break;
		}
	}

//	public virtual void ReturnToInventory () {
//		switch (itemType) 
//		{
//		case ItemType.Melee:
//			player.playerMelee_INV = GetComponent<Physics_Item> ();
//			break;
//		case ItemType.Ranged:
//			player.playerRanged_INV = GetComponent<Physics_Item> ();
//			break;
//		}
//
//		Destroy (this.gameObject);
//
//		//TODO this wont work. Its storing a reference not a copy. 
//		//Gotta see if I can direct it to a prefab type, etc. 
//
//		//IDEA. The item stores a prefab of itself and the player grabs this. 
//		//IDEA 2, The item doesnt destroy, just deactivates
//		//IDEA 3, the item doesnt deactivate, just holsters, Raw Data style. 
//		//IDEA 3 cont., Make sure to turn off colliders etc for it when holstered. 
//	}

	public void SetupFunc () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		initStage = true;
		initPos = transform.position;
//		rb.isKinematic = true;

		if (dropOnAwake) {
			PutDown ();
		}
	}

	void OnCollisionEnter (Collision col) {
		ItemCollision (col);
	}

	public virtual void ItemCollision (Collision col) {
		
	}
}
