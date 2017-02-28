using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour {
	public Rigidbody rb;

	bool floating;

	Vector3 floatPos;
	Vector3 projectileVelocity;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		floating = false;
		rb.useGravity = false;
	}

	void Update () {
		if (floating) {
			transform.position = Vector3.SmoothDamp (transform.position, floatPos, ref projectileVelocity, 1);
		}
	}
	
	void OnCollisionEnter (Collision col) {
		Activate ();
	}

	/// <summary>
	/// Activate Singularity weightlessness protocols;
	/// </summary>
	void Activate () {
		print ("Singularity Activated");
		Collider [] objs = Physics.OverlapSphere(transform.position, 5);

		foreach (Collider c in objs) {
			GameObject g = c.gameObject;
			if (g.GetComponent<SingularityApplication>()) {
				break;
			}
//			Rigidbody r = g.GetComponent<Rigidbody> ();

			SingularityApplication sA = g.AddComponent<SingularityApplication> ();

			sA.singularityPosition = transform.position;
		}
		floatPos = transform.position + Vector3.up;
		floating = true;
		rb.useGravity = false;
	} 


}
