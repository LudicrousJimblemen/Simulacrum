using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Simulengine;

public class Select : MonoBehaviour {
	public Vector3 position;
	public Vector3 selection;
	GameObject PersonParent;
	GameObject[] persons;

	public Vector3 Marquee1;
	public Vector3 Marquee2;
	public bool inMarquee = false;
	Rect MarqueeRect;
	
	void Awake () {
		PersonParent = GameObject.Find ("Persons");
		persons = new GameObject[200];
		MarqueeRect = new Rect ();
	}
	
	void Update () {
		for (int i = 0; i < PersonParent.transform.childCount; i ++) {
			persons[i] = PersonParent.transform.GetChild (i).gameObject;
		}
		RaycastHit TerrainHit;
		if (Physics.Raycast (OrthoRay (), out TerrainHit, Mathf.Infinity,~(1 << 9))) {
			position = TerrainHit.point;
		}
		RaycastHit UnitHit;
		if (Input.GetMouseButtonDown (0)) {
		    if (Physics.Raycast (OrthoRay (), out UnitHit, Mathf.Infinity,~(1 << 8)) && !UnitHit.collider.gameObject.GetComponent<Generic> ().Selected) {
				UnitHit.collider.gameObject.GetComponent<Generic> ().Selected = true;
				foreach (GameObject p in persons) {
					if (p == null) {
						break;
					}
					if (p.GetComponent<Generic> ().Selected && p != UnitHit.collider.gameObject) p.GetComponent<Generic> ().Selected = false;
				}
			} else {
				foreach (GameObject p in persons) {
					if (p == null) {
						break;
					}
					if (p.GetComponent<Generic> ().Selected) p.GetComponent<Generic> ().Selected = false;
				}
			}
			inMarquee = true;
			Marquee1 = Input.mousePosition;
		}
		if (inMarquee) {
			MarqueeSelection ();
			foreach (GameObject unit in persons) {
				if (unit == null) {
					break;
				} else {
					Vector3 unitScreenPosition = Camera.main.WorldToScreenPoint (unit.transform.position);
					Vector3 unitScreenPoint = new Vector3 (unitScreenPosition.x,Screen.height - unitScreenPosition.y);
					if (MarqueeRect.Contains (unitScreenPoint)) {
						unit.GetComponent<Generic> ().Selected = true;
					}
				}
			}
		}
		/*
		RaycastHit Marquee;
		if (Input.GetMouseButton (0)) {
			if (Physics.Raycast (OrthoRay (),out Marquee,Mathf.Infinity,~(1<<9))) {
				if (!inMarquee) {
					Marquee1 = Marquee.point;
					inMarquee = true;
				} else {
					Marquee2 = Marquee.point;
				}
			}
		}
		foreach (GameObject unit in persons) {
			if (unit == null) {
				break;
			} else {
				//unit.GetComponent<Generic> ().Selected = true;
			}
		}
		*/
		if (Input.GetMouseButtonUp (0) && inMarquee) {
			inMarquee = false;
		}
		if (Input.GetMouseButton (1)) {
			selection = position;
			movePersons ();
		}
	}
	void OnDrawGizmosSelected () {
		Gizmos.DrawSphere (selection, 1);
		Gizmos.DrawSphere (Camera.main.ScreenToWorldPoint (Marquee1),.1f);
		Gizmos.DrawSphere (Camera.main.ScreenToWorldPoint (Marquee2),.1f);
	}
	Ray OrthoRay () {
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (Input.mousePosition), Camera.main.gameObject.transform.forward);
		return ray;
	}

	void MarqueeSelection () {
		Marquee2 = Input.mousePosition;
	}

	void OnGUI () {
		if (inMarquee) {
			float[] maxmin = {Mathf.Max (Marquee1.x, Marquee2.x), //Max x
							Mathf.Min (Marquee1.x, Marquee2.x), //Min x
							Mathf.Max (Screen.height - Marquee1.y, Screen.height - Marquee2.y), //Max y
							Mathf.Min (Screen.height - Marquee1.y, Screen.height - Marquee2.y), //Min y
			};
			MarqueeRect.xMax = maxmin[0];
			MarqueeRect.xMin = maxmin[1];
			MarqueeRect.yMax = maxmin[2];
			MarqueeRect.yMin = maxmin[3];
			GUI.DrawTexture (MarqueeRect,MarqueeTexture ());
		}
	}

	Texture2D MarqueeTexture () {
		Texture2D marquee = new Texture2D (1,1,TextureFormat.ARGB32,false);
		Color32[] pixels = { new Color32 (0,0,255,50) };
		marquee.SetPixels32 (pixels);
		marquee.Apply ();
		return marquee;
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
