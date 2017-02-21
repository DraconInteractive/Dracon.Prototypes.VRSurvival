using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class AngrySlingshot : Physics_Item
{
    public AudioClip activate;
    public AudioClip stretch;
    public AudioClip fire;

    public Transform graphic;
    public Transform sling;

    public AngrySlingNotch notch;

    public float maxiumDistance = 0.31f;
    public float maxiumSlingScale = 1.3f;

    bool notchGrabbed;
    bool soundPlayed = false;

    public Vector3 slingForkPoint;

    AudioSource sound;

    public Animation anim;
    AnimationState animation;



    internal override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();

        if (notch == null)
            notch = GetComponentInChildren<AngrySlingNotch>();

        if (notch == null)
            return;

        notch.OnGrabbed += Notch_OnGrabbed;
        notch.OnUnGrabbed += Notch_OnUnGrabbed;
        notch.OnTrigger += Notch_OnTrigger;

        animation = anim["Slingshot Stretch"];

        SetSlingStrech(0.15f);
    }

    private void Notch_OnTrigger(bool val)
    {
        throw new System.NotImplementedException();
    }

    private void Notch_OnUnGrabbed()
    {
        if (notchGrabbed)
            Fire();

        notchGrabbed = false;

        SetSlingStrech(0.15f);
        graphic.forward = -transform.up;
    }

    private void Notch_OnGrabbed(GameObject hand, HandRole handRole)
    {
        notchGrabbed = true;
        soundPlayed = false;
    }

    internal override void Update()
    {
        base.Update();
        if (!notchGrabbed)
            return;

        Vector3 pullPosition = notch.controllerObj.transform.position;
        Vector3 fromPosition = transform.TransformPoint(slingForkPoint);

        float distance = Vector3.Distance(fromPosition, pullPosition);
        distance = Mathf.Clamp(distance, 0, maxiumDistance);

        SetSlingStrech(distance / maxiumDistance);

        Vector3 normal = fromPosition - pullPosition;

        graphic.rotation = Quaternion.LookRotation(normal.normalized, transform.forward);
    }

    public override void OnPickup(GameObject hand, HandRole handRole)
    {
        base.OnPickup(hand, handRole);
        sound.PlayOneShot(activate);
        notch.interactable = true;

        rb.isKinematic = true;


        // Make this sling not intractable so it can't switch hands on us.
        interactable = false;

    }
    public override void OnPutDown()
    {
        rb.isKinematic = false;

        base.OnPutDown();
        notch.PutDown();

        notch.interactable = false;

        SetSlingStrech(0.15f);



        // Make this intractable again so we can pick it back up again.
        interactable = true;
    }

    void SetSlingStrech(float ammount)
    {
        animation.normalizedTime = ammount;

        if (ammount > 0.4f)
            AnimPlaySound();

        animation.speed = 0f;
    }

    void AnimPlaySound()
    {
        if (soundPlayed)
            return;

        soundPlayed = true;
        sound.PlayOneShot(stretch);
    }

    public void Fire()
    {
        sound.PlayOneShot(fire);
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.TransformPoint(slingForkPoint);
        float size = UnityEditor.HandleUtility.GetHandleSize(pos);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(pos, size * 0.3f);

        Gizmos.DrawRay(pos, transform.up * maxiumDistance);
        Gizmos.DrawWireSphere(pos + (transform.up * maxiumDistance), size * 0.1f);
    }
#endif
}
