using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Repulsor : Physics_Item {

	bool activated;
	public ParticleSystem ps;
//	Player_Main player;

	internal override void Start ()
	{
		base.Start ();
		activated = false;
		ps.Stop ();
	}

	internal override void Update ()
	{
		base.Update ();

		if (ViveInput.GetPressDown(handRole, ControllerButton.HairTrigger)) {
			player.rb.AddForce (transform.up * 6 * Time.deltaTime, ForceMode.VelocityChange);
			var em = ps.emission;
			em.rateOverTime = 5;
		} else {
			var em = ps.emission;
			em.rateOverTime = 0;
		}
	}
}
