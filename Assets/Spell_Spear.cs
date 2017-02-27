using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Spear : MonoBehaviour {

	Rigidbody rb;

	public float damage;

	public GameObject hilt;
	public GameObject particle;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		rb.AddForce (transform.forward * 15, ForceMode.VelocityChange);
	}

	void OnDrawGizmos () {
		Gizmos.DrawLine (hilt.transform.position, hilt.transform.position + hilt.transform.forward);
	}
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
		NPC n = col.gameObject.GetComponent<NPC> ();
		if (n != null) {
			n.Damage (damage);
		} 

		Ray ray = new Ray (hilt.transform.position, hilt.transform.forward);
		RaycastHit[] hits = Physics.RaycastAll (ray, 1);

		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject == col.gameObject) {
				GameObject p = Instantiate (particle, hit.point, Quaternion.identity, col.gameObject.transform);
				Destroy (p, 1);
				break;
			}
		}

		Destroy (this.gameObject, 0.1f);

	}
}
