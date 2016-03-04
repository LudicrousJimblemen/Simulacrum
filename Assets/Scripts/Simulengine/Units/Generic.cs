using UnityEngine;
using System.Collections;

public class Generic : MonoBehaviour {
	public bool Selected = false;
	public int[] Resources = {0, 0, 0};
	
	void Update () {
		transform.GetChild (2).gameObject.SetActive (Selected);
	}
	
	void CollectResources () {
		
	}
}
