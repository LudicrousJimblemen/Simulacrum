using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
	public TerrainType[] TerrainTypes = new TerrainType[] {
		new TerrainType {
			name = "Water",
			height = 0.32f,
			color = new Color(
				56f / 255f,
				132f / 255f,
				255f / 255f
			)
		},
		new TerrainType {
			name = "Sand",
			height = 0.4f,
			color = new Color(
				255f / 255f,
				249f / 255f,
				139f / 255f
			)
		},
		new TerrainType {
			name = "Grass",
			height = 1f,
			color = new Color(
				57f / 255f,
				199f / 255f,
				44f / 255f
			)
		}
	};

	public void Generate(TerrainConfig config) {
		float[,] noiseMap = Noise.GenerateNoiseMap(
			config.MapWidth,
			config.MapHeight,
			config.Seed,
			config.NoiseScale,
			config.Octaves,
			config.Persistance,
			config.Lacunarity,
			config.Offset
		);
		Color[] colorMap = new Color[config.MapWidth * config.MapHeight];
		for (int y = 0; y < config.MapHeight; y++) {
			for (int x = 0; x < config.MapWidth; x++) {
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < TerrainTypes.Length; i++) {
					if (currentHeight <= TerrainTypes[i].height) {
						colorMap[y * config.MapWidth + x] = TerrainTypes[i].color;
						//print (y * config.MapWidth + x);
						break;
					}
				}
			}
		}

		int width = noiseMap.GetLength(0);
		int height = noiseMap.GetLength(1);
		float topLeftX = (width - 1) / -2f;
		float topLeftZ = (height - 1) / 2f;

		MeshData meshData = new MeshData(width, height);
		int vertexIndex = 0;

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				meshData.Vertices[vertexIndex] = new Vector3((topLeftX + x) / 4, 0, (topLeftZ - y) / 4);
				meshData.Uvs[vertexIndex] = new Vector2((x / (float) width), (y / (float) height));

				if (x < width - 1 && y < height - 1) {
					meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}

				vertexIndex++;
			}
		}

		Texture2D texture = new Texture2D(width, height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colorMap);
		texture.Apply();

		GetComponent<MeshFilter>().sharedMesh = meshData.CreateMesh();
		GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;

		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
	}

	void Start() {
		//
	}

	void Update() {
		//
	}
}
