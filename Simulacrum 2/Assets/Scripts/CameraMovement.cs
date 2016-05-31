using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public float PanSpeed = 1.5f;
	public float ZoomSpeed = 0f;

	void LateUpdate() {
		float Delta = Time.deltaTime * 20;

		Vector3 FlatLook = transform.forward;

		FlatLook.y = 0;
		FlatLook.Normalize();
		transform.position += FlatLook * Input.GetAxis("Vertical") * PanSpeed * Delta
			+ Vector3.Cross(-FlatLook, Vector3.up) * Input.GetAxis("Horizontal") * PanSpeed * Delta;

		GetComponent<Camera>().orthographicSize -= (Input.GetAxis("Zoom") * ZoomSpeed * Delta).Clamp(5, 25);
		GetComponent<Camera>().orthographicSize += (Input.GetAxis("Zoom Keys") * ZoomSpeed * Delta).Clamp(5, 25); //TODO fix zoom
	}
	/*
	Vector3 LookAt(Vector3 position) {
		Vector3 forward = transform.forward;
		forward.Normalize();
		transform.position = position;
		//TODO
	}
	*/
}
