using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select : MonoBehaviour {
	public Vector3 position;
	public Vector3 selection;
	GameObject persons;
	void Awake () {
		persons = GameObject.Find ("Persons");
	}
	
	void Update () {
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) ){
			position = hit.point;
		}
		if (Input.GetMouseButton (0)) {
			selection = position;
			movePersons ();
		}
	}
	void OnDrawGizmosSelected () {
		Gizmos.DrawSphere (selection, 1);
	}
	void movePersons () {
		for (int i = 0; i < persons.transform.childCount; i++) {
			GameObject obj = persons.transform.GetChild (i).gameObject;
			obj.GetComponent<Person> ().SetDestination (selection);
		}
		/*
		List<NavMeshAgent> PersonAgents = new List<NavMeshAgent> (persons.transform.childCount);
		for (int i = 0; i < PersonAgents.Count; i ++) {
			PersonAgents[i] = (persons.transform.GetChild (i).GetComponent <NavMeshAgent> ());
		}
		List<Vector3> Destinations = UnitOrganization.Organize (PersonAgents, selection);
		for (int i = 0; i < PersonAgents.Count; i ++) {
			PersonAgents[i].destination = Destinations[i];
		}
		*/
	}
}
