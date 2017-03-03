using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityApplication : MonoBehaviour {
	Rigidbody rb;
	public Vector3 singularityPosition;
	void Awake () {
//		if (GetComponent<NPC>() || GetComponent<Player_Main>() || GetComponent<Enemy>()) {
//			Destroy (GetComponent<SingularityApplication> ());
//		} 

		rb = GetComponent<Rigidbody> ();
	}

	public void Activate () {
		if (rb == null) {
			Deactivate ();
			return;
		}
		rb.useGravity = false;
		rb.AddExplosionForce (10, singularityPosition, 5, 0.5f, ForceMode.VelocityChange);
		Invoke ("Deactivate", 5);
	}

	public void Deactivate () {
		rb.useGravity = true;
		Destroy (GetComponent<SingularityApplication> ());
	}
}
