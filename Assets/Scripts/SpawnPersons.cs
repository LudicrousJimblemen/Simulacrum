using UnityEngine;
using System;
using System.Collections;

public class SpawnPersons : MonoBehaviour {

	public GameObject fighterPrefab;
	private GameObject player; 

	void Start(){
		player = GameObject.Find("Persons");
    }

	void Update () {
		try { 
			//spawn single new unit
            if (Input.GetKeyDown (KeyCode.Q)) {
				GameObject newUnit = (GameObject) Instantiate(fighterPrefab, Vector3.zero, Quaternion.identity);
				newUnit.transform.parent = player.transform;
			}

			//spawn mass of new units
			/*
			if (Input.GetKey (KeyCode.E)) {
				GameObject newUnit = (GameObject) Instantiate (Fighter,Vector3.zero,Quaternion.identity);
				newUnit.transform.parent = transform;
			}
			no thanks man*/
		} catch (IndexOutOfRangeException) {
			print ("Max Unit Count Reached Nerd");
		}
	}
}
