using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : MonoBehaviour {

	ParticleSystem ps;

	float timer = 0, timerTarget = 1;

	int craftProgress;

	public List<GameObject> objTemplate;

	int currentSelection, selectionRange;

	public GameObject anvilMenu;
	public Button m_leftArrow, m_rightArrow, c_menuToggle;
	public Text m_Item;

	void Awake () {
		ps = GetComponentInChildren<ParticleSystem> ();
		m_leftArrow.onClick.AddListener (() => M_LeftArrow ());
		m_rightArrow.onClick.AddListener (() => M_RightArrow ());
		c_menuToggle.onClick.AddListener (() => ToggleAnvilMenu ());
	}

	void Start () {
		craftProgress = 0;
		timer = 0;
		timerTarget = 1;

		selectionRange = objTemplate.Count;
		currentSelection = 0;

		ToggleAnvilMenu ();
	}

	void Update () {
		if (timer <= timerTarget) {
			timer += Time.deltaTime;
		}
		UpdateUI ();
	}

	public void AnvilStrike () {
		if (timer >= 1) {
			ps.Play ();
			Progress ();
			timer = 0;
		}
	}

	void Craft (GameObject obj) {
		Instantiate (objTemplate[currentSelection], transform.position + Vector3.up * 1.5f, Quaternion.identity);
	}

	void Progress () {
		craftProgress += 10;
		if (craftProgress >= 100) {
			Craft (objTemplate[currentSelection]);
			craftProgress = 0;
		}
	}

	public void ToggleAnvilMenu () {
		anvilMenu.gameObject.SetActive (!anvilMenu.activeSelf);
	}

	void UpdateUI () {
		m_Item.text = objTemplate [currentSelection].name;
	}

	void M_LeftArrow () {
		if (currentSelection == 0) {
			currentSelection = selectionRange;
		} else {
			currentSelection--;
		}
	}

	void M_RightArrow () {
		if (currentSelection == selectionRange) {
			currentSelection = 0;
		} else {
			currentSelection++;
		}
	}
}
