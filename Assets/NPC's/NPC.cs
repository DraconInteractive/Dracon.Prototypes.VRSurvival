using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	Animator anim;

	public bool interacting;

	public bool gazeTrigger;

	float laWeight;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}

	void Start () {
		laWeight = 0;
	}

	void Update () {
		
		if (!gazeTrigger && interacting) {
			EndInteraction ();
		}

		if (interacting) {
			if (!gazeTrigger) {
				EndInteraction ();
			}

			if (laWeight < 1) {
				laWeight += Time.deltaTime;
			}
		} else {
			if (gazeTrigger) {
				BeginInteraction ();
			}

			if (laWeight > 0) {
				laWeight -= Time.deltaTime;
			}
		}
		gazeTrigger = false;
	}

	void OnAnimatorIK () {

		if (interacting && anim != null) {
			anim.SetLookAtPosition (Camera.main.transform.position);
		} 

		anim.SetLookAtWeight (laWeight);
	}

	public void BeginInteraction () {
		interacting = true;
		print ("Begun Interaction");
//		Invoke ("EndInteraction", 3);
	}

	public void EndInteraction () {
		print ("Ending Interaction");
		interacting = false;
	}
}

