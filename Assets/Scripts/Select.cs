using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Simulengine;

public class Select : MonoBehaviour {
	public Vector3 position;
	public Vector3 selection;
	GameObject persons;
	
	bool startBox;
	
	void Awake () {
		persons = GameObject.Find ("Persons");
	}
	
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (OrthoRay (), out hit) ) {
			position = hit.point;
		}
		if (Input.GetMouseButton (1)) {
			selection = position;
			movePersons ();
		}
	}
	void OnDrawGizmosSelected () {
		Gizmos.DrawSphere (selection, 1);
	}
	Ray OrthoRay () {
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (Input.mousePosition), Camera.main.gameObject.transform.forward);
		return ray;
	}
	void movePersons () {
		/*
		for (int i = 0; i < persons.transform.childCount; i++) {
			GameObject obj = persons.transform.GetChild (i).gameObject;
			obj.GetComponent<Person> ().SetDestination (selection);
		}
		*/
		NavMeshAgent[] PersonAgents = new NavMeshAgent[persons.transform.childCount];
		for (int i = 0; i < PersonAgents.Length; i ++) {
			PersonAgents[i] = (persons.transform.GetChild (i).GetComponent <NavMeshAgent> ());
		}
		Vector3[] Destinations = UnitOrganization.Organize (PersonAgents, selection);
		for (int i = 0; i < PersonAgents.Length; i++) {
			PersonAgents[i].destination = Destinations[i];
		}
	}
}
