﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public float activationDistance;

	public float yOffset;

	Player_Main player;

	Vector3 initPos;

	Vector3 doorVel;
	// Use this for initialization
	void Start () {
		player = Player_Main.player;
		initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckPlayerDist()) {
			transform.position = Vector3.SmoothDamp(transform.position, initPos + Vector3.up * yOffset, ref doorVel, 0.1f);
		} else {
			transform.position = Vector3.SmoothDamp(transform.position, initPos, ref doorVel, 0.1f);
		}
	}

	bool CheckPlayerDist () {
		if (Vector3.Distance(player.transform.position, transform.position) < activationDistance) {
			return true;
		}	

		return false;
	}
}
