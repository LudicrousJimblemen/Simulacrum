using System;
using System.Linq;
using UnityEngine;

public class ObjectExtension : UnityEngine.Object {
	public static T FindComponent<T>() where T : UnityEngine.Object {
		return FindObjectsOfType<GameObject>().First(x => x.GetComponent<T>() != null).GetComponent<T>();
	}
}