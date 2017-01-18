using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


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
//		thisAxe = GetComponent<PickAxe> ();
		render = GetComponentInChildren<Renderer> ();
	}

	void Start () {
		currentCharge = maxCharge;
//		PutDown ();
	}

	void Update () {
		if (boostActive && currentCharge > 0) {
			chargeMultiplier = 2;
			currentCharge -= Time.deltaTime;
		} else {
			chargeMultiplier = 1;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (equipped) {
			GameObject hitObj = col.collider.gameObject;
			if (hitObj.tag == "Rock" && itemVel.magnitude > 0.1f && equipped) {
				
				hitObj.GetComponent<Rock> ().PickAt (Mathf.RoundToInt(pickStrength * chargeMultiplier));
			}
		}
	}

	public override void PickUp (GameObject hand) {
		base.PickUp (hand);
		SteamVR_TrackedController c = hand.GetComponent<SteamVR_TrackedController> ();
		c.TriggerClicked += StartBoostTrigger;
	}

	void StartBoostTrigger (object sender, ClickedEventArgs e) {
		StartCoroutine (StartBoost ());
	}

	void EndBoostTrigger (object sender, ClickedEventArgs e) {
		StartCoroutine (EndBoost ());
	}

	IEnumerator StartBoost () {
		StopCoroutine ("EndBoost");
		boostActive = true;
		Color targetColor = Color.white;
		while (render.material.GetColor("_EmissiveColor") != targetColor) {
			render.material.SetColor("_EmissiveColor", Color.Lerp (render.material.GetColor ("_EmissiveColor"), targetColor, 0.1f));
			yield return null;
		}
		yield break;
	}

	IEnumerator EndBoost () {
		StopCoroutine ("StartBoost");
		boostActive = false;
		Color targetColor = Color.black;
		while (render.material.GetColor("_EmissiveColor") != targetColor) {
			render.material.SetColor("_EmissiveColor", Color.Lerp (render.material.GetColor ("_EmissiveColor"), targetColor, 0.1f));
			yield return null;
		}
		yield break;
	}
}
