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

	public static GameObject WaterObstacle () {
		GameObject obstacle = new GameObject ();
		obstacle.AddComponent<NavMeshObstacle> ();
		obstacle.GetComponent<NavMeshObstacle> ().shape = NavMeshObstacleShape.Box;
		obstacle.GetComponent<NavMeshObstacle> ().center = Vector3.zero;
		obstacle.GetComponent<NavMeshObstacle> ().size = new Vector3 (2.5f, .1f, 2.5f);
		return obstacle;
	}
}
