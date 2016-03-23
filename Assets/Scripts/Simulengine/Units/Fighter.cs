using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour {

	private Animator personAnimator;
	private NavMeshAgent agent;

	void Awake() {
		//PersonAnimator.enabled = true;
		personAnimator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		personAnimator.SetBool("spaceKeyDown", Input.GetKeyDown(KeyCode.Space));
		personAnimator.SetBool("wKeyHeld", agent.velocity.sqrMagnitude > 0.5f);
	}
}
