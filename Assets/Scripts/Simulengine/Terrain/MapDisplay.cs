using UnityEngine;
using System.Linq;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	
	public Texture2D currentTexture;

	public void DrawMesh(MeshData meshData, Texture2D texture) {
		currentTexture = texture;
		//print (currentTexture.width + " " + currentTexture.height);
		
		meshFilter.sharedMesh = meshData.CreateMesh ();
		meshRenderer.sharedMaterial.mainTexture = texture;
		
		GetComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
	}
}