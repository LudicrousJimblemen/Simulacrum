using UnityEngine;
using System;
using System.Collections;

public class SpawnPersons : MonoBehaviour {
	public GameObject citizenPrefab;
	private GameObject player;
	private Transform workers;
	private Transform fighters;

	void Start() {
		player = GameObject.Find("Player");
		workers = player.transform.GetChild(0);
		fighters = player.transform.GetChild (1);
	}

	void Update() {
		//spawn single new worker
		if (Input.GetKeyDown(KeyCode.Q)) {
			GameObject newUnit = (GameObject) Instantiate(citizenPrefab, Vector3.zero, Quaternion.identity);
			newUnit.GetComponent<Citizen> ().Behaviour = BehaviourType.StoneMiner;
			newUnit.transform.parent = workers.transform;
		}

		//spawn single new fighter
		if (Input.GetKeyDown(KeyCode.E)) {
			GameObject newUnit = (GameObject) Instantiate (citizenPrefab,Vector3.zero,Quaternion.identity);
			newUnit.GetComponent<Citizen> ().Behaviour = BehaviourType.Fighter;
			newUnit.transform.parent = fighters.transform;
		}
	}
}
