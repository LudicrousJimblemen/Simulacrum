using UnityEngine;
using System.Linq;

public class GameGUI : MonoBehaviour {
	public Object personPrefab;
	public Object storehousePrefab;

	public void SummonPerson(int behavior) {
		GameObject createdPerson = Instantiate(personPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		createdPerson.layer = LayerMask.NameToLayer("Units");
		createdPerson.tag = "Unit";
		createdPerson.GetComponent<Citizen>().Behavior = (BehaviorType) behavior;

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