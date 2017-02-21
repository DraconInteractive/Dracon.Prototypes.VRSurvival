using System;
using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class AngrySlingNotch : Base_Item
{
    internal delegate void Grabbed(GameObject hand, HandRole handRole);
    internal delegate void UnGrabbed();

    internal event Grabbed OnGrabbed;
    internal event UnGrabbed OnUnGrabbed;

    internal delegate void TriggerChanged(bool val);

    internal event TriggerChanged OnTrigger;

    [HideInInspector]
    public AngrySlingshot sling;

    internal override void Update() {}

    public override void OnPickup(GameObject hand, HandRole handRole)
    {
        OnGrabbed(hand, handRole);
    }

    public override void OnPutDown()
    {
        OnUnGrabbed();
    }
}