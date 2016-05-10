using UnityEngine;
using System.Collections;

public static class MeshGenerator {
	public static MeshData GenerateTerrainMesh(float[,] heightMap) {
		int width = heightMap.GetLength (0);
		Debug.Log (width);
		int height = heightMap.GetLength (1);
		float topLeftX = (width - 1) / -2f;
		float topLeftZ = (height - 1) / 2f;

		MeshData meshData = new MeshData (width, height);
		int vertexIndex = 0;

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				meshData.vertices [vertexIndex] = new Vector3 ((topLeftX + x)/4, 0, (topLeftZ - y)/4);
				meshData.uvs [vertexIndex] = new Vector2 ((x / (float)width), (y / (float)height));

				if (x < width - 1 && y < height - 1) {
					meshData.AddTriangle (vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					meshData.AddTriangle (vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}

				vertexIndex++;
			}
		}

		return meshData;

	}
}