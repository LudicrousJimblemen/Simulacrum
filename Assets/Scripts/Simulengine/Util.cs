using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Util {
	public static Ray OrthoRay (Vector3 MousePosition) {
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (MousePosition), Camera.main.transform.forward);
		return ray;
	}
	
	public static bool EvaluateResource (BehaviourType CitizenBehaviour, ResourceType Resource) {
		return (CitizenBehaviour == BehaviourType.StoneMiner && Resource == ResourceType.Stone) /* || (b == btype && r = rtype) || ...*/;
	}
}
