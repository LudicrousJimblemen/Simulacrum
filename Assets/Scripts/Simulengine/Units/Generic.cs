using UnityEngine;
using System.Collections;

public class Generic : MonoBehaviour {
	public int[] Resources = { 0, 0, 0 };
	public bool Selected;
	Unit unit;

	void Awake() {
		unit = GetComponent<Unit>();
	}

	void Update() {
		GetComponent<NavMeshAgent>().speed = unit.Speed;
		GetComponent<NavMeshAgent>().angularSpeed = unit.Speed * 216;
	}
}
