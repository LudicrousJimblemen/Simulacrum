using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour {

	public bool IsLocal = true;
	public GameObject CitizenPrefab;

	public int Stone;
	
	private Transform units;

	void Start() {

		units = transform.GetChild(0);
	}

	void Update() {
		if (IsLocal) {
			GameObject.Find("UIStoneText").GetComponent<Text>().text = String.Format("Stone\n{0}", Stone);

			if (units.childCount < Camera.main.GetComponent<Select>().persons.Length) {
				if (Input.GetKeyDown(KeyCode.Q)) {
					SummonCitizen(BehaviourType.StoneMiner);
				}
			}
		}
	}

	public void SummonCitizen(BehaviourType type) {
		GameObject newUnit = (GameObject) Instantiate(CitizenPrefab, Vector3.zero, Quaternion.identity);
		newUnit.GetComponent<Citizen>().Behaviour = type;
		newUnit.transform.parent = units.transform;
	}
}
