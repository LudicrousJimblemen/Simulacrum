using UnityEngine;
using System;
using System.Collections.Generic;

public class GameConfig : MonoBehaviour {
	public List<PlayerInfo> players;

	public void Start() {
		players = new List<PlayerInfo>();
		
		players.Add(new PlayerInfo {
			Username = "testbro1",
			IsHuman = true,
			IsCurrent = true
		});
		
		players.Add(new PlayerInfo {
			Username = "testbro2"
		});
		
		players.Add(new PlayerInfo {
			Username = "testbro3"
		});
		
		players.Add(new PlayerInfo {
			Username = "testbro4"
		});
		
		players.Add(new PlayerInfo {
			Username = "testbro5"
		});
		
		DontDestroyOnLoad(gameObject);
	}
}