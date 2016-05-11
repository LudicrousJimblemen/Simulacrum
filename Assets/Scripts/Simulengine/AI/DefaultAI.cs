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
			case 0:
				ControlledPlayer.SummonBuilding<Storehouse>();
				break;
		}
	}
	
	void Update() {
		//
	}
}