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
		base.Awake();

		Behaviour = BehaviourType.Idle;
		CurrentAction = CitizenState.Idle;

		GetComponent<NavMeshAgent>().stoppingDistance = 0;
	}

	public override void Update() {
		base.Update();

		if (GetComponent<NavMeshAgent>().remainingDistance < InteractRange) {

		}

		GetComponent<Animator>().SetBool("running", GetComponent<NavMeshAgent>().velocity.sqrMagnitude > 0.5f);

		GetComponent<NavMeshAgent>().speed = Speed;
		GetComponent<NavMeshAgent>().angularSpeed = Speed * 216;
		if (Behaviour == BehaviourType.Idle) {
			//eg
		} else if (Behaviour == BehaviourType.StoneMiner) {
			StoneMinerBehaviour();
		} else if (Behaviour == BehaviourType.Fighter) {
			//eg
		}
	}

	public void SelectResource(GameObject ResourceObj) {
		if (Util.EvaluateResource(Behaviour, ResourceObj.GetComponent<Resource>().Type))
			CurrentTarget = ResourceObj;
	} //TODO make more of these

	void StoneMinerBehaviour() {
		if (CurrentAction == CitizenState.Idle) {
			if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CitizenDepositingState")) {
				return;
			}

			GetComponent<Animator>().SetBool("working", false);

			if (Load < MaxLoad) {
				CurrentTarget = FindClosestObject<StoneMine>();

				if (CurrentTarget == null) {
					CurrentTarget = FindClosestObject<Storehouse>();
					CurrentAction = CitizenState.Depositing;
					return;
				} else {
					GetComponent<NavMeshAgent>().destination = CurrentTarget.transform.position;
					GetComponent<NavMeshAgent>().stoppingDistance = CurrentTarget.GetComponent<BasicObject>().InteractRange;
					CurrentAction = CitizenState.Seeking;
				}
			} else {
				GetComponent<Animator>().SetBool("working", false);
				CurrentTarget = FindClosestObject<Storehouse>();

				if (CurrentTarget != null) {
					GetComponent<NavMeshAgent>().destination = CurrentTarget.transform.position;
					GetComponent<NavMeshAgent>().stoppingDistance = CurrentTarget.GetComponent<BasicObject>().InteractRange;
					CurrentAction = CitizenState.Depositing;
				}
			}
		} else if (CurrentAction == CitizenState.Seeking) {
			if (CurrentTarget != null) {
				if (GetComponent<NavMeshAgent>().remainingDistance < CurrentTarget.GetComponent<BasicObject>().InteractRange) {
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
				if (GetComponent<NavMeshAgent>().remainingDistance < CurrentTarget.GetComponent<BasicObject>().InteractRange) {
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