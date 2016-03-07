using UnityEngine;
using System;

public class CameraMove : MonoBehaviour {
	private Vector3 LookDirection;

	public float PanSpeed = 1.5f;
	public float ZoomSpeed = 6.5f;

	void Awake() {
		LookDirection = transform.forward;
	}

	void LateUpdate() {
		Vector3 FlatLook = LookDirection;
		FlatLook.y = 0;
		FlatLook.Normalize();
		transform.position += FlatLook * Input.GetAxis("Vertical") * PanSpeed
			+ Vector3.Cross(-FlatLook, Vector3.up) * Input.GetAxis("Horizontal") * PanSpeed;

		GetComponent<Camera>().orthographicSize += (-Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
		GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize.Clamp(5, 20);
	}
}
