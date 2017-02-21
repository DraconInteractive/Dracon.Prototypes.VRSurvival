using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public abstract class Base_Item : MonoBehaviour {

    [HideInInspector]
    public GameObject controllerObj;
    [HideInInspector]
    public HandRole handRole;

    public bool equipped
    {
        get { return controllerObj != null; }
    }

    internal virtual void Start()
    {

    }
    internal virtual void Update()
    {

    }
    internal virtual void Awake()
    {

    }

    public void PickUp(GameObject hand, HandRole role)
    {
        handRole = role;
        controllerObj = hand;


        Debug.LogFormat("The {0} hand has picked up {1}",role.ToString(),name);
        OnPickup(hand, role);
    }
    /// <summary>Called when the item has been picked up.</summary>
    public abstract void OnPickup(GameObject hand, HandRole handRole);

    public void PutDown()
    {
        controllerObj = null;

        OnPutDown();
    }
    /// <summary>Called when the items has been removed from the hand.</summary>
    public abstract void OnPutDown();
}
