using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour {

	public enum Item {Shield, Sword, Torch, None};
	public Item startingL, startingR;


	public GameObject shieldL, shieldR, sword, torch;

	public GameObject leftHandPoint, rightHandPoint;
	// Use this for initialization
	void Start () {
		switch (startingL)
		{
		case Item.Shield:
			Instantiate (shieldL, leftHandPoint.transform.position, Quaternion.identity, leftHandPoint.transform);
			break;
		case Item.Sword:
			Instantiate (sword, leftHandPoint.transform.position, Quaternion.identity, leftHandPoint.transform);
			break;
		case Item.Torch:
			Instantiate (torch, leftHandPoint.transform.position, Quaternion.identity, leftHandPoint.transform);
			break;
		case Item.None:
			
			break;
		}

		switch (startingR)
		{
		case Item.Shield:
			Instantiate (shieldR, rightHandPoint.transform.position, Quaternion.identity, rightHandPoint.transform);
			break;
		case Item.Sword:
			Instantiate (sword, rightHandPoint.transform.position, Quaternion.identity, rightHandPoint.transform);
			break;
		case Item.Torch:
			Instantiate (torch, rightHandPoint.transform.position, Quaternion.identity, rightHandPoint.transform);
			break;
		case Item.None:
			break;
		}
	}
}
