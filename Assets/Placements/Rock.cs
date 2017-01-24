using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int rockContent;

	public ParticleSystem hitParticle, destroyParticle;

	float timer, timerTarget;

	public GameObject rockPiece;
	// Use this for initialization
	void Start () {
		rockContent = Random.Range (0, 100);
		timer = 0;
		timerTarget = 1;
	}

	void Update () {
		if (timer < timerTarget) {
			timer += Time.deltaTime;
		}
	}

	public void PickAt (int pickStrength) {
		if (timer >= timerTarget) {
			rockContent -= pickStrength;
			hitParticle.Play ();
			if (rockContent <= 0) {
				DestroyRock ();
			}
			timer = 0;
		}

	}

	public void DestroyRock () {
		ParticleSystem d = Instantiate (destroyParticle, transform.position, Quaternion.identity) as ParticleSystem;
		for (int i = 0; i < 10; i++) {
			/*GameObject p = */
			Instantiate (rockPiece, transform.position + Vector3.one * Random.Range (-1.0f, 1.0f), Quaternion.identity)/* as GameObject*/;
		}
		Destroy (d.gameObject, 1);
		Destroy (this.gameObject);
	}
}
