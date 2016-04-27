using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {
	public void Start() {
		GameConfig config = FindObjectOfType<GameConfig>();
		
		foreach (PlayerInfo configPlayer in config.players) {
			GameObject playerObject = new GameObject();
			playerObject.AddComponent<Player>().PlayerInfo = configPlayer;
			playerObject.name = "Player";
		}
		
		MapGenerator map = FindObjectOfType<MapGenerator>();
		
		map.mapWidth = 128;
		map.mapHeight = 128;
		map.noiseScale = 20;
		map.octaves = 3;
		map.persistance = 0.35f;
		map.lacunarity = 1;
		map.seed = Mathf.RoundToInt(Random.value * 1000000);
		map.regions = new [] {
			new TerrainType {
				name = "Water",
				height = 0.32f,
				color = new Color(0, 181, 155)
			},
			new TerrainType {
				name = "Sand",
				height = 0.4f,
				color = new Color(255, 255, 122)
			},
			new TerrainType {
				name = "Grass",
				height = 1f,
				color = new Color(42, 236, 92)
			}
		};
		
		map.GenerateMap();
	}
}