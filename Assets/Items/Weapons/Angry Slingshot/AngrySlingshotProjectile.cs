using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AngrySlingshotProjectile : PhysProjectile
{
    AudioSource sound;

    public AudioClip deathSound;
    public AudioClip spawnSound;
    public AudioClip launchSound;
    public AudioClip bounceSound;

    public Component[] componetsToDestroy;

    ParticleSystem particles;

    public int totalBounces;
    bool hitGround;

    public override void Fire(Vector3 direction, Vector3 origin, float velocity)
    {
        base.Fire(direction, origin, velocity);
        sound.PlayOneShot(launchSound);
        hitGround = false;
        Invoke("Die", 20f);
        particles.Emit(50);
    }

    internal override void Awake()
    {
        base.Awake();
        sound = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        sound.PlayOneShot(spawnSound);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (totalBounces < 0)
            Die();
        else
            sound.PlayOneShot(bounceSound);

        totalBounces--;

        CancelInvoke("Die");
        Invoke("Die", 1f);
    }
    public void Die()
    {
        CancelInvoke("Die");

        rigidbody.isKinematic = true;
        for (int i = 0; i < componetsToDestroy.Length; i++)
        {
            Destroy(componetsToDestroy[i]);
        }
        Destroy(gameObject, 5f);
        sound.PlayOneShot(deathSound);
        particles.Emit(250);
    }
}
