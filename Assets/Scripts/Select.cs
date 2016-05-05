using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Select : MonoBehaviour {
	public Vector3 position;
	public Vector3 selection;
	GameObject PersonParent;
	public GameObject[] persons;

	public Vector3 Marquee1;
	public Vector3 Marquee2;
	public bool inMarquee;
	Rect MarqueeRect;

	GameObject SelectedResource;

	void Start	() {
<<<<<<< HEAD
		Debug.Log(FindObjectsOfType<GameObject>().Any());
		PersonParent = FindObjectsOfType<GameObject>()
			.Where(x => x.GetComponent<Player>() != null)
			.First(x => x.GetComponent<Player>().PlayerInfo.IsCurrent);
=======
		PersonParent = FindObjectsOfType<Player>().Where(
			x => x.GetComponent<Player>().PlayerInfo.IsCurrent
		).ToList().First().gameObject;
>>>>>>> 6da7a56c030a60e34dfad477609c53b8cbf43e05
		persons = new GameObject[500];
		MarqueeRect = new Rect();
	}

	void Update() {
		for (int i = 0; i < PersonParent.transform.GetChild(0).childCount; i++) {
			persons[i] = PersonParent.transform.GetChild(0).GetChild(i).gameObject;
		}
		RaycastHit UnitHit;
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(OrthoRay(), out UnitHit, Mathf.Infinity, 1 << 9)) {
				UnitHit.collider.gameObject.GetComponent<BasicObject>().Selected = true;
				foreach (GameObject p in persons.Where(x => x != null && x != UnitHit.collider.gameObject)) {
					if (p.GetComponent<BasicObject>().Selected && !Input.GetKey(KeyCode.LeftShift))
						p.GetComponent<BasicObject>().Selected = false;
				}
			} else {
				foreach (GameObject p in persons.Where(x => x != null)) {
					if (p.GetComponent<BasicObject>().Selected && !Input.GetKey(KeyCode.LeftShift))
						p.GetComponent<BasicObject>().Selected = false;
				}
				inMarquee = true;
				Marquee1 = Input.mousePosition;
			}
		}
		if (inMarquee) {
			MarqueeSelection();
			foreach (GameObject unit in persons.Where(x => x != null)) {
				//If the screen position of the unit is within the marquee, select it.
				//If the screen position of the uniti is not within the marquee, but the user is holding shift and it is already selected, stay selected
				//Otherwise, unselect it
				Vector3 unitScreenPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
				Vector3 unitScreenPoint = new Vector3(unitScreenPosition.x, Screen.height - unitScreenPosition.y);
				unit.GetComponent<BasicObject>().Selected = MarqueeRect.Contains(unitScreenPoint)
					|| (Input.GetKey(KeyCode.LeftShift) && unit.GetComponent<BasicObject>().Selected);
			}
		}
		if (Input.GetMouseButtonUp(0) && inMarquee) {
			inMarquee = false;
		}
		if (Input.GetMouseButton(1)) {
			RaycastHit TerrainHit;
			RaycastHit ResourceHit;
			if (Physics.Raycast(OrthoRay(), out ResourceHit, Mathf.Infinity, 1 << 10)) {
				SelectedResource = ResourceHit.collider.gameObject;
				SelectedResource.GetComponent<Resource>().Selected = true;
				movePersons();
			} else if (Physics.Raycast(OrthoRay(), out TerrainHit, Mathf.Infinity, 1 << 8)) {
				selection = TerrainHit.point;
				if (SelectedResource != null) {
					SelectedResource.GetComponent<Resource>().Selected = false;
					SelectedResource = null;
				}
				movePersons();
			} else {
				if (SelectedResource != null) {
					SelectedResource.GetComponent<Resource>().Selected = false;
					SelectedResource = null;
				}
			}
		}
		ChangeUnitBehaviour();
	}
	void OnDrawGizmosSelected() {
		Gizmos.DrawSphere(selection, 1);
		Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Marquee1), .1f);
		Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Marquee2), .1f);
	}
	Ray OrthoRay() {
		Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.gameObject.transform.forward);
		return ray;
	}

	void MarqueeSelection() {
		Marquee2 = Input.mousePosition;
	}

	void OnGUI() {
		if (inMarquee) {
			float[] maxmin = {Mathf.Max(Marquee1.x, Marquee2.x), //Max x
							Mathf.Min(Marquee1.x, Marquee2.x), //Min x
							Mathf.Max(Screen.height - Marquee1.y, Screen.height - Marquee2.y), //Max y
							Mathf.Min(Screen.height - Marquee1.y, Screen.height - Marquee2.y), //Min y
			};
			MarqueeRect.xMax = maxmin[0];
			MarqueeRect.xMin = maxmin[1];
			MarqueeRect.yMax = maxmin[2];
			MarqueeRect.yMin = maxmin[3];
			GUI.DrawTexture(MarqueeRect, MarqueeTexture());
		}
	}

	Texture2D MarqueeTexture() {
		Texture2D marquee = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		Color32[] pixels = { new Color32(0, 0, 255, 50) };
		marquee.SetPixels32(pixels);
		marquee.Apply();
		return marquee;
	}

	void movePersons() {
		List<NavMeshAgent> FighterAgents = new List<NavMeshAgent>();
		List<NavMeshAgent> StonerAgents = new List<NavMeshAgent>(); //420 blaze it
		foreach (GameObject person in persons.Where(x => x != null)) {
			if (person.GetComponent<BasicObject>().Selected) {
				switch (person.GetComponent<Citizen>().Behaviour) {
					case BehaviourType.Fighter:
					FighterAgents.Add(person.GetComponent<NavMeshAgent>());
					break;
					case BehaviourType.StoneMiner:
					StonerAgents.Add(person.GetComponent<NavMeshAgent>());
					break;
				}
			}
		}
		if (FighterAgents.Count > 0) {
			Vector3[] Destinations = UnitOrganization.FighterOrganization.OrganizeFighters(FighterAgents.ToArray(), selection);
			for (int i = 0; i < FighterAgents.Count; i++) {
				FighterAgents[i].destination = Destinations[i];
			}
		}
		if (StonerAgents.Count > 0) {
			if (SelectedResource == null) {
				Vector3[] Destinations = UnitOrganization.FighterOrganization.OrganizeWorkers(StonerAgents.ToArray(), selection);
				for (int i = 0; i < StonerAgents.Count; i++) {
					StonerAgents[i].GetComponent<Citizen> ().CurrentAction = CitizenState.Idle;
					StonerAgents[i].destination = Destinations[i];
				}
			} else {
				for (int i = 0; i < StonerAgents.Count; i++) {
					StonerAgents[i].GetComponent<Citizen>().SelectResource(SelectedResource);
				}
			}
		}
	}

	void ChangeUnitBehaviour() {
		BehaviourType newBehaviour = BehaviourType.Idle;
		bool didChangeBehaviour = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2);
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			newBehaviour = BehaviourType.Fighter;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			newBehaviour = BehaviourType.StoneMiner;
		}
		if (didChangeBehaviour) {
			foreach (GameObject unit in persons.Where(x => x.GetComponent<Citizen>().Selected)) {
				unit.GetComponent<Citizen>().Behaviour = newBehaviour;
			}
		}
	}
}
