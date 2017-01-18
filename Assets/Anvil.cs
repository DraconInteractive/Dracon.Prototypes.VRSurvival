using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour {

	ParticleSystem ps;

	void Awake () {
		ps = GetComponentInChildren<ParticleSystem> ();
	}

	void AnvilStrike () {
		ps.Play ();
	}
}
