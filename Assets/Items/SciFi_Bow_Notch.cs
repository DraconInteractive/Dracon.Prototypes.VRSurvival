using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFi_Bow_Notch : Base_Item {

	public GameObject arrowTemplate;
	public GameObject initNotchPos;
	public float maxStretch;

	GameObject nockedArrow;
	internal override void Update () {
//		1. convert the position to local-space
//		2. set the z to the value(s) you want
//		3. convert back in-to work space.

		if (controllerObj != null)
		{
			Vector3 localTargetPos = controllerObj.transform.TransformPoint (controllerObj.transform.position);
//			transform.position = controllerObj.transform.position;
//			transform.rotation = controllerObj.transform.rotation;
		}
		if (Vector3.Distance(initNotchPos.transform.position, transform.position) > maxStretch) {
			transform.position = initNotchPos.transform.position + (transform.position - initNotchPos.transform.position).normalized * maxStretch;
		}
	}

//	internal override void Update()
//	{
//		base.Update();
//		if (!notchGrabbed)
//			return;
//
//		Vector3 pullPosition = notch.controllerObj.transform.position;
//		Vector3 fromPosition = transform.TransformPoint(slingForkPoint);
//
//		float distance = Vector3.Distance(fromPosition, pullPosition);
//		distance = Mathf.Clamp(distance, 0, maximumDistance);
//
//		stretchDecimal = distance / maximumDistance;
//
//		SetSlingStrech(stretchDecimal);
//
//		normal = (fromPosition - pullPosition).normalized;
//
//		graphic.rotation = Quaternion.LookRotation(normal.normalized, transform.forward);
//
//		if(currentProjectile != null)
//		{
//			currentProjectile.transform.position = fromPosition - (normal * distance);
//			currentProjectile.transform.forward = normal;
//		}
//	}

	public override void OnPickup (GameObject hand, HTC.UnityPlugin.Vive.HandRole handRole)
	{
		OnGrabbed ();
	}

	public override void OnPutDown ()
	{
		OnRelease ();
	}

	void OnGrabbed () {
		//Instantiate Arrow (instantiate + parent)
		nockedArrow = Instantiate (arrowTemplate, transform.position, Quaternion.identity, transform) as GameObject;
	}

	void OnRelease () {
		//Fire Arrow (unparent + addForce)
		nockedArrow.transform.parent = null;

		nockedArrow.GetComponent<Rigidbody> ().AddForce ((initNotchPos.transform.position - transform.position).normalized * 10, ForceMode.VelocityChange);
	
		transform.position = initNotchPos.transform.position;
	}
}
