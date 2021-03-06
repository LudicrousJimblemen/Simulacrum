﻿using UnityEngine;
using System;

public class CameraMove : MonoBehaviour {
	private Vector3 LookDirection;

	public float PanSpeed = 1.5f;
	public float ZoomSpeed = 0f;

	public enum ZoomControl {
		ScrollWheel,
		Keys
	}
	public ZoomControl zoomControl;

	void Awake() {
		LookDirection = transform.forward;
		zoomControl = ZoomControl.Keys;
	}

	void LateUpdate() {
		float Delta = Time.deltaTime * 20;
		Vector3 FlatLook = LookDirection;
		FlatLook.y = 0;
		FlatLook.Normalize();
		transform.position += FlatLook * Input.GetAxis("Vertical") * PanSpeed * Delta
			+ Vector3.Cross(-FlatLook, Vector3.up) * Input.GetAxis("Horizontal") * PanSpeed * Delta;
		//if (zoomControl == ZoomControl.ScrollWheel) {
			GetComponent<Camera>().orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Delta);
			GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize.Clamp(5, 25);
		//} else if (zoomControl == ZoomControl.Keys) {
			GetComponent<Camera>().orthographicSize -= (Input.GetAxis("Alternate Zoom") * ZoomSpeed * Delta);
			GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize.Clamp(5, 25);
		//}
	}
}
