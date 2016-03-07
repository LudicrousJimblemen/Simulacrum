using UnityEngine;
using System;
using System.Collections;

public class SpawnPersons : MonoBehaviour {

	public GameObject fighterPrefab;
	private GameObject player;

	void Start() {
		player = GameObject.Find("Persons");
	}

	void Update() {
		//spawn single new unit
		if (Input.GetKeyDown(KeyCode.Q)) {
			GameObject newUnit = (GameObject) Instantiate(fighterPrefab, Vector3.zero, Quaternion.identity);
			newUnit.transform.parent = player.transform;
		}

		//spawn mass of new units
		if (Input.GetKeyDown(KeyCode.E)) {
			for (int i = 0; i < 5; i++) {
				GameObject newUnit = (GameObject) Instantiate(fighterPrefab, Vector3.zero, Quaternion.identity);
				newUnit.transform.parent = player.transform;
			}
		}
	}
}
