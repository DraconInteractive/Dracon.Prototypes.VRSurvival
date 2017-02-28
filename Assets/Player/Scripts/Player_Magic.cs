using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Player_Magic : MonoBehaviour {

	public static Player_Magic pMagic;

	#region baseVar-Objects
	GameObject leftController, rightController, leftRModel, rightRModel;
	Player_Main player;
	#endregion

	#region baseVar-SPELLS
	[Header("baseVar SPELLS")]
	public GameObject gestureSpellTemplate;
	public GameObject telekinesisSpellTemplate, levitateSpellTemplate, pushSpellTemplate, summonSwordSpellTemplate, spearShotSpellTemplate, singularitySpellTemplate;

	GameObject lSpell, rSpell;

	public enum Spell {Gesture, Telekinesis, Levitate, Push, Summon_Sword, Spear_Shot, Singularity};
	public Spell leftSpell, rightSpell;
	#endregion

	void Awake () {
		pMagic = GetComponent<Player_Magic> ();
	}
	// Use this for initialization
	void Start () {
		player = Player_Main.player;
		leftController = player.leftController;
		rightController = player.rightController;
		leftRModel = player.leftRModel;
		rightRModel = player.rightRModel;
	}

	// Update is called once per frame
	void Update () {
		if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger) && player.rightHandItem == null) {
			BeginRightCast ();
		}

		if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Trigger) && player.leftHandItem == null) {
			BeginLeftCast ();
		}

		if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Trigger) && player.rightHandItem == null) {
			EndRightCast ();
		}

		if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Trigger) && player.leftHandItem == null) {
			EndLeftCast ();
		}
	}

	void BeginLeftCast () {
		//		lSpell = Instantiate (spellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
		switch (leftSpell)
		{
		case Spell.Gesture:
			lSpell = Instantiate (gestureSpellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
			leftRModel.GetComponent<Animator> ().SetBool ("pointing", true);
			break;
		case Spell.Push:
			CreatePushPointer (leftController);
			break;
		case Spell.Summon_Sword:
			SummonSword (leftController);
			break;
		case Spell.Spear_Shot:
			CreateSpearPointer (leftController);
			break;
		case Spell.Singularity:
			CreateSingularity (leftController);
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
		case Spell.Push:
			CreatePushPointer (rightController);
			break;
		case Spell.Summon_Sword:
			SummonSword (rightController);
			break;
		case Spell.Spear_Shot:
			CreateSpearPointer (rightController);
			break;
		case Spell.Singularity:
			CreateSingularity (rightController);
			break;
		}
		//		rightRModel.GetComponent<Animator> ().SetBool ("pointing", true);
	}

	void EndLeftCast () {
		switch (leftSpell)
		{
		case Spell.Gesture:
			Destroy (lSpell);
			leftRModel.GetComponent<Animator> ().SetBool ("pointing", false);
			break;
		case Spell.Push:
			PushSpell (leftController);
			DestroyPushPointer (leftController);
			break;
		case Spell.Spear_Shot:
			DestroySpearPointer (leftController);
			ShootSpear (leftController);
			break;
		case Spell.Singularity:
			ThrowSingularity (leftController);
			break;
		}

	}

	void EndRightCast () {
		switch (rightSpell)
		{
		case Spell.Gesture:
			Destroy (rSpell);
			rightRModel.GetComponent<Animator> ().SetBool ("pointing", false);
			break;
		case Spell.Push:
			PushSpell (rightController);
			DestroyPushPointer (rightController);
			break;
		case Spell.Spear_Shot:
			DestroySpearPointer (rightController);
			ShootSpear (rightController);
			break;
		case Spell.Singularity:
			ThrowSingularity (rightController);
			break;
		}

	}

	void SetLeftSpell (Spell spell) {
		leftSpell = spell;
	}

	void SetRightSpell (Spell spell) {
		rightSpell = spell;
	}

	void PushSpell (GameObject hand) {
		RaycastHit[] hits;
		Ray ray = new Ray (hand.transform.position, hand.transform.forward);
		int l = ~(1 << LayerMask.NameToLayer("Player"));
		hits = Physics.SphereCastAll (ray, 1, 10, l);

		foreach (RaycastHit hit in hits) {
			Rigidbody r = hit.transform.gameObject.GetComponent<Rigidbody> ();
			if (r != null) {
				r.AddForce (ray.direction * 10, ForceMode.Impulse);
			}
		}
	}

	void CreatePushPointer (GameObject hand) {
		GameObject pointer = Instantiate (pushSpellTemplate, hand.transform.position, hand.transform.rotation, hand.transform) as GameObject;
		PushSpellTemplate pointerScript = pointer.GetComponent<PushSpellTemplate> ();
		pointerScript.targetHand = hand;
	}

	void CreateSpearPointer (GameObject hand) {
		GameObject pointer = Instantiate (pushSpellTemplate, hand.transform.position - hand.transform.up * 0.25f, hand.transform.rotation, hand.transform) as GameObject;
		PushSpellTemplate pointerScript = pointer.GetComponent<PushSpellTemplate> ();
		pointerScript.targetHand = hand;
	}

	void DestroyPushPointer (GameObject hand) {
		GameObject pointer = hand.transform.GetComponentInChildren<PushSpellTemplate> ().gameObject;
		Destroy (pointer);
	}

	void DestroySpearPointer (GameObject hand) {
		GameObject pointer = hand.transform.GetComponentInChildren<PushSpellTemplate> ().gameObject;
		Destroy (pointer);
	}

	void SummonSword (GameObject hand) {
		GameObject sword = Instantiate (summonSwordSpellTemplate, hand.transform.position - hand.transform.up * 0.25f, Quaternion.identity);
	}

	void ShootSpear (GameObject hand) {
		GameObject spear = Instantiate (spearShotSpellTemplate, hand.transform.position - hand.transform.up * 0.25f, hand.transform.rotation);
	}

	void CreateSingularity (GameObject hand) {
		GameObject projectile = Instantiate (singularitySpellTemplate, hand.transform.position, Quaternion.identity, hand.transform);
	}

	void ThrowSingularity (GameObject hand) {
		Singularity singularity = hand.transform.GetComponentInChildren<Singularity> ();
		singularity.transform.SetParent (null);
		singularity.rb.useGravity = true;

		var device = SteamVR_Controller.Input ((int)hand.GetComponent<SteamVR_TrackedObject> ().index);
		singularity.rb.velocity = device.velocity * 2;
		singularity.rb.angularVelocity = device.velocity * 2;


		//		var device = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
		//		var rigidbody = (leftHandItem as Physics_Item).rb;
		//		rigidbody.velocity = device.velocity * itemThrowRatio;
		//		rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;

	}


}
