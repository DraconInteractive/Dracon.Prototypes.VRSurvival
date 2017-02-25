using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Spear : MonoBehaviour {

	Rigidbody rb;

	public float damage;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		rb.AddForce (transform.forward * 10, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
		NPC n = col.gameObject.GetComponent<NPC> ();
		if (n != null) {
			n.Damage (damage);
		}
	}
}
