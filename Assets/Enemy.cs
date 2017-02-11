using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Animator anim;

	List<GameObject> targets = new List<GameObject>();
    public List<GameObject> detectedTargets = new List<GameObject>();

	Rigidbody rb;
	public GameObject attackTarget;
	public GameObject eye;

	public int health, maxHealth;

	bool inCombat;

	bool attacking;
	float attackTimer;
	public float attackTimerTarget;
	public float detectionRange, attackingRange;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		if (anim == false) {
			anim = GetComponentInChildren<Animator> ();
		}
	}
	// Use this for initialization
	void Start () {
		targets.Add (Player_Main.player.gameObject);
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		Detection ();
		if (inCombat) {
			Combat ();
		}
		TimerUpdate ();
	}

	void Detection () {
		for (int i = 0; i < targets.Count; i++) {
			if (InDetectionRange(targets[i])) {
				if (Quaternion.Angle (transform.rotation, Quaternion.LookRotation (targets [i].transform.position - transform.position)) < 90) {
					RaycastHit hit;
					Ray ray = new Ray (eye.transform.position, targets [i].transform.position - eye.transform.position);
					//				Ray ray = new Ray (eye.transform.position, eye.transform.forward);

					if (Physics.Raycast(ray, out hit, 100)) {
						GameObject hObj = hit.collider.gameObject;
						if (hObj == targets[i]) {
							if (!detectedTargets.Contains(hObj)) {
								detectedTargets.Add (hObj);
							}
						}
					}			
				}
			}

		}

		if (detectedTargets.Count > 0) {
			inCombat = true;
		} else {
			inCombat = false;
		}
		anim.SetBool ("inCombat", inCombat);
	}

	void Combat () {
		FindClosestTarget ();
		if (attackTimer >= attackTimerTarget && InAttackingRange(attackTarget)) {
			Attack ();
		} else {
			MoveToTarget ();
		}
	}

	void Attack () {
		attackTimer = 0;
		anim.SetTrigger ("Attack");
	}
		
	void MoveToTarget () {
		if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(attackTarget.transform.position - transform.position, Vector3.up)) > 1) {
			rb.MoveRotation (Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (attackTarget.transform.position - transform.position, Vector3.up),0.1f));
		}
	}

	void TimerUpdate () {
		if (attackTimer < attackTimerTarget) {
			attackTimer += Time.deltaTime;
		}
	}

	bool InDetectionRange (GameObject t) {
		if (Vector3.Distance(t.transform.position, transform.position) < detectionRange) {
			return true;
		}
		return false;
	}

	bool InAttackingRange (GameObject t) {
		if (Vector3.Distance(t.transform.position, transform.position) < attackingRange) {
			return true;
		}
		return false;
	}

	void FindClosestTarget () {
		float dist = detectionRange;
		for (int i = 0; i < detectedTargets.Count; i++) {
			float f = Vector3.Distance(transform.position, detectedTargets[i].transform.position);
			if (f < dist) {
				dist = f;
				attackTarget = detectedTargets [i];
			}
		}
	}
	public void Damage (int damage) {
		health -= damage;
		if (health <= 0) {
			Destroy (this.gameObject);
		}
	}
}
