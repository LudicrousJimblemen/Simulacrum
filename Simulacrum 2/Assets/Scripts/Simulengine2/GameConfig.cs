using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour {
	public List<PlayerInfo> Players;

	public void Start() {
		Players = new List<PlayerInfo>();

		Players.Add(new PlayerInfo {
			Username = "testbro1",
			IsHuman = true,
			IsCurrent = true
		});

		Players.Add(new PlayerInfo {
			Username = "testbro2"
		});

		DontDestroyOnLoad(gameObject); //carry info held by this gameconfig into actual game
	}
}