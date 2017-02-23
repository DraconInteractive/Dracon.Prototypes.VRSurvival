using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvedUI;

public partial class Player_Main : MonoBehaviour {

	void GazeUpdate () {
		RaycastHit hit;
		Ray ray = new Ray (mainC.transform.position, mainC.transform.forward);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			string t = hit.collider.tag;
			GameObject hitObj = hit.collider.gameObject;
			//			print ("Item Name: " + hitObj.name + " Item Tag: " + t);
			if (t == "NPC") {
				NPC npc = hit.collider.gameObject.GetComponent<NPC> ();
				npc.gazeTrigger = true;
			} 
		}

		List<GameObject> objUnderPointerL = leftCanvas.GetComponent<CurvedUIRaycaster> ().GetObjectsHitByRay (ray);
		List<GameObject> objUnderPointerR = rightCanvas.GetComponent<CurvedUIRaycaster> ().GetObjectsHitByRay (ray);

		List<GameObject> combObj = new List<GameObject> ();

		//		print (combObj.Count);
		combObj.AddRange (objUnderPointerL);
		combObj.AddRange (objUnderPointerR);
//		print (combObj.Count);
		string s = "";
		foreach (GameObject go in combObj) {
			s += "\n" + go.name;
			HandButton b = go.GetComponent<HandButton> ();
			if (b != null) {
				b.gazeTrigger = true;
			} else {
				print ("No HandButton");
			}
		}
		//		print (s);

	}
}
