using UnityEngine;
using System.Collections;
using Simulengine;

public class UserInput : MonoBehaviour {
	private Player player;
	
	void Start() { //Initially called
		player = transform.root.GetComponent<Player>();
	}
	
	void Update() { //Called once per frame (60FPS)
		if(player && player.isHuman) {
			MoveCamera();
		}
	}
	
	private void MoveCamera() {
		if (Input.GetKey("Up") && Input.GetKey("Down")) {
		} else if (Input.GetKey("Up")) {
			Camera.main.transform.Translate(0,0, ResourceManager.TranslateSpeed);
		} else if (Input.GetKey("Down")) {
			Camera.main.transform.Translate(0,0, -ResourceManager.TranslateSpeed);
		} 

		if (Input.GetKey("Right") && Input.GetKey("Left")) {
		} else if (Input.GetKey("Right")) {
			Camera.main.transform.Translate(ResourceManager.TranslateSpeed, 0,0);
		} else if (Input.GetKey("Left")) {
			Camera.main.transform.Translate(-ResourceManager.TranslateSpeed, 0,0);
		}
	}
}