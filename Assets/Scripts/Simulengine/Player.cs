using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public PlayerInfo PlayerInfo;
	
	public int Stone;

	public Player (PlayerInfo playerInfo) {
		PlayerInfo = playerInfo;
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
		
	//=========DEMO AI=========\\
	
	public Object PersonPrefab;
	public Object StorehousePrefab;
	
	void Start() {
		if (PlayerInfo.IsHuman) {
			return;
		}
		
		GameObject createdStorehouse = Instantiate(StorehousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		createdStorehouse.layer = LayerMask.NameToLayer("Unit");

		createdStorehouse.GetComponentInChildren<SkinnedMeshRenderer>().material = GetPlayerMaterial();

		createdStorehouse.transform.parent = transform;
		
		switch (PlayerInfo.PlayerNumber) {
			case 2:
				createdStorehouse.transform.position = new Vector3(-35 + 1.25f, 0, -35 + 1.25f);
			break;
			case 3:
				createdStorehouse.transform.position = new Vector3(35 - 1.25f, 0,35 - 1.25f);
			break;
			case 4:
				createdStorehouse.transform.position = new Vector3(-35 + 1.25f, 0,35 - 1.25f);
			break;
			case 5:
				createdStorehouse.transform.position = new Vector3(35 - 1.25f, 0, -35 + 1.25f);
			break;
		}
	}
	
	void Update() {
		if (PlayerInfo.IsHuman) {
			return;
		}
		
		if (Random.value * 60 * 10 >= 599) { // 1/600 chance, 60FPS, i.e. once per ~10 seconds
			GameObject createdPerson = Instantiate(PersonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			createdPerson.layer = LayerMask.NameToLayer("Unit");
			createdPerson.GetComponent<Citizen>().Behavior = (BehaviorType) 1;
	
			createdPerson.GetComponentInChildren<SkinnedMeshRenderer>().material = GetPlayerMaterial();
			createdPerson.transform.parent = transform;
		
			switch (PlayerInfo.PlayerNumber) {
				case 2:
					createdPerson.transform.position = new Vector3(-35, 0, -35);
				break;
				case 3:
					createdPerson.transform.position = new Vector3(35, 0, 35);
				break;
				case 4:
					createdPerson.transform.position = new Vector3(-35, 0, 35);
				break;
				case 5:
					createdPerson.transform.position = new Vector3(35, 0, -35);
				break;
			}
		}
	}
}