using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BasicObject : MonoBehaviour {
	public bool IsGhost;
	
	public bool Selected;

	public float Sight = 9f;
	public float InteractRange = 2f;

	public virtual void Awake() {
		//
	}
	
	public virtual void Start() {
		GetComponentInChildren<SkinnedMeshRenderer>().material.color = 
			Util.GetTerrainAtPosition(transform.position);
	}

	public virtual void Update() {
		if (IsGhost) {
			return;
		}
		
		try {
			Color playerColor = GetComponentInParent<Player>().GetPlayerMaterial().color;
		
			if (Selected) {
					new Color(
					playerColor.r + 0.5f,
					playerColor.g + 0.5f,
					playerColor.b + 0.5f
				);
			} else {
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = playerColor;
			}
		} catch {}
	}

	public GameObject FindClosestObject<Type>() {
		GameObject finalObject = null;

		Vector3 currentPosition = transform.position;
		float currentClosestDistanceSquaredToTarget = Mathf.Infinity;
		foreach (GameObject potentialTarget in GameObject.FindObjectsOfType<GameObject>().ToList()
			.Where(x => x.GetComponent<Type>() != null)) {
			Vector3 heading = potentialTarget.transform.position - currentPosition;
			float distanceSquaredToTarget = heading.sqrMagnitude;

			if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
				currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
				finalObject = potentialTarget;
			}
		}

		return finalObject;
	}

	public GameObject FindClosestChildOf<Type>(Transform input) {
		GameObject finalObject = null;

		Vector3 currentPosition = transform.position;
		float currentClosestDistanceSquaredToTarget = Mathf.Infinity; 

		foreach (GameObject potentialTarget in FindObjectsOfType<GameObject>().ToList()
			.Where(x => x.GetComponent<Type>() != null)
			.Where(x => x.transform.IsChildOf(input))) {
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

			if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
				currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
				finalObject = potentialTarget;
			}
		}

		return finalObject;
	}
}
