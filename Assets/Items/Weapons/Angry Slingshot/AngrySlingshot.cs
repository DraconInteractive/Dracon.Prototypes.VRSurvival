﻿using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class AngrySlingshot : Item {
    public AudioClip activate;
    public AudioClip strech;
    public AudioClip pull;
    public AudioClip fire;

    public 

    AudioSource sound;
	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}

	void Update () {
        BaseUpdate();
	}

    public override void PickUp(GameObject hand, HandRole handType)
    {
        base.PickUp(hand, handType);
        sound.PlayOneShot(activate);
    }
}