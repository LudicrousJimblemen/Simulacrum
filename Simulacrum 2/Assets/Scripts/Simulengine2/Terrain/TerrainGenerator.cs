using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
	public Texture2D Texture;

	public TerrainConfig Config;

	public TerrainType[] TerrainTypes = new TerrainType[] {
		new TerrainType { //TODO terraintypes > regions
			Name = "Water",
			Height = 0.32f,
			Color = new Color(
				56f / 255f,
				132f / 255f,
				255f / 255f
			)
		},
		new TerrainType {
			Name = "Sand",
			Height = 0.4f,
			Color = new Color(
				255f / 255f,
				249f / 255f,
				139f / 255f
			)
		},
		new TerrainType {
			Name = "Grass",
			Height = 1f,
			Color = new Color(
				57f / 255f,
				199f / 255f,
				44f / 255f
			)
		}
	};

	public void Generate(TerrainConfig config) {
		config.MapHeight += 1;
		config.MapWidth += 1;

		Config = config;

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
					if (currentHeight <= TerrainTypes[i].Height) {
						colorMap[y * config.MapWidth + x] = TerrainTypes[i].Color;
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

		Texture = new Texture2D(width, height);
		Texture.filterMode = FilterMode.Point;
		Texture.wrapMode = TextureWrapMode.Clamp;
		Texture.SetPixels(colorMap);
		Texture.Apply();

		Mesh finalMesh = meshData.CreateMesh();

		GetComponent<MeshFilter>().sharedMesh = finalMesh;
		GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Texture;

		GetComponent<MeshCollider>().sharedMesh = finalMesh;
	}

	public TerrainType GetTerrainAtPosition(Vector3 input) {
		Debug.Log(input.x + " " + input.z);
		return TerrainTypes.FirstOrDefault(t =>
			Texture.GetPixel(
				(int) Math.Ceiling(input.x + (Config.MapWidth / 2)) - 1,
				(int) (-Math.Ceiling(input.z - (Config.MapHeight / 2)))
			) == t.Color
		);
	}

	public TerrainType[] GetTerrainNearPosition(Vector3 input, float bufferRadius) {
		Debug.Log(input.x + " " + input.z);
		List<TerrainType> returned = new List<TerrainType>();
		returned.Add(TerrainTypes.FirstOrDefault(t =>
			Texture.GetPixel(
				(int) Math.Ceiling(input.x + bufferRadius + (Config.MapWidth / 2)) - 1,
				(int) (-Math.Ceiling(input.z + bufferRadius - (Config.MapHeight / 2)))
			) == t.Color
		));
		returned.Add(TerrainTypes.FirstOrDefault(t =>
			Texture.GetPixel(
				(int) Math.Ceiling(input.x + bufferRadius + (Config.MapWidth / 2)) - 1,
				(int) (-Math.Ceiling(input.z - bufferRadius - (Config.MapHeight / 2)))
			) == t.Color
		));
		returned.Add(TerrainTypes.FirstOrDefault(t =>
			Texture.GetPixel(
				(int) Math.Ceiling(input.x - bufferRadius + (Config.MapWidth / 2)) - 1,
				(int) (-Math.Ceiling(input.z + bufferRadius - (Config.MapHeight / 2)))
			) == t.Color
		));
		returned.Add(TerrainTypes.FirstOrDefault(t =>
			Texture.GetPixel(
				(int) Math.Ceiling(input.x - bufferRadius + (Config.MapWidth / 2)) - 1,
				(int) (-Math.Ceiling(input.z - bufferRadius - (Config.MapHeight / 2)))
			) == t.Color
		));
		return returned.ToArray();
	}

	void Start() {
		//
	}

	void Update() {
		//
	}
}
