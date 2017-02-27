using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAssignButton : Base_Button {

	public Player_Magic.Spell spellToSet;
	Player_Magic pMagic;

	public GameObject activateParticle;

	void Start () {
		pMagic = Player_Magic.pMagic;
	}
	public override void ButtonFunction (GameObject hand) {
		switch (hand.tag) {
		case "PlayerHandL":
			pMagic.leftSpell = spellToSet;
			break;
		case "PlayerHandR":
			pMagic.rightSpell = spellToSet;
			break;
		}

		GameObject g = Instantiate (activateParticle, transform.position, Quaternion.identity) as GameObject;
		Destroy (g, 1);
	}
}
