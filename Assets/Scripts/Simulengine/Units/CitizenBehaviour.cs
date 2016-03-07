using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//stop it

public class CitizenBehaviour : MonoBehaviour {
	
	public GameObject target;
	Citizen citizen;
	
	public void Awake() {
		//Behaviour = BehaviourType.Idle;
		citizen.CurrentAction = CitizenState.Idle;
	}
	
	void Update () {
		GetComponent<Animator>().SetBool("running", GetComponent<NavMeshAgent>().velocity.sqrMagnitude > 0.5f);

		GetComponent<NavMeshAgent>().speed = citizen.Speed;
		GetComponent<NavMeshAgent>().angularSpeed = citizen.Speed * 216;

		if (citizen.Behaviour == BehaviourType.StoneMiner) {
			if (citizen.CurrentAction == CitizenState.Idle) {
				List<GameObject> nearbyResources = GameObject.FindGameObjectsWithTag("Gaia").Where(x => x.GetComponent<Resource>() != null).ToList();
				List<GameObject> nearbyStoneMines = nearbyResources.Where(x => x.GetComponent<Resource>().Type == ResourceType.Stone).ToList();

				Vector3 currentPosition = transform.position;
				float currentClosestDistanceSquaredToTarget = Mathf.Infinity;
				GameObject closestTarget = null;

				foreach (var potentialTarget in nearbyStoneMines) {
					Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
					float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

					if (distanceSquaredToTarget > citizen.Sight)
						break;

					if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
						currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
						closestTarget = potentialTarget;
					}
				}

				if (closestTarget != null) {
					GetComponent<NavMeshAgent>().destination = closestTarget.transform.position;
					citizen.CurrentAction = CitizenState.Seeking;
				}
			} else if (citizen.CurrentAction == CitizenState.Seeking) {
				//move the dudes towards the place
			}
		}
	}
}
