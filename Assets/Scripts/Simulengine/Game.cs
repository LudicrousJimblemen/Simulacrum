using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public Object StoneMinePrefab;
	public Object PersonPrefab;
	public Object StorehousePrefab;

	public void Awake() {
		int playerNumber = 1;
		
		GameConfig config = FindObjectOfType<GameConfig>();
		
		foreach (PlayerInfo configPlayer in config.players) {
			GameObject playerObject = new GameObject();
			playerObject.AddComponent<Player>().PlayerInfo = configPlayer;
			playerObject.GetComponent<Player>().PlayerInfo.PlayerNumber = playerNumber;
			playerObject.name = "Player " + playerNumber;
			playerObject.GetComponent<Player>().Stone = 200;
			
			playerObject.GetComponent<Player>().PersonPrefab = PersonPrefab;
			playerObject.GetComponent<Player>().StorehousePrefab = StorehousePrefab;
			
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

		for (int i = 0; i < map.mapWidth * 20; i++) {
			GameObject newRock = Instantiate(StoneMinePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Vector3 SpawnPosition = new Vector3 (
				Random.Range(-map.mapWidth, map.mapWidth),
				0,
				Random.Range(-map.mapWidth, map.mapWidth)
			) * 1.15f;
			newRock.transform.Translate (SpawnPosition);
			newRock.transform.Rotate(
				0,
				Random.Range(0f, 360f),
				0
			);
			Resource rockResource = newRock.GetComponent<Resource> ();
			rockResource.MaxStock = Random.Range (20,35);
			rockResource.Stock = rockResource.MaxStock;
            newRock.transform.parent = GameObject.Find ("Resources").transform;
		}
	}
}