using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Citizen : Unit {
	public BehaviorType Behavior;
	public int Load;

	public int CollectionDelay = 20;
	public int CollectionTimer = 0;

	public int MaxLoad = 10;

	public CitizenState CurrentAction;
	public GameObject CurrentTarget = null;

	NavMeshAgent navAgent;
	
	Transform OwnerPlayer;

	public override void Awake() {
		base.Awake();

		Behavior = BehaviorType.Idle;
		CurrentAction = CitizenState.Idle;
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.stoppingDistance = 0;
	}
	
	public override void Start () {
		OwnerPlayer = GetComponentsInParent<Player>().First().transform;
	}

	public override void Update() {
		base.Update();
		
		AITimer++;
		if (AITimer > AITime) {
			AITimer = 0;
		}

		GetComponent<Animator>().SetBool("running", navAgent.velocity.sqrMagnitude > 0.3f);

		navAgent.speed = Speed;
		navAgent.angularSpeed = Speed * 216;
		if (Behavior == BehaviorType.Idle) {
			//eg
		} else if (Behavior == BehaviorType.StoneMiner) {
			StoneMinerBehavior();
		} else if (Behavior == BehaviorType.Fighter) {
			//eg
		}
	}

	public void SelectResource(GameObject ResourceObj) {
		if (Util.EvaluateResource(Behavior, ResourceObj.GetComponent<Resource>().Type))
			CurrentTarget = ResourceObj;
	} //TODO make more of these

	void StoneMinerBehavior() {
		if (CurrentAction == CitizenState.Idle) {
			if (AITimer != AITime) {
				return;
			}
			
			GetComponent<Animator>().SetBool("working", false);

			if (Load < MaxLoad) {
				CurrentTarget = FindClosestChildOf<StoneMine>(GameObject.Find ("Resources").transform);

				if (CurrentTarget == null) {
					if (Load > 0) {
						if (FindClosestChildOf<Storehouse> (OwnerPlayer) != null) {
							CurrentTarget = FindClosestChildOf<Storehouse>(OwnerPlayer);
							CurrentAction = CitizenState.Depositing;
						}
						return;
					}
				} else {
					navAgent.destination = CurrentTarget.transform.position;
					navAgent.stoppingDistance = CurrentTarget.GetComponent<BasicObject>().InteractRange;
					CurrentAction = CitizenState.Seeking;
				}
			} else {
				GetComponent<Animator>().SetBool("working", false);
				CurrentTarget = FindClosestChildOf<Storehouse>(OwnerPlayer);

				if (CurrentTarget != null) {
					navAgent.destination = CurrentTarget.transform.position;
					navAgent.stoppingDistance = CurrentTarget.GetComponent<BasicObject>().InteractRange;
					CurrentAction = CitizenState.Depositing;
				}
			}
		} else if (CurrentAction == CitizenState.Seeking) {
			if (CurrentTarget != null) {
				if (navAgent.remainingDistance < CurrentTarget.GetComponent<BasicObject>().InteractRange) {
					CurrentAction = CitizenState.Working;
					GetComponent<Animator>().SetBool("working", true);
				}
			} else {
				CurrentAction = CitizenState.Idle;
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
				}
			}

			CollectionTimer++;
		} else if (CurrentAction == CitizenState.Depositing) {
			if (CurrentTarget != null) {
				if (navAgent.remainingDistance < CurrentTarget.GetComponent<BasicObject>().InteractRange) {
					transform.parent.transform.GetComponentInParent<Player>().Stone += Load;
					Load = 0;
					CurrentAction = CitizenState.Idle;
				}
			} else {
				CurrentAction = CitizenState.Idle;
			}
		}
	}
}