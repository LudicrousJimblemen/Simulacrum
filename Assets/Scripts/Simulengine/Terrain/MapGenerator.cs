using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;
	
	public bool autoUpdate;

	public TerrainType[] regions;
	List<Vector3> WaterSquares;
	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
		WaterSquares =new List<Vector3> ();
		Color[] colorMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						if (regions[i].name == "Water") {
							WaterSquares.Add (-new Vector3 ((x - mapWidth / 2) * 2.5f + 1.25f,0,(y - mapHeight / 2) * 2.5f + 1.25f));
						}
						colorMap [y * mapWidth + x] = regions [i].color;
						break;
					}
				}
			}
		}
		Util.WaterSquares = WaterSquares.ToArray ();
		MapDisplay display = FindObjectOfType<MapDisplay> ();
		display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap), TextureGenerator.TextureFromColorMap (colorMap, mapWidth, mapHeight));
	}
	
	void OnDrawGizmos () {
		foreach (Vector3 water in Util.WaterSquares) {
			Gizmos.DrawCube (water,Vector3.one * 2.5f);
			if (water == Vector3.zero) break;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color color;
}
