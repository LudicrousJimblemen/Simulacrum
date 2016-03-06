using UnityEngine;
using System.Collections;

public class SpawnPersons : MonoBehaviour {

	public GameObject Fighter;

	void Update () {
		if (transform.childCount < Camera.main.GetComponent<Select> ().persons.Length) { 
			//spawn single new unit
			if (Input.GetKeyDown (KeyCode.Q)) {
				GameObject newUnit = (GameObject) Instantiate (Fighter, Vector3.zero, Quaternion.identity);
				newUnit.transform.parent = transform;
			}

			//spawn mass of new units
			if (Input.GetKey (KeyCode.E)) {
				GameObject newUnit = (GameObject) Instantiate (Fighter,Vector3.zero,Quaternion.identity);
				newUnit.transform.parent = transform;
			}
		} else {
			print ("Max Unit Count Reached");
		}
	}
}
