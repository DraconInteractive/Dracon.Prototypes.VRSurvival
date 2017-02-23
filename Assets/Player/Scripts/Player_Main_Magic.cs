using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Main : MonoBehaviour {

	void BeginLeftCast () {
		//		lSpell = Instantiate (spellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
		switch (leftSpell)
		{
		case Spell.Gesture:
			lSpell = Instantiate (gestureSpellTemplate, leftController.transform.position + leftController.transform.forward * 0.075f - leftController.transform.up * 0.05f, Quaternion.identity, leftController.transform);
			leftRModel.GetComponent<Animator> ().SetBool ("pointing", true);
			break;
		case Spell.Push:
			PushSpell (leftController);
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
			PushSpell (rightController);
			break;
		}
//		rightRModel.GetComponent<Animator> ().SetBool ("pointing", true);
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

	void PushSpell (GameObject hand) {
		RaycastHit[] hits;
		Ray ray = new Ray (hand.transform.position, hand.transform.forward);

		hits = Physics.SphereCastAll (ray, 1, 50);

		foreach (RaycastHit hit in hits) {
			Rigidbody r = hit.transform.gameObject.GetComponent<Rigidbody> ();
			if (r != null) {
				r.AddForce (ray.direction * 10, ForceMode.VelocityChange);
			}
		}
	}
}
