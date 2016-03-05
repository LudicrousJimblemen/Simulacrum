using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Simulengine;

public class Select : MonoBehaviour {
	public Vector3 position;
	public Vector3 selection;
	GameObject PersonParent;
	GameObject[] persons;
	
	bool startBox;
	
	void Awake () {
		PersonParent = GameObject.Find ("Persons");
		persons = new GameObject[200];
	}
	
	void Update () {
		for (int i = 0; i < PersonParent.transform.childCount; i ++) {
			persons[i] = PersonParent.transform.GetChild (i).gameObject;
		}
		RaycastHit TerrainHit;
		if (Physics.Raycast (OrthoRay (), out TerrainHit, Mathf.Infinity, 1<<8)) {
			position = TerrainHit.point;
		}
		RaycastHit UnitHit;
		if (Input.GetMouseButtonDown (0)) {
		    if (Physics.Raycast (OrthoRay (), out UnitHit, Mathf.Infinity, 1<<9)) {
				UnitHit.collider.gameObject.GetComponent<Generic> ().Selected = true;
			} else {
				foreach (GameObject p in persons) {
					if (p == null) {
						break;
					}
					if (p.GetComponent<Generic> ().Selected) p.GetComponent<Generic> ().Selected = false;
				}
			}
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
		List<NavMeshAgent> Agents = new List<NavMeshAgent> ();
		foreach (GameObject person in persons) {
			if (person == null) {
				break;
			} 
			if (person.GetComponent<Generic> ().Selected) {
				Agents.Add (person.GetComponent<NavMeshAgent> ());
			}
		}
		if (Agents.Count > 0) {
			Vector3[] Destinations = UnitOrganization.OrganizeFighters (Agents.ToArray (), selection);
			for (int i = 0; i < Agents.Count; i++) {
				Agents[i].destination = Destinations[i];
			}
		}
	}
}
