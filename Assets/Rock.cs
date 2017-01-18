using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public int rockContent;
	// Use this for initialization
	void Start () {
		rockContent = Random.Range (0, 100);
	}

	public void PickAt (int pickStrength) {
		rockContent -= pickStrength;
		if (rockContent <= 0) {
			DestroyRock ();
		}
	}

	public void DestroyRock () {
		
	}
}
