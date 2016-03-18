using UnityEngine;
using System;
using System.Collections;

public class SpawnPersons : MonoBehaviour {
	public GameObject citizenPrefab;
	private GameObject player;
	private Transform units;

	void Start() {
		player = GameObject.Find("Player");

		units = player.transform.GetChild(0);
		//fighters = player.transform.GetChild (1);
	}

	void Update() {
		try {
			if (units.childCount < Camera.main.GetComponent<Select> ().persons.Length) {
				//spawn single new worker
				if (Input.GetKeyDown(KeyCode.Q)) {
					GameObject newUnit = (GameObject) Instantiate(citizenPrefab, Vector3.zero, Quaternion.identity);
					newUnit.GetComponent<Citizen> ().Behaviour = BehaviourType.StoneMiner;
					newUnit.transform.parent = units.transform;
				}
			
				//spawn single new fighter
				if (Input.GetKeyDown(KeyCode.E)) {
					GameObject newUnit = (GameObject) Instantiate (citizenPrefab,Vector3.zero,Quaternion.identity);
					newUnit.GetComponent<Citizen> ().Behaviour = BehaviourType.Fighter;	
					newUnit.transform.parent = units.transform;
				}
			}
		} catch (IndexOutOfRangeException) {
			print ("too many, dummy");
		}
	}
}
