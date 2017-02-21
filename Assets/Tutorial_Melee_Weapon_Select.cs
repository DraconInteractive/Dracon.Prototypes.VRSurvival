using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Melee_Weapon_Select : MonoBehaviour {

	public static List<GameObject> weapons = new List<GameObject>();

	Vector3 initPos;

	public GameObject holoWall;
	// Use this for initialization
	void Start () {
		initPos = transform.position;
		weapons.Add (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != initPos) {
			foreach (GameObject g in weapons) {
				if (g != this.gameObject) {
					Destroy (g);
				}
			}
			Destroy (holoWall);
			Destroy (GetComponent<Tutorial_Melee_Weapon_Select> ());
		}
	}
}
