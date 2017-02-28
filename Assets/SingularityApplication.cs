using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityApplication : MonoBehaviour {
	Rigidbody rb;
	public Vector3 singularityPosition;
	void Start () {
//		if (GetComponent<NPC>() || GetComponent<Player_Main>() || GetComponent<Enemy>()) {
//			Destroy (GetComponent<SingularityApplication> ());
//		} 

		rb = GetComponent<Rigidbody> ();
	}
	public void Activate () {
		rb.useGravity = false;
		rb.AddForce ((singularityPosition - transform.position).normalized * 5, ForceMode.VelocityChange);
		Invoke ("Deactivate", 5);
	}

	public void Deactivate () {
		rb.useGravity = true;
	}
}
