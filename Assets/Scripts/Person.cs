using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    public Animator personAnimator;

    // Use this for initialization
    void Start() {
        personAnimator.enabled = true;
    }
	
	// Update is called once per frame
	void Update() {
        personAnimator.SetBool("spaceKeyDown", Input.GetKeyDown(KeyCode.Space));
        personAnimator.SetBool("wKeyHeld", Input.GetKey(KeyCode.W));
    }
}
