using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour {
	public Object CitizenPrefab;

	public Object StoneMinePrefab;
	public Object StorehousePrefab;

	public void Awake() {
		int playerNumber = 1; //used to assign numbers to players

		GameConfig config = FindObjectOfType<GameConfig>(); //finds config data passed from menu

		foreach (PlayerInfo configPlayer in config.Players) { //iterate through each player
			GameObject playerObject = new GameObject(); //create gameobject for each player
			playerObject.AddComponent<Player>().PlayerInfo = configPlayer; //set info for each player
			playerObject.GetComponent<Player>().PlayerInfo.PlayerNumber = playerNumber;
			playerObject.name = "Player " + playerNumber;
			playerObject.GetComponent<Player>().OwnedResources = new Resources { //give player default resources
				Stone = 200
			};

			playerNumber++; //add 1 to player number, for next player
		}

		TerrainConfig terrainConfig = new TerrainConfig(); //configure map

		terrainConfig.MapWidth = 32;
		terrainConfig.MapHeight = 32;
		terrainConfig.NoiseScale = 13;
		terrainConfig.Octaves = 3;
		terrainConfig.Persistance = 0.45f;
		terrainConfig.Lacunarity = 1.6f;
		terrainConfig.Seed = Mathf.RoundToInt(Random.value * 1000000);

		ObjectExtension.FindComponent<TerrainGenerator>().Generate(terrainConfig); //finally, generate map
	}
}