using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	public int Stone;

	void Start() {
		//
	}

	void Update() {
		GameObject.Find("UIStoneText").GetComponent<Text>().text = String.Format("Stone\n{0}", Stone);
	}
}
