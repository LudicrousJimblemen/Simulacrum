using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0, 1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;
	
	public bool autoUpdate;

	public TerrainType[] regions = {
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
	
	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
		Color[] colorMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions[i].height) {
						colorMap[y * mapWidth + x] = regions[i].color;
						//print (y * mapWidth + x);
						break;
					}
				}
			}
		}
		MapDisplay display = FindObjectOfType<MapDisplay>();
		display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color color;
}
