using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Citizen : Unit {
	public BehaviourType Behaviour;
	public List<KeyValuePair<ResourceType, int>> Load;

	public int MaxLoad = 10;

	public CitizenState CurrentAction;

	public virtual void Awake() {
		//Behaviour = BehaviourType.Idle;
		CurrentAction = CitizenState.Idle;
	}

	public virtual void Update() {
		GetComponent<Animator>().SetBool("running", GetComponent<NavMeshAgent>().velocity.sqrMagnitude > 0.5f);

		GetComponent<NavMeshAgent>().speed = Speed;
		GetComponent<NavMeshAgent>().angularSpeed = Speed * 216;

		if (Behaviour == BehaviourType.StoneMiner) {
			if (CurrentAction == CitizenState.Idle) {
				List<GameObject> nearbyResources = GameObject.FindGameObjectsWithTag("Gaia").Where(x => x.GetComponent<Resource>() != null).ToList();
				List<GameObject> nearbyStoneMines = nearbyResources.Where(x => x.GetComponent<Resource>().Type == ResourceType.Stone).ToList();

				Vector3 currentPosition = transform.position;
				float currentClosestDistanceSquaredToTarget = Mathf.Infinity;
				GameObject closestTarget = null;

				foreach (var potentialTarget in nearbyStoneMines) {
					Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
					float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

					if (distanceSquaredToTarget > Sight)
						break;

					if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
						currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
						closestTarget = potentialTarget;
					}
				}

				if (closestTarget != null) {
					GetComponent<NavMeshAgent>().destination = closestTarget.transform.position;
					CurrentAction = CitizenState.Seeking;
				}
			} else if (CurrentAction == CitizenState.Seeking) {
				//move the dudes towards the place
			}
		}
	}
}