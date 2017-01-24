using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_P : MonoBehaviour {
	[HideInInspector]
	public int woodContent;

	float timer, timerTarget;

	public GameObject woodPiece;
	void Start () {
		woodContent = Random.Range (0, 100);
		timer = 0;
		timerTarget = 1;
	}

	void Update () {
		if (timer < timerTarget) {
			timer += Time.deltaTime;
		}
	}

	public void Chop (int axeStrength) {
		if (timer >= timerTarget) {
			woodContent -= axeStrength;
//			hitParticle.Play ();
			if (woodContent <= 0) {
				DestroyTree ();
			}
			timer = 0;
		}
	}

	void DestroyTree () {
//		ParticleSystem d = Instantiate (destroyParticle, transform.position, Quaternion.identity) as ParticleSystem;
		for (int i = 0; i < 10; i++) {
			/*GameObject p = */
			Instantiate (woodPiece, transform.position + Vector3.one * Random.Range (-1.0f, 1.0f), Quaternion.identity)/* as GameObject*/;
		}
//		Destroy (d.gameObject, 1);
		Destroy (this.gameObject);
	}
}
