using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DefaultAI : MonoBehaviour {
	public Player ControlledPlayer;
	
	void Awake() {
		ControlledPlayer = GetComponent<Player>();
	}
	
	void Start() {
		GameObject startingStorehouse  = ControlledPlayer.SummonBuilding<Storehouse>();
		
		switch (ControlledPlayer.PlayerInfo.PlayerNumber) {
				case 2:
					startingStorehouse.transform.position = new Vector3(-35, 0, -35);
					break;
				case 3:
					startingStorehouse.transform.position = new Vector3(35, 0, 35);
					break;
				case 4:
					startingStorehouse.transform.position = new Vector3(-35, 0, 35);
					break;
				case 5:
					startingStorehouse.transform.position = new Vector3(35, 0, -35);
					break;

		}
	}
	
	void Update() {
		if (Random.value * 60 * 10 >= 599) { // 1/600 chance, 60FPS, i.e. once per ~10 seconds
			GameObject createdPerson = ControlledPlayer.SummonUnit<Citizen>();
			createdPerson.layer = LayerMask.NameToLayer("Unit");
			createdPerson.GetComponent<Citizen>().Behavior = (BehaviorType) 1;
	
			createdPerson.GetComponentInChildren<SkinnedMeshRenderer>().material = ControlledPlayer.GetPlayerMaterial();
			createdPerson.transform.parent = transform;
		
			switch (ControlledPlayer.PlayerInfo.PlayerNumber) {
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