using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour {
	public Object StoneMinePrefab;
	public Object PersonPrefab;
	public Object StorehousePrefab;
	public Object GhostHousePrefab;

	public void Awake() {
		int playerNumber = 1;
		
		GameConfig config = FindObjectOfType<GameConfig>();
		
		foreach (PlayerInfo configPlayer in config.players) {
			GameObject playerObject = new GameObject();
			playerObject.AddComponent<Player>().PlayerInfo = configPlayer;
			playerObject.GetComponent<Player>().PlayerInfo.PlayerNumber = playerNumber;
			playerObject.name = "Player " + playerNumber;
			playerObject.GetComponent<Player>().OwnedResources = new Resources {
				Stone = 200
			};
			
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
		
		map.GenerateMap();

		for (int i = 0; i < map.mapWidth * 4; i++) {
			GameObject newRock = Instantiate(StoneMinePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Vector3 spawnPosition = new Vector3(
				Random.Range(-map.mapWidth, map.mapWidth),
				0,
				Random.Range(-map.mapWidth, map.mapWidth)
			) * 1.15f;
			
			while (Util.GetTerrainAtPosition(spawnPosition).Select(x => x.name).Contains("Water")) {
				spawnPosition = new Vector3(
					Random.Range(-map.mapWidth, map.mapWidth),
					0,
					Random.Range(-map.mapWidth, map.mapWidth)
				) * 1.15f;
			}
			newRock.transform.Translate(spawnPosition);
			newRock.transform.Rotate(
				0,
				Random.Range(0f, 360f),
				0
			);
			Resource rockResource = newRock.GetComponent<Resource>();
			rockResource.MaxStock = Random.Range(20, 35);
			rockResource.Stock = rockResource.MaxStock;
			newRock.transform.parent = GameObject.Find("Resources").transform;
		}
	}
}