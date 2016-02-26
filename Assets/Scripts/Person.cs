using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    public Animator personAnimator; //Animator

    void Start() { //Initially called
        personAnimator.enabled = true;
    }

	void Update() { //Called once per frame (60FPS)
		personAnimator.SetBool ("spaceKeyDown", Input.GetKeyDown (KeyCode.Space)); //Triggers when Space key is pressed; used for Excited state
		personAnimator.SetBool ("wKeyHeld", Input.GetKey (KeyCode.W)); //True when W is held; used for Running state
    }
}
