using UnityEngine;

public class BasicObject : MonoBehaviour {
	public bool Selected;

	public Material defaultMaterial;
	public Material outlineMaterial;
	SkinnedMeshRenderer mesh;

	public virtual void Awake () {
		mesh = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	
	public virtual void Update() {
		if (Selected && mesh.material != outlineMaterial) {
			mesh.material = outlineMaterial;
		} else if (!Selected && mesh.material != defaultMaterial) {
			mesh.material = defaultMaterial;
		}
	}
}
