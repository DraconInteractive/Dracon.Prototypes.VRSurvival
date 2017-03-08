using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SciFi_BowString : MonoBehaviour {

	LineRenderer stringRenderer;
	public GameObject nock, stringUpper, stringLower;
	// Use this for initialization
	void Start () {
		stringRenderer = GetComponent<LineRenderer> ();
	}	
	
	// Update is called once per frame
	void Update () {
		if (nock != null) {
			stringRenderer.SetPosition (0, stringUpper.transform.position);
			stringRenderer.SetPosition (1, nock.transform.position);
			stringRenderer.SetPosition (2, stringLower.transform.position);
		}

	}
}
