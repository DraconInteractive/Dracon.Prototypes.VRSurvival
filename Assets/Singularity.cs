using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour {
	public Rigidbody rb;

	bool floating;

	Vector3 floatPos;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		floating = false;
	}

	void FixedUpdate () {
		
	}
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Ground") {
			floating = true;
			Activate ();
			floatPos = transform.position + Vector3.up;
		}
	}

	void Activate () {
		Collider [] objs = Physics.OverlapSphere(transform.position, 5);

		foreach (Collider c in objs) {
			GameObject g = c.gameObject;

			Rigidbody r = g.GetComponent<Rigidbody> ();

			SingularityApplication sA = g.AddComponent<SingularityApplication> ();

			sA.singularityPosition = transform.position;
		}
	} 


}
