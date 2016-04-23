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
		if (units.childCount < Camera.main.GetComponent<Select>().persons.Length) {
			if (Input.GetKeyDown(KeyCode.Q)) {
				SummonCitizen(BehaviourType.StoneMiner);
            }
		}
		if (units.childCount < Camera.main.GetComponent<Select> ().persons.Length) {
			if (Input.GetKeyDown (KeyCode.E)) {
				SummonCitizen (BehaviourType.Fighter);
			}
		}
	}

	public void SummonCitizen(BehaviourType type) {
		GameObject newUnit = (GameObject) Instantiate(citizenPrefab, Vector3.zero, Quaternion.identity);
		newUnit.GetComponent<Citizen>().Behaviour = type;
		newUnit.transform.parent = units.transform;
	}
}
