using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameGUI : MonoBehaviour {
	public UnityEngine.Object personPrefab;
	public UnityEngine.Object storehousePrefab;

	public void SummonPerson(BehaviorType behavior) {
		GameObject createdPerson = Instantiate(personPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		createdPerson.layer = LayerMask.NameToLayer("Units");
		createdPerson.tag = "Unit";
		createdPerson.GetComponent<Citizen>().Behavior = behavior;

		createdPerson.GetComponentInChildren<SkinnedMeshRenderer>().material =
			FindObjectsOfType<Player>()
			.Where(x => x.PlayerInfo.IsCurrent)
			.First()
			.GetPlayerMaterial();

		createdPerson.transform.parent = FindObjectsOfType<Player>()
			.Where(x => x.PlayerInfo.IsCurrent)
			.First().transform;
	}

	public void SummonStorehouse() {
		GameObject createdPerson = Instantiate(storehousePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		createdPerson.layer = LayerMask.NameToLayer("Units");
		createdPerson.tag = "Unit";

		createdPerson.GetComponentInChildren<SkinnedMeshRenderer>().material =
			FindObjectsOfType<Player>()
			.Where(x => x.PlayerInfo.IsCurrent)
			.First()
			.GetPlayerMaterial();

		createdPerson.transform.parent = FindObjectsOfType<Player>()
			.Where(x => x.PlayerInfo.IsCurrent)
			.First().transform;
	}
}