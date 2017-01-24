using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Piece : MonoBehaviour {
	Rigidbody rb;
	Player_Main player;

	public float speed;

	float initTimer;
	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void Start () {
		player = Player_Main.player;
		initTimer = 0;
		rb.AddForce (Vector3.up * 2, ForceMode.Impulse);
	}

	// Update is called once per frame
	void Update () {
		if (initTimer < 1) {
			initTimer += Time.deltaTime;
		} else {
			rb.AddForce ((player.transform.position - transform.position).normalized * speed);
			rb.velocity = Vector3.ClampMagnitude (rb.velocity, 5);
		}

	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			player.woodAmount += 10;
			Destroy (this.gameObject);
		}
	}
}
