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

	public override void Awake() {
		base.Awake ();
		
		Behaviour = BehaviourType.Idle;
		CurrentAction = CitizenState.Idle;
	}

	public override void Update() {
		GetComponent<Animator>().SetBool("running", GetComponent<NavMeshAgent>().velocity.sqrMagnitude > 0.5f);

		GetComponent<NavMeshAgent>().speed = Speed;
		GetComponent<NavMeshAgent>().angularSpeed = Speed * 216;
		if (Behaviour == BehaviourType.Idle) {
			//eg
		} else if (Behaviour == BehaviourType.StoneMiner) {
			StoneMinerBehaviour ();
		} else if (Behaviour == BehaviourType.Fighter) {
			//GetComponent<NavMeshAgent> ().stoppingDistance = 0;
			//eg
		}
		
		base.Update ();
	}

	public void ChangeBehaviour (BehaviourType NewBehaviour) {
		Behaviour = NewBehaviour;
	}

	public void SelectResource (GameObject ResourceObj) {
		if (Util.EvaluateResource (Behaviour, ResourceObj.GetComponent<Resource> ().Type)) CurrentTarget = ResourceObj;
	}

	void StoneMinerBehaviour () {
		if (CurrentAction == CitizenState.Idle) {
			if (GetComponent<Animator> ().GetCurrentAnimatorClipInfo(0).First().clip.name == "CitizenDepositingState") {
				return;
			}
			
			GetComponent<Animator> ().SetBool ("depositing",false);
			GetComponent<Animator> ().SetBool ("working",false);
			
			if (Load < MaxLoad) {
				List<GameObject> nearbyResources = GameObject.FindGameObjectsWithTag ("Gaia").Where (x => x.GetComponent<Resource> () != null).ToList ();
				List<GameObject> nearbyStoneMines = nearbyResources.Where (x => x.GetComponent<Resource> ().Type == ResourceType.Stone).ToList ();
	
				Vector3 currentPosition = transform.position;
				float currentClosestDistanceSquaredToTarget = Mathf.Infinity;
	
				foreach (var potentialTarget in nearbyStoneMines) {
					Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
					float distanceSquaredToTarget = directionToTarget.sqrMagnitude;
	
					if (distanceSquaredToTarget > Mathf.Pow (Sight,2)) {
						break;
					}
	
					if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
						currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
						CurrentTarget = potentialTarget;
					}
				}
	
				if ((CurrentTarget != null) &&
					(Load < MaxLoad)) {
					GetComponent<NavMeshAgent> ().ResetPath ();
					GetComponent<NavMeshAgent> ().destination = CurrentTarget.transform.position;
					CurrentAction = CitizenState.Seeking;
				}
			} else {
				//GetComponent<NavMeshAgent> ().stoppingDistance = 2;
				GetComponent<Animator> ().SetBool ("working",false);
				List<GameObject> nearbyBuildings = GameObject.FindGameObjectsWithTag ("Building").Where (x => x.GetComponent<Storehouse> () != null).ToList ();
				List<GameObject> nearbyStorehouses = nearbyBuildings.Where (x => x.GetComponent<Storehouse> ().Type == ResourceType.Stone).ToList ();
	
				Vector3 currentPosition = transform.position;
				float currentClosestDistanceSquaredToTarget = Mathf.Infinity;
	
				foreach (var potentialTarget in nearbyStorehouses) {
					Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
					float distanceSquaredToTarget = directionToTarget.sqrMagnitude;
	
					if (distanceSquaredToTarget > Mathf.Pow (Sight,2)) {
						break;
					}
	
					if (distanceSquaredToTarget < currentClosestDistanceSquaredToTarget) {
						currentClosestDistanceSquaredToTarget = distanceSquaredToTarget;
						CurrentTarget = potentialTarget;
					}
				}
	
				if (CurrentTarget != null) {
					GetComponent<NavMeshAgent> ().ResetPath ();
					GetComponent<NavMeshAgent> ().destination = CurrentTarget.transform.position;
					CurrentAction = CitizenState.Depositing;
				}
			}
		} else if (CurrentAction == CitizenState.Seeking) {
			//GetComponent<NavMeshAgent> ().stoppingDistance = 0;
			if (CurrentTarget != null) {
				Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
				float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

				if (distanceSquaredToTarget < Mathf.Pow(CurrentTarget.GetComponent<Resource>().Range, 2)) {
					CurrentAction = CitizenState.Working;
					GetComponent<NavMeshAgent> ().ResetPath ();
					GetComponent<Animator> ().SetBool ("working",true);
				}
			} else {
				CurrentAction = CitizenState.Idle;
			}
		} else if (CurrentAction == CitizenState.Working) {
			if (CurrentTarget == null) {
				CurrentAction = CitizenState.Idle;
				return;
			}

			transform.LookAt (new Vector3 (
				CurrentTarget.transform.position.x,
				0,
				CurrentTarget.transform.position.z
			));

			if (CollectionTimer >= CollectionDelay) {
				CollectionTimer = 0;

				if (Load + 1 <= MaxLoad) {
					CurrentTarget.GetComponent<Resource> ().Stock--;
					Load++;
				} else {
					CurrentAction = CitizenState.Idle;
				}
			}

			CollectionTimer++;
		} else if (CurrentAction == CitizenState.Depositing) {
			if (CurrentTarget != null) {
				Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
				float distanceSquaredToTarget = directionToTarget.sqrMagnitude;

				if (distanceSquaredToTarget < Mathf.Pow(CurrentTarget.GetComponent<Storehouse>().Range, 2)) {
					GetComponent<Animator> ().SetBool ("depositing",true);
					Load = 0;
					CurrentAction = CitizenState.Idle;
				}
			} else {
				CurrentAction = CitizenState.Idle;
			}
		}
	}
}