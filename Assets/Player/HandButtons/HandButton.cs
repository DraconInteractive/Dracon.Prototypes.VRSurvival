using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandButton : MonoBehaviour {
//	public float f;

//	Button thisButton;
	Image i;
//	Color initColor;

	public bool gazeTrigger;
	float gazeTimer = 0;
	[HideInInspector]
	public Player_Main player;
	void Awake () {
//		thisButton = GetComponent<Button> ();
		i = GetComponent<Image> ();
	}

	void Start () {
//		initColor = i.color;
		player = Player_Main.player;
//		f = 1;
	}

	// Update is called once per frame
	void Update () {
		if (gazeTrigger) {
			SetTrigger ();
		} else {
			DisableTrigger ();
		}
		if (gazeTimer > 1) {
			ButtonFunc ();
			gazeTimer = 0;
		}
//		if (f > 1) {
//			Color c = i.color;
//			i.color = new Color (c.r, c.g, c.b, 1);
//		}
		gazeTrigger = false;
	}

	void SetTrigger () {
		Color c = i.color;
		i.color = new Color (c.r, c.g, c.b, 0.25f);
		gazeTimer += Time.deltaTime;
//		f = 0;
	}

	void DisableTrigger () {
		Color c = i.color;
		i.color = new Color (c.r, c.g, c.b, 1);
		gazeTimer = 0;
	}

	public virtual void ButtonFunc () {
		print ("ButtonFunc");
	}
		
}
