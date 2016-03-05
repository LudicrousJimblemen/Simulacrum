using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour {

    Animator PersonAnimator;
    NavMeshAgent agent;
    public GameObject Selector;

    void Awake() {
    	//PersonAnimator.enabled = true;
    	PersonAnimator = GetComponent<Animator> ();
    	agent = GetComponent<NavMeshAgent> ();
    }
    
    void Update() {
    	PersonAnimator.SetBool("spaceKeyDown", Input.GetKeyDown(KeyCode.Space));
    	PersonAnimator.SetBool("wKeyHeld", agent.velocity.sqrMagnitude > .5f);
    }
	
	public void SetDestination (Vector3 destination) {
		agent.destination = destination;
	}
}
