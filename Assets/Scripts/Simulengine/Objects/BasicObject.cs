using UnityEngine;
using System.Collections;

public class BasicObject : MonoBehaviour
{
	public bool Selectable;
	public bool Selected;
	public Material defaultMaterial;
	public Material outlineMaterial;

	void Update() {
		if (Selected) {
			GetComponentInChildren<SkinnedMeshRenderer>().material = outlineMaterial;
		} else {
			GetComponentInChildren<SkinnedMeshRenderer>().material = defaultMaterial;
		}
	}
}
