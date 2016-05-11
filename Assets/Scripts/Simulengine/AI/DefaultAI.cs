using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DefaultAI : MonoBehaviour {
	public Player ControlledPlayer;
	
	void Start() {
		ControlledPlayer = GetComponent<Player>();
	}
	
	void Update() {
		//
	}
}