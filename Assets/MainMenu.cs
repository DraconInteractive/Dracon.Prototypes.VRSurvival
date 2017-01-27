using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public static MainMenu menu;
	Player_Main player;
	public Button invButton, playerMenuButton, helpButton, exitButton;

	public int yOffset;
	void Awake () {
		if (menu != null) {
			Destroy (menu.gameObject);
		}
		menu = GetComponent<MainMenu> ();
		exitButton.onClick.AddListener (() => QuitGame ());
	}
	// Use this for initialization
	void Start () {
		player = Player_Main.player;

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 100)) {
			if (hit.collider.gameObject.tag == "Ground") {
				transform.position = hit.point + Vector3.up * yOffset;
				Quaternion.LookRotation ((player.transform.position - transform.position), Vector3.up);
			}
		}
	}
	
	// Update is called once per frame


	void QuitGame () {
		Application.Quit ();
	}
}
