using UnityEngine;
using System;
using System.Collections.Generic;

public class GameConfig : MonoBehaviour {
	public List<PlayerInfo> players;

	public void Start() {
		players = new List<PlayerInfo>();
		
		PlayerInfo testPlayer = new PlayerInfo {
			Username = "testbro1",
			IsHuman = true
		};
		
		players.Add(testPlayer);
		
		DontDestroyOnLoad(gameObject);
	}
}