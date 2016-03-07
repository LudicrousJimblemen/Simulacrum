using UnityEngine;
using System.Collections;

public class Generic : MonoBehaviour {
	public int[] Resources = { 0, 0, 0 };
	public bool Selected;
	Unit unit;
	NavMeshAgent agent;

	void Awake () {
		unit = GetComponent<Unit>();
		agent = GetComponent<NavMeshAgent> ();
    	}

	void Update() {
		agent.speed = unit.Speed;
		agent.angularSpeed = unit.Speed*216;
	}
}
