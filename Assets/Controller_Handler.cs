using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Handler : MonoBehaviour {

	public Player_Main player;

	public bool leftHand, rightHand;

	public SteamVR_Controller.Device device;

	// Use this for initialization
	void OnEnable () {

		if (player != null) {
			if (leftHand) {
				SteamVR_TrackedController LH = GetComponent<SteamVR_TrackedController> ();
				player.leftHand = LH;
				device = SteamVR_Controller.Input ((int)player.leftHand.controllerIndex);
				player.leftDevice = device;
				player.leftHandC = GetComponent<Controller_Handler> ();
				LH.Gripped += player.PickUpWithLeft;
			} else if (rightHand) {
				SteamVR_TrackedController RH = GetComponent<SteamVR_TrackedController> ();
				player.rightHand = RH;
				device = SteamVR_Controller.Input ((int)player.rightHand.controllerIndex);
				player.rightDevice = device;
				player.rightHandC = GetComponent<Controller_Handler> ();
				RH.Gripped += player.PickUpWithRight;
			}
		} else {
			player = FindObjectOfType<Player_Main> ();
			if (leftHand) {
				SteamVR_TrackedController LH = GetComponent<SteamVR_TrackedController> ();
				player.leftHand = LH;
				device = SteamVR_Controller.Input ((int)player.leftHand.controllerIndex);
				player.leftDevice = device;
				player.leftHandC = GetComponent<Controller_Handler> ();
				LH.Gripped += player.PickUpWithLeft;
			} else if (rightHand) {
				SteamVR_TrackedController RH = GetComponent<SteamVR_TrackedController> ();
				player.rightHand = RH;
				device = SteamVR_Controller.Input ((int)player.rightHand.controllerIndex);
				player.rightDevice = device;
				player.rightHandC = GetComponent<Controller_Handler> ();
				RH.Gripped += player.PickUpWithRight;
			}
		}


	}
	
	// Update is called once per frame
	void OnDisable () {
		
	}
}
