using UnityEngine;
using System.Collections;

public class TextFollow : MonoBehaviour {

	GameObject Cam;
	public GameObject person;
	// Use this for initialization
	void Start() {
		Cam = Camera.main.gameObject;
	}

	// Update is called once per frame
	void Update() {
		transform.rotation.SetLookRotation(-Cam.transform.forward);
		transform.position = person.transform.position + Vector3.up * 4;
		GetComponent<TextMesh>().text = person.GetComponent<NavMeshAgent>().destination.ToString();
	}
}
