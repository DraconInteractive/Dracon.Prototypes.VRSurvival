using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Physics_Item {

	public int damage;

	bool recharged;

	public GameObject hilt;

	public GameObject particleSys;
	internal override void Start () {
		base.Start ();
		recharged = true;
	}

	void OnDrawGizmos () {
		Gizmos.DrawLine (hilt.transform.position, hilt.transform.position + hilt.transform.forward * 1);
	}
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.GetComponent<Enemy>()) {
			if (recharged) {
				col.gameObject.GetComponent<Enemy> ().Damage (damage);
				recharged = false;
				Invoke ("Recharge", 1);

				Ray ray = new Ray (hilt.transform.position, hilt.transform.forward);
				RaycastHit[] hits = Physics.RaycastAll (ray, 1);

				foreach (RaycastHit hit in hits) {
					if (hit.collider == col) {
						Instantiate (particleSys, hit.point, Quaternion.identity, col.gameObject.transform);
						break;
					}
				}
			}
		}
	}

	void Recharge () {
		recharged = true;
	}
}
