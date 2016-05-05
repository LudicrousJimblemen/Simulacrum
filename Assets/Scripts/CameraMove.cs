using UnityEngine;
using System;

public class CameraMove : MonoBehaviour {
	private Vector3 LookDirection;

	public float PanSpeed = 1.5f;
	public float ZoomSpeed = 0.5f;

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
		Vector3 FlatLook = LookDirection;
		FlatLook.y = 0;
		FlatLook.Normalize();
		transform.position += FlatLook * Input.GetAxis("Vertical") * PanSpeed
			+ Vector3.Cross(-FlatLook, Vector3.up) * Input.GetAxis("Horizontal") * PanSpeed;
		if (zoomControl == ZoomControl.ScrollWheel) {
			GetComponent<Camera>().orthographicSize += (Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
			GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize.Clamp(5, 25);
		} else if (zoomControl == ZoomControl.Keys) {
			GetComponent<Camera>().orthographicSize += (Input.GetAxis("Alternate Zoom") * ZoomSpeed);
			GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize.Clamp(5, 25);
		}
	}
}
