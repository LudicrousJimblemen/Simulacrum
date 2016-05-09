﻿using UnityEngine;

public class Game : MonoBehaviour {
	public Object stoneMinePrefab;

	public void Awake() {
		int playerNumber = 1;
		
		GameConfig config = FindObjectOfType<GameConfig>();
		
		foreach (PlayerInfo configPlayer in config.players) {
			GameObject playerObject = new GameObject();
			playerObject.AddComponent<Player>().PlayerInfo = configPlayer;
			playerObject.GetComponent<Player>().PlayerInfo.PlayerNumber = playerNumber;
			playerObject.name = "Player " + playerNumber;
			playerObject.GetComponent<Player>().Stone = 200;
			
			playerNumber++;
		}
		
		MapGenerator map = FindObjectOfType<MapGenerator>();
		
		map.mapWidth = 32;
		map.mapHeight = 32;
		map.noiseScale = 13;
		map.octaves = 3;
		map.persistance = 0.45f;
		map.lacunarity = 1.6f;
		map.seed = Mathf.RoundToInt(Random.value * 1000000);
		map.regions = new [] {
			new TerrainType {
				name = "Water",
				height = 0.32f,
				color = new Color(
					56f / 256f,
				    132f / 256f,
				    255f / 256f
				)
			},
			new TerrainType {
				name = "Sand",
				height = 0.4f,
				color = new Color(
					255f / 256f,
					249f / 256f,
					139f / 256f
				)
			},
			new TerrainType {
				name = "Grass",
				height = 1f,
				color = new Color(
					57f / 256f,
					199f / 256f,
					44f / 256f
				)
			}
		};
		
		map.GenerateMap();

		for (int i = 0; i < map.mapWidth; i++) {
			GameObject newRock = Instantiate(stoneMinePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			newRock.transform.Translate(
				Random.Range(-map.mapWidth, map.mapWidth),
				0,
				Random.Range(-map.mapWidth, map.mapWidth)
			);
			newRock.transform.Rotate(
				0,
				Random.Range(0, 360),
				0
			);
			newRock.transform.parent = GameObject.Find ("Resources").transform;
		}
	}
}