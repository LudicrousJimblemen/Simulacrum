using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Citizen : Unit {
	public BehaviourType Behaviour;
	public int Load;

	public int CollectionDelay = 20;
	public int CollectionTimer = 0;

	public int MaxLoad = 10;

	public CitizenState CurrentAction;
	public GameObject CurrentTarget = null;

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
				GetComponent<Animator>().SetBool("working", false);

				List<GameObject> nearbyResources = GameObject.FindGameObjectsWithTag("Gaia").Where(x => x.GetComponent<Resource>() != null).ToList();
				List<GameObject> nearbyStoneMines = nearbyResources.Where(x => x.GetComponent<Resource>().Type == ResourceType.Stone).ToList();

				Vector3 currentPosition = transform.position;
				float currentClosestDistanceSquaredToTarget = Mathf.Infinity;

				foreach (var potentialTarget in nearbyStoneMines) {
					Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
					float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

					if (distanceSquaredToTarget > Mathf.Pow(Sight, 2))
						break;

					if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
						currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
						CurrentTarget = potentialTarget;
					}
				}

				if ((CurrentTarget != null) &&
					(Load != MaxLoad)) {
					GetComponent<NavMeshAgent>().ResetPath();
					GetComponent<NavMeshAgent>().destination = CurrentTarget.transform.position;
					CurrentAction = CitizenState.Seeking;
				}
			} else if (CurrentAction == CitizenState.Seeking) {
				Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
				float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

				if (distanceSquaredToTarget < CurrentTarget.GetComponent<Resource>().Range) {
					CurrentAction = CitizenState.Working;
					GetComponent<NavMeshAgent>().ResetPath();
					GetComponent<Animator>().SetBool("working", true);
				}
			} else if (CurrentAction == CitizenState.Working) {
				if (CurrentTarget == null) {
					CurrentAction = CitizenState.Idle;
					return;
				}

				transform.LookAt(new Vector3(
					CurrentTarget.transform.position.x,
					0,
					CurrentTarget.transform.position.z
				));

				if (CollectionTimer >= CollectionDelay) {
					CollectionTimer = 0;

					if (Load + 1 <= MaxLoad) {
						CurrentTarget.GetComponent<Resource>().Stock--;
						Load++;
					} else {
						CurrentAction = CitizenState.Idle;
						//CurrentAction = CitizenState.Depositing;
					}
				}

				CollectionTimer++;
			}
		}
	}
}