using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public abstract void Fire(Vector3 direction, Vector3 origin, float velocity);
}