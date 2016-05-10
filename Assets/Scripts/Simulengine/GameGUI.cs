using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameGUI : MonoBehaviour {
	//TODO: MOVE ALL OF THIS STUFF INTO THE PLAYER CLASS
	
	public Object PersonPrefab;
	public Object StorehousePrefab;
	public Object GhostHousePrefab;
	
	private GameObject ghostObject;
	
	private bool summoning;
	
	void Update() {
		GetComponentsInChildren<Text>().First(x => x.tag == "UIStoneText").text = Util.GetCurrentPlayer().Stone.ToString();
		
		if (summoning) {
			RaycastHit location;
			if (Physics.Raycast(Util.OrthoRay(Input.mousePosition), out location, Mathf.Infinity, 1 <<  LayerMask.NameToLayer("Terrain"))) {
				ghostObject.transform.position = location.point;
				print (Util.GetTerrainAtPosition (location.point));
			}
			
			if (Input.GetMouseButtonDown(0) && !Util.IsOnWater (location.point, 2.5f)) {
				DestroyObject(ghostObject);
				Util.GetCurrentPlayer().Stone -= 50;
				
				summoning = false;
				
				GameObject createdStorehouse = Instantiate(StorehousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				createdStorehouse.layer = LayerMask.NameToLayer("Unit");
		
				createdStorehouse.GetComponentInChildren<SkinnedMeshRenderer>().material = Util.GetCurrentPlayer().GetPlayerMaterial();
		
				createdStorehouse.transform.parent = Util.GetCurrentPlayer().transform;
				
				createdStorehouse.transform.position = location.point;
			}
		}
	}

	public void SummonPerson(int behavior) {
		GameObject createdPerson = Instantiate(PersonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		createdPerson.layer = LayerMask.NameToLayer("Unit");
		createdPerson.GetComponent<Citizen>().Behavior = (BehaviorType) behavior;
		Player CurrentPlayer = Util.GetCurrentPlayer ();
		createdPerson.GetComponentInChildren<SkinnedMeshRenderer>().material = CurrentPlayer.GetPlayerMaterial();

		Storehouse[] PlayerStorehouses = CurrentPlayer.transform.GetComponentsInChildren<Storehouse> ();
		Transform ParentStorehouse = PlayerStorehouses[Random.Range (0,PlayerStorehouses.Count ())].transform;
		createdPerson.transform.Translate (
			ParentStorehouse.position + new Vector3 (Random.Range (-2.5f, 2.5f), 0, Random.Range (-2.5f,2.5f))
		);

		createdPerson.transform.parent = Util.GetCurrentPlayer().transform;
	}

	public void SummonStorehouse() {
		if (Util.GetCurrentPlayer().Stone >= 50) {
			summoning = true;
			
			ghostObject = Instantiate(GhostHousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			ghostObject.GetComponent<BasicObject>().IsGhost = true;
		}
	}
}