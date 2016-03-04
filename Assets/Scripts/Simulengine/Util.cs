using UnityEngine;
using System.Collections;

public static class Util {
	public static Ray OrthoRay (Vector3 MousePosition) {
		Ray ray = new Ray (Camera.main.ScreenToWorldPoint (MousePosition), Camera.main.transform.forward);
		return ray;
	}

}
