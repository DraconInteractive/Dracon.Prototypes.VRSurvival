using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Repulsor : Physics_Item {

	bool activated;

//	Player_Main player;

	internal override void Start ()
	{
		base.Start ();
		activated = false;
	}

	internal override void Update ()
	{
		base.Update ();

		if (ViveInput.GetPressDown(handRole, ControllerButton.HairTrigger)) {
			player.rb.AddForce (controllerObj.transform.forward * 6 * ViveInput.GetTriggerValue(handRole, false) * Time.deltaTime, ForceMode.VelocityChange);
		}
//		if (activated) {
//			
//		}

//		ViveInput.GetTrig
	}
}
