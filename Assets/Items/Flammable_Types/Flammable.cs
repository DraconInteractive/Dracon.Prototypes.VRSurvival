using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour {

	public ParticleSystem ps;
	public GameObject fLight;
	public bool ignited, igniteOnAwake;
	// Use this for initialization
	void Start () {
		if (igniteOnAwake) {
			Ignite ();
		} else {
			Extinguish ();
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		print ("hit obj");
		if (col.gameObject.tag == "Flammable") {
			print ("Hit Flammable");
			Flammable f = col.gameObject.GetComponent<Flammable> ();
			if (f.ignited) {
				if (!ignited) {
					Ignite ();
				}
			}
		}
	}

	public void Ignite () {
		ps.Play ();
		fLight.SetActive (true);
		ignited = true;
	}

	public void Extinguish () {
		ps.Stop ();
		fLight.SetActive (false);
		ignited = false;
	}
}
