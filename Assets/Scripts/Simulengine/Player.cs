using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public PlayerInfo PlayerInfo;
	
	public Resources OwnedResources;
	
	public Object PersonPrefab;
	public Object StorehousePrefab;
	public Object GhostHousePrefab;
	
	private bool summoning;
	private GameObject ghostObject;

	public Player(PlayerInfo playerInfo) {
		PlayerInfo = playerInfo;
	}
	
	void Start() {
		if (PlayerInfo.IsHuman) {
			gameObject.AddComponent<DefaultAI>();
		}
	}
	
	void Update() {
		if (summoning) {
			RaycastHit location;
			if (Physics.Raycast(Util.OrthoRay(Input.mousePosition), out location, Mathf.Infinity, 1 << LayerMask.NameToLayer("Terrain"))
			    && !Util.GetTerrainAtPosition(location.point).Select(x => x.name).Contains("Water")) {
				ghostObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
				ghostObject.transform.position = location.point;
			} else {
				ghostObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
			}
			
			if (Input.GetMouseButtonDown(0) && !Util.GetTerrainAtPosition(location.point, 1f).Select(x => x.name).Contains("Water")) {
				DestroyObject(ghostObject);
				OwnedResources.Stone -= 50;
				
				summoning = false;
				
				GameObject createdStorehouse = Instantiate(StorehousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				createdStorehouse.layer = LayerMask.NameToLayer("Unit"); //TODO NOT HARDCODE
				createdStorehouse.GetComponentInChildren<SkinnedMeshRenderer>().material = GetPlayerMaterial();
				createdStorehouse.transform.parent = transform;
				createdStorehouse.transform.position = location.point;
			}
		}
	}

	public Material GetPlayerMaterial(bool ghost = false) {
		Color playerColor = new Color();
		
		switch (PlayerInfo.PlayerNumber) {
			default:
				playerColor = Color.grey;
				break;
			case 1:
				playerColor = Color.red;
				break;
			case 2:
				playerColor = Color.blue;
				break;
			case 3:
				playerColor = Color.green;
				break;
			case 4:
				playerColor = Color.yellow;
				break;
			case 5:
				playerColor = new Color(1f, 0.5f, 0f); //orange
				break;
			case 6:
				playerColor = Color.magenta;
				break;
		}
		
		Material newMaterial = new Material(Shader.Find("Standard"));
		
		newMaterial.color = playerColor + new Color(0.1f, 0.1f, 0.1f);

		return newMaterial;
	}

	public void SummonBuilding<T>() where T : Building {
		//if (OwnedResources > T.Cost) { TODO IT
		ghostObject = Instantiate(GhostHousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		ghostObject.GetComponent<BasicObject>().IsGhost = true; //TODO MAKE HOUSE NOT HARDCODED
		summoning = true;
		//}
	}
	
	public void SummonUnit<T>(BasicObject parent = null) where T : Unit {
		//if (OwnedResources > T.Cost) {
		Unit createdUnit = Instantiate(PersonPrefab, Vector3.zero, Quaternion.identity) as Unit;
		createdUnit.gameObject.layer = LayerMask.NameToLayer("Unit"); //TODO MAKE PERSON NOT HARDCODED
		createdUnit.Parent = GetComponent<Player>();
		createdUnit.GetComponentInChildren<SkinnedMeshRenderer>().material = GetPlayerMaterial();
		createdUnit.transform.parent = transform;
			
		if (parent != null) {
			createdUnit.transform.position = new Vector3(
				Random.Range(-parent.InteractRange, parent.InteractRange) + parent.transform.position.x,
				0,
				Random.Range(-parent.InteractRange, parent.InteractRange) + parent.transform.position.z
			);
		}
			
		OwnedResources = OwnedResources - createdUnit.Cost;
		//}
	}
}