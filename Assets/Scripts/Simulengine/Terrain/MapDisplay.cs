using UnityEngine;
using System.Linq;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	
	public Texture2D currentTexture;

	public void DrawMesh(MeshData meshData, Texture2D texture) {
		currentTexture = texture;
		
		meshFilter.sharedMesh = meshData.CreateMesh ();
		meshRenderer.sharedMaterial.mainTexture = texture;
	}
	
	void OnDrawGizmosSelected() {
		Debug.Log("it me");
		for (int i = -40; i < 40; i++) {
			for (int j = -40; j < 40; j++) {
				Gizmos.color = Util.GetTerrainAtPosition(new Vector3(i,0,j));
				Gizmos.DrawCube(new Vector3(i,0,j), new Vector3(1,1,1));
			}
		}
	}
}