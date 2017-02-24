using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using System;

public class Physics_Item : Base_Item
{
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Vector3 itemVel;
    [HideInInspector]
    public Vector3 initPos;
    [HideInInspector]
    public bool initStage;
    [HideInInspector]
    public Player_Main player;

    

    public bool dropOnAwake;

   


    internal override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        initStage = true;
        initPos = transform.position;
        //		rb.isKinematic = true;

        if (dropOnAwake)
        {
            base.PutDown();
        }
    }

    internal override void Start()
    {
        base.Start();

        player = Player_Main.player;
    }

    [System.Obsolete("This is obsolete, please override start instead.")]
    public void BaseStart()
    {
        player = Player_Main.player;
    }
    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        if (equipped)
        {
            transform.position = Vector3.SmoothDamp(transform.position, controllerObj.transform.position, ref itemVel, 0.025f);
            transform.rotation = Quaternion.Lerp(transform.rotation, controllerObj.transform.rotation, 0.8f);
        }
        else if (initStage)
            transform.position = initPos;
    }
    [System.Obsolete("This is obsolete, please override update instead.")]
    public void BaseUpdate()
    {
        if (equipped)
        {

            //			rb.MovePosition (Vector3.Lerp (transform.position, controllerObj.transform.position, 0.8f));
            //			rb.MoveRotation (Quaternion.Lerp (transform.rotation, controllerObj.transform.rotation, 0.8f));
            transform.position = Vector3.SmoothDamp(transform.position, controllerObj.transform.position, ref itemVel, 0.025f);
            transform.rotation = Quaternion.Lerp(transform.rotation, controllerObj.transform.rotation, 0.8f);

        }
        else if (initStage)
        {
            transform.position = initPos;
        }
    }

    public override void OnPickup(GameObject hand, HandRole handRole)
    {
        initStage = false;
        rb.useGravity = false;
        print(name + " picked up");
		transform.parent = null;
		rb.isKinematic = false;
    }

    public override void OnPutDown()
    {
        initStage = false;
        rb.useGravity = true;
    }
    [Obsolete("Physics_Item.PickUp is obsolete, please use OnPickup instead.")]
    public virtual void PickUp(GameObject hand, HandRole handType)
    {
        Debug.LogWarning("Old Method");

        initStage = false;
        /*equipped = true;
        controllerObj = hand;
        handRole = handType;*/
        rb.useGravity = false;
		GetComponent<BoxCollider> ().enabled = false;
        print(name + " picked up");
    }
    [Obsolete("Physics_Item.PutDown is obsolete, please use OnPutDown instead.")]
    public virtual void PutDown()
    {
        Debug.LogWarning("Old Method");

        initStage = false;
        /*equipped = false;
        controllerObj = null;*/
        rb.useGravity = true;
		GetComponent<BoxCollider> ().enabled = true;
    }

    [System.Obsolete("This is obsolete, Please override awake instead.")]
    public void SetupFunc()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        initStage = true;
        initPos = transform.position;
        //		rb.isKinematic = true;

        if (dropOnAwake)
        {
            PutDown();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        ItemCollision(col);
    }

    public virtual void ItemCollision(Collision col)
    {

    }
}