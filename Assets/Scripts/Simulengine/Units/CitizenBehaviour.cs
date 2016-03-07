using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CitizenBehaviour : MonoBehaviour {
	
	public GameObject target;
	Citizen citizen;
	
	public void Awake() {
		citizen = GetComponent<Citizen> ();
		citizen.animator = GetComponent<Animator>();
		citizen.navMeshAgent = GetComponent<NavMeshAgent>();

		//Behaviour = BehaviourType.Idle;
		citizen.CurrentAction = CitizenState.Idle;
	}
	
	void Update () {
		citizen.animator.SetBool("running", citizen.navMeshAgent.velocity.sqrMagnitude > 0.5f);

		citizen.navMeshAgent.speed = citizen.Speed;
		citizen.navMeshAgent.angularSpeed = citizen.Speed * 216;

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
					citizen.navMeshAgent.destination = closestTarget.transform.position;
					citizen.CurrentAction = CitizenState.Seeking;
				}
			} else if (citizen.CurrentAction == CitizenState.Seeking) {
				//move the dudes towards the place
			}
		}
	}
}
