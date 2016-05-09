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

	public static Vector3[] WaterSquares;
	public static bool IsOnWater (Vector3 Position, float Padding) {
		float WaterX;
		float WaterY;
        foreach (Vector3 v in WaterSquares) {
			WaterX = v.x;
			WaterY = v.z;
			if (Position.x < WaterX + (1.25f + Padding) && 
				Position.x > WaterX - (1.25f + Padding) &&
				Position.z < WaterY + (1.25f + Padding) && 
				Position.z > WaterY - (1.25f + Padding)) {
				return true;
			}
		}
		return false;
	}

	//:ng:
	//do not use
	//necrosis ridden skeleton of a dead function
	public static GameObject WaterObstacle () {
		GameObject obstacle = new GameObject ();
		obstacle.AddComponent<NavMeshObstacle> ();
		obstacle.GetComponent<NavMeshObstacle> ().shape = NavMeshObstacleShape.Box;
		obstacle.GetComponent<NavMeshObstacle> ().center = Vector3.zero;
		obstacle.GetComponent<NavMeshObstacle> ().size = new Vector3 (2.5f, .1f, 2.5f);
		return obstacle;
	}
}
