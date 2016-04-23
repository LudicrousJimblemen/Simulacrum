using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {
	public void Start() {
		GameConfig config = FindObjectOfType<GameConfig>();
		
		foreach (Player configPlayer in config.players) {
			GameObject playerObject = new GameObject();
			Player player = playerObject.AddComponent<Player>();
			player.IsHuman = configPlayer.IsHuman;
			player.Username = configPlayer.Username;
		}
	}
}