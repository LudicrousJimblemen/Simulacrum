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
		
		map.mapWidth = 64;
		map.mapHeight = 64;
		map.noiseScale = 20;
		map.octaves = 3;
		map.persistance = 0.35f;
		map.lacunarity = 1;
		map.seed = Mathf.RoundToInt(Random.value * 1000000);
		map.regions = new [] {
			new TerrainType {
				name = "Water",
				height = 0.32f,
				color = new Color(56f, 132f, 255f)
			},
			new TerrainType {
				name = "Sand",
				height = 0.4f,
				color = new Color(255f, 249f, 139f)
			},
			new TerrainType {
				name = "Grass",
				height = 1f,
				color = new Color(57f, 199f, 44f)
			}
		};
		
		map.GenerateMap();
	}
}