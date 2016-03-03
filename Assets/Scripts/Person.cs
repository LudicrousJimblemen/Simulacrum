using System.Collections;
using UnityEngine;

public class Person : MonoBehaviour {

    public Animator personAnimator;
    public GameObject Selector;
    bool inWalk = false;
    Vector3 walkStart;
    Vector3 walkEnd;
    float walkStartTime;
    Rigidbody rb;

    // Use this for initialization
    void Awake() {
        personAnimator.enabled = true;
        
    }
	
	// Update is called once per frame
	void Update() {
        personAnimator.SetBool("spaceKeyDown", Input.GetKeyDown(KeyCode.Space));
        personAnimator.SetBool("wKeyHeld", inWalk);
        if (inWalk) {
        	float distance = Vector3.Distance (walkStart, walkEnd);
        	transform.position = Vector3.Lerp (walkStart, walkEnd, (Time.time-walkStartTime)*5/distance);
        	if (transform.position == walkEnd) inWalk = false;
        	transform.LookAt (walkEnd);
        }
    }
	
	public void startWalk (Vector3 goal) {
		print (goal);
		walkStart = transform.position;
		walkEnd = goal;
		walkStartTime = Time.time;
		inWalk = true;
	}
}
