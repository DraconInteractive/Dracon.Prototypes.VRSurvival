using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_02 : NPC {

	public GameObject npcUI;

	public override void BeginInteraction () {
		base.BeginInteraction ();
		GetComponent<BoxCollider> ().enabled = true;
		npcUI.SetActive (true);
	}

	public override void EndInteraction () {
		base.EndInteraction ();
		GetComponent<BoxCollider> ().enabled = false;
		npcUI.SetActive (false);
	}
}
