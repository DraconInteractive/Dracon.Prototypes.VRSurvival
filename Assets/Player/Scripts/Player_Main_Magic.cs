using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Main : MonoBehaviour {

	#region baseVar-SPELLS
	[Header("baseVar SPELLS")]
	public GameObject gestureSpellTemplate;
	public GameObject telekinesisSpellTemplate, levitateSpellTemplate, pushSpellTemplate, summonSwordSpellTemplate, spearShotSpellTemplate;

	GameObject lSpell, rSpell;

	public enum Spell {Gesture, Telekinesis, Levitate, Push, Summon_Sword, Spear_Shot};
	public Spell leftSpell, rightSpell;
	#endregion

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

	void DestroyPushPointer (GameObject hand) {
		GameObject pointer = hand.transform.GetComponentInChildren<PushSpellTemplate> ().gameObject;
		Destroy (pointer);
	}

	void SummonSword (GameObject hand) {
		GameObject sword = Instantiate (summonSwordSpellTemplate, hand.transform.position - hand.transform.up * 0.25f, Quaternion.identity);
	}
}
