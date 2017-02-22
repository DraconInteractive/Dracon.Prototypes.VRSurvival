using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class ReturnToINVButton : HandButton {

	public bool isLeft, isRight;
	public override void ButtonFunc () {
		if (isLeft) {
			player.ReturnToInventory (HandRole.LeftHand, player.leftHandItem);
		} else {
			player.ReturnToInventory (HandRole.RightHand, player.leftHandItem);
		}
	}
}
