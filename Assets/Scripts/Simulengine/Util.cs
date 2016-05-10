using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Util {
	public static Ray OrthoRay(Vector3 MousePosition) {
		Ray ray = new Ray(Camera.main.ScreenToWorldPoint(MousePosition), Camera.main.gameObject.transform.forward);
		return ray;
	}

	public static bool EvaluateResource(BehaviorType CitizenBehavior, ResourceType Resource) {
		return (CitizenBehavior == BehaviorType.StoneMiner && Resource == ResourceType.Stone) /* || (b == btype && r = rtype) || ...*/;
	}
	
	public static Player GetCurrentPlayer() {
		return GameObject.FindObjectsOfType<Player>()
			.First(x => x.PlayerInfo.IsCurrent);
	}
	
	public static List<TerrainType> GetTerrainAtPosition(Vector3 position, float radius = 0) {
		List<TerrainType> terrains = new List<TerrainType>();
		
		for (int i = 0; i < 4; i++) {
			RaycastHit hit;
			
			Ray ray;
			
			ray = new Ray(position, Vector3.down);
			
			switch (i) {
				case 0:
					ray = new Ray(position + new Vector3(0 + radius, 1, 0 + radius), Vector3.down);
					break;
				case 1:
					ray = new Ray(position + new Vector3(0 - radius, 1, 0 - radius), Vector3.down);
					break;
				case 2:
					ray = new Ray(position + new Vector3(0 - radius, 1, 0 + radius), Vector3.down);
					break;
				case 3:
					ray = new Ray(position + new Vector3(0 + radius, 1, 0 - radius), Vector3.down);
					break;
			}
			
			Physics.Raycast(
				ray,
				out hit, Mathf.Infinity,
				1 << LayerMask.NameToLayer("Terrain")
			);
			
			Renderer rend = hit.transform.GetComponent<Renderer>();
			
			Texture2D tex = rend.material.mainTexture as Texture2D;
			Vector2 pixelUV = hit.textureCoord;
			pixelUV.x *= tex.width;
			pixelUV.y *= tex.height;
			
			terrains.Add(
				GameObject.FindObjectOfType<MapGenerator>().regions.First(
					x => x.color == tex.GetPixel(
						(int)pixelUV.x,
						(int)pixelUV.y
					)
				)
			);
		}
		return terrains.Distinct().ToList();
	}
}
