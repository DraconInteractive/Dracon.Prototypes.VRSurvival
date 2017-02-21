using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParabolicProjectile : Projectile
{
    public override void Fire(Vector3 direction, Vector3 origin, float velocity)
    {
        //transform.position = origin;
        //rigidbody.AddForce(direction * velocity, ForceMode.VelocityChange)
    }
}