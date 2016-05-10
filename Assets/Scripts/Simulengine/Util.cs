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
	
	public static Color GetTerrainAtPosition(Vector3 position) {
		int width = GameObject.FindObjectOfType<MapGenerator>().mapWidth;
		int height = GameObject.FindObjectOfType<MapGenerator>().mapHeight;
		
		return (GameObject.FindObjectOfType<MapDisplay>()
			.meshRenderer
			.material
			.mainTexture as Texture2D).GetPixelBilinear(
				(position.x + (width/2))/width,
				(position.z + (height/2))/height
			);
	}

	public static GameObject WaterObstacle () {
		GameObject obstacle = new GameObject ();
		obstacle.AddComponent<NavMeshObstacle> ();
		obstacle.GetComponent<NavMeshObstacle> ().shape = NavMeshObstacleShape.Box;
		obstacle.GetComponent<NavMeshObstacle> ().center = Vector3.zero;
		obstacle.GetComponent<NavMeshObstacle> ().size = new Vector3 (2.5f, .1f, 2.5f);
		return obstacle;
	}
}
