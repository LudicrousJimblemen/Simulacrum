using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BasicObject : MonoBehaviour {
	public bool Selected;

	public float Sight = 9f;

	public Material defaultMaterial;
	public Material outlineMaterial;
	//SkinnedMeshRenderer mesh;

	public float InteractRange = 2f;

	public virtual void Awake() {
		//mesh = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	public virtual void Update() {
		if (Selected) {
			//mesh.material = outlineMaterial;
		} else if (!Selected) {
			//mesh.material = defaultMaterial;
		}
	}

	public GameObject FindClosestObject<Type>() {
		GameObject finalObject = null;

		Vector3 currentPosition = transform.position;
		float currentClosestDistanceSquaredToTarget = Mathf.Infinity;

		foreach (GameObject potentialTarget in Object.FindObjectsOfType<GameObject>().ToList().Where(x => x.GetComponent<Type>() != null)) {
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

			if (distanceSquaredToTarget > Mathf.Pow(Sight, 2)) {
				break;
			}

			if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
				currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
				finalObject = potentialTarget;
			}
		}

		return finalObject;
	}
}
