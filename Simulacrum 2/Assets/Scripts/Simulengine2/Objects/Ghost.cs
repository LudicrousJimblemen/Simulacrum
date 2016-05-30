using UnityEngine;

public class Ghost : MonoBehaviour {
	public void Update() {
		GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);

		RaycastHit rayInfo;
		bool hasHit = Physics.Raycast(
			new Ray(
				Camera.main.ScreenToWorldPoint(Input.mousePosition),
				Camera.main.transform.forward
			),
			out rayInfo,
			Mathf.Infinity,
			1 << LayerMask.NameToLayer("Terrain")
		);

		bool hitOnWater = ObjectExtension.FindComponent<TerrainGenerator>().GetTerrainAtPosition(rayInfo.point).Name == "Water";

		if (hasHit && !hitOnWater) {
			GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			transform.position = rayInfo.point;
		} else {
			GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		}

		if (Input.GetAxis("Submit") == 1) {
			GetComponent<BasicObject>().Parent.OwnedResources -= GetComponent<BasicObject>().Cost;

			Component myself = GetComponent<Ghost>();
			Destroy(myself); //what a sad day :(
		}
	}
}