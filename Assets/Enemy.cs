using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	List<GameObject> targets = new List<GameObject>();
    public List<GameObject> detectedTargets = new List<GameObject>();
	public GameObject eye;

	public int health, maxHealth;
	// Use this for initialization
	void Start () {
		targets.Add (Player_Main.player.gameObject);
		health = maxHealth;
	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}

	void Detection () {
		for (int i = 0; i < targets.Count; i++) {
			if (Quaternion.Angle (transform.rotation, Quaternion.LookRotation (targets [i].transform.position - transform.position)) < 90) {
				RaycastHit hit;
				Ray ray = new Ray (eye.transform.position, eye.transform.forward);

				if(Physics.Raycast(ray, out hit, 100)) {
					GameObject hObj = hit.collider.gameObject;
					if (hObj == targets[i]) {
						if (!detectedTargets.Contains(hObj)) {
							detectedTargets.Add (hObj);
						}
					}
				} else {
					if (detectedTargets.Contains(hit.collider.gameObject)) {
						detectedTargets.Remove (hit.collider.gameObject);
					}
				}
			} else {
				if (detectedTargets.Contains(targets[i])) {
					detectedTargets.Remove (targets[i]);
				}
			}
				
		}
	}

	public void Damage (int damage) {
		health -= damage;
		if (health <= 0) {
			Destroy (this.gameObject);
		}
	}
}
