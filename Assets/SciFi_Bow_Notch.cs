using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFi_Bow_Notch : Base_Item {

	internal override void Update () {}

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
	}

	void OnRelease () {
		//Fire Arrow (unparent + addForce)
	}
}
