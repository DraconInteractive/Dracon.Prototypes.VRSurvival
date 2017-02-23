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
		anim.SetBool ("interacting", interacting);
		if (interacting) {
			if (!gazeTrigger) {
				EndInteraction ();
			}

//			if (laWeight < 0.75f) {
//				laWeight += Time.deltaTime;
//			}


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

	public virtual void BeginInteraction () {
		interacting = true;

		anim.SetTrigger ("OnSeePlayer");
//		print ("Begun Interaction");
//		Invoke ("EndInteraction", 3);
	}

	public virtual void EndInteraction () {
//		print ("Ending Interaction");
		interacting = false;
	}
}

