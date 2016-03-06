using UnityEngine;
using System.Collections;

public class Generic : MonoBehaviour {
	public bool Selected;
	public int[] Resources = {0, 0, 0};
	public Material defaultMaterial;
	public Material outlineMaterial;

	void Start() {
		//'Sup?
    }

	void Update() {
		if (Selected) {
			GetComponentInChildren<SkinnedMeshRenderer>().material = outlineMaterial;
		} else {
			GetComponentInChildren<SkinnedMeshRenderer>().material = defaultMaterial;
		}
	}
}
