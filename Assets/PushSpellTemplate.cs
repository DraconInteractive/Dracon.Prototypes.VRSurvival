using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSpellTemplate : MonoBehaviour {
	public GameObject targetHand;
	LineRenderer[] l;
	void Awake () {
		l = GetComponentsInChildren<LineRenderer> ();
	}
	// Use this for initialization
	void Start () {
		foreach (LineRenderer line in l) {
			line.numPositions = 2;
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (LineRenderer line in l) {
			line.SetPosition (0, targetHand.transform.position);
			line.SetPosition (1, targetHand.transform.position + targetHand.transform.forward * 10);
		}

	}
}
