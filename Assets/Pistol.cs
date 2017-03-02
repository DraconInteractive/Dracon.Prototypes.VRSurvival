using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Pistol : Physics_Item {

	Coroutine fireRoutine;

	public GameObject firePoint;

	public GameObject bulletParticleDecal, fireParticle;
	public float fireWait;
	internal override void Update () {
		base.Update ();
		if (equipped) {
			if (ViveInput.GetPressDown(handRole, ControllerButton.Trigger)) {
				if (fireRoutine == null) {
					fireRoutine = StartCoroutine (Fire ());
				}
			}
		}
	}

	IEnumerator Fire () {
		Instantiate (fireParticle, firePoint.transform.position, Quaternion.LookRotation (firePoint.transform.forward, firePoint.transform.up), firePoint.transform);
		Ray ray = new Ray (firePoint.transform.position, firePoint.transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 50)) {
			Instantiate (bulletParticleDecal, hit.point, Quaternion.identity);
		}
		yield return new WaitForSeconds (fireWait);
		fireRoutine = null;
		yield break;
	}
}
