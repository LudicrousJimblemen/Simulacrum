using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {
	public List<Player> Players;
	public int Size = 8;
	
	public Game() {
		Players = new List<Player>();
	}
	
	public void Initialize() {
		//Application.LoadLevel("Game");
		Mesh Ground = new Mesh();
		
		Ground.name = "GroundMesh";
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		
		for (int i = 0; i < Size + 1; i++) {
			for (int j = 0; j < Size + 1; j++) {
				if ((i % 2 == 0 && j % 2 == 0) ||
				    (i % 2 != 0 && j % 2 != 0)) {
					vertices.Add(new Vector3(i, 0, j));
				}
			}
		}
		
		foreach (var vertex in vertices) {
			Debug.Log(vertex.x + " " + vertex.z);
		}
		
		Ground.vertices = vertices.ToArray();
		
		/*Ground.uv = new Vector2[] {
			Vector2(0, 0),
			Vector2(0, 1),
			Vector2(1, 1),
			Vector2(1, 0)
		};*/
		
		//Ground.triangles = new int[] {0, 1, 2, 0, 2, 3};
		
		//Ground.RecalculateNormals();

	}
}