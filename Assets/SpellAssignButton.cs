using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAssignButton : Base_Button {

	public Player_Main.Spell spellToSet;

	public override void ButtonFunction (GameObject hand) {
		switch (hand.tag) {
		case "PlayerHandL":
			Player_Main.player.leftSpell = spellToSet;
			break;
		case "PlayerHandR":
			Player_Main.player.rightSpell = spellToSet;
			break;
		}
	}
}
