using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HTC.UnityPlugin.Vive;


public class PickAxe : Item {
	public int pickStrength;
	private bool pickedUp;

	public float currentCharge, maxCharge;
	bool boostActive;
	float chargeMultiplier;
	public bool PickedUp {
		get {
			return pickedUp;
		}
		set {
			pickedUp = value;
		}
	}

//	PickAxe thisAxe;
	Renderer render; 

	void Awake () {
		render = GetComponentInChildren<Renderer> ();
		SetupFunc ();
	}

	void Start () {
		currentCharge = maxCharge;
	}

	void Update () {
		BaseUpdate ();

		if (boostActive && currentCharge > 0) {
			chargeMultiplier = 2;
			currentCharge -= Time.deltaTime;
		} else {
			chargeMultiplier = 1;
		}

		if (equipped) {
			if (ViveInput.GetPress(equippedHand, ControllerButton.FullTrigger)) {
				StartBoost ();
			} else {
				EndBoost ();
			}
		}
	}

//	void OnCollisionEnter (Collision col) {
//		if (equipped) {
//			GameObject hitObj = col.gameObject;
//			if (hitObj.tag == "Rock" && equipped) {
//				hitObj.GetComponent<Rock> ().PickAt (Mathf.RoundToInt(pickStrength * chargeMultiplier));
//			}
//		}
//	}

	public override void ItemCollision (Collision col) {
		if (equipped) {
			GameObject hitObj = col.gameObject;
			if (hitObj.tag == "Rock" && equipped) {
				hitObj.GetComponent<Rock> ().PickAt (Mathf.RoundToInt(pickStrength * chargeMultiplier));
			}
		}
	}

	void StartBoost () {
		boostActive = true;
		Color targetColor = Color.white;
		if (render.material.GetColor("_EmissionColor") != targetColor) {
//			render.material.SetColor("_EmissionColor", Color.Lerp (render.material.GetColor ("_EmissionColor"), targetColor, 0.1f));
			render.material.SetColor ("_EmissionColor", targetColor);
		}
	}

	void EndBoost () {
		boostActive = false;
		Color targetColor = Color.black;
		if (render.material.GetColor("_EmissionColor") != targetColor) {
//			render.material.SetColor("_EmissionColor", Color.Lerp (render.material.GetColor ("_EmisssionColor"), targetColor, 0.1f));
			render.material.SetColor ("_EmissionColor", targetColor);
		}
	}
}
