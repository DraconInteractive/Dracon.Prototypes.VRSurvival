using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class FetchFromINVButton : HandButton {

	public bool isLeft, isRight;
	public Base_Item.ItemType fetchType;
	public override void ButtonFunc () {

		if (isLeft) {
			player.FetchFromInventory (HandRole.LeftHand, fetchType);
		} else {
			player.FetchFromInventory (HandRole.RightHand, fetchType);
		}
	}
}
