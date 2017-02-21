using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysProjectile : Projectile
{
    [HideInInspector]
    new public Rigidbody rigidbody;
    internal virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void Fire(Vector3 direction, Vector3 origin, float velocity)
    {
        transform.position = origin;
        rigidbody.AddForce(direction * velocity, ForceMode.VelocityChange);
    }
}