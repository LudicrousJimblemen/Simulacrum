using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	Vector3 LookDirection;
	public float CameraSpeed = 2.5f;
	
	void Awake () {
		LookDirection = transform.forward;
	}
	
	void LateUpdate () {
		Vector3 FlatLook = LookDirection;
		FlatLook.y = 0;
		FlatLook.Normalize ();
		transform.position += FlatLook * Input.GetAxis ("Vertical") * CameraSpeed + Vector3.Cross (-FlatLook, Vector3.up) * Input.GetAxis ("Horizontal") * CameraSpeed;
	}
}
