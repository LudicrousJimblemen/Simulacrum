using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {
	private Player currentPlayer;

	public List<GameObject> SelectedObjects = new List<GameObject>();

	void Start() {
		currentPlayer = ObjectExtension.FindComponent<Player>(x => x.PlayerInfo.IsCurrent);
	}

	void Update() {
        if (Input.GetAxis("Select") == 1) {
            RaycastHit rayInfo;
            bool hasHit = Physics.Raycast(
                new Ray(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition),
                    Camera.main.transform.forward
                ),
                out rayInfo,
                Mathf.Infinity,
                1 << LayerMask.NameToLayer("Unit")
            );
            if (hasHit) {
                SelectObject(rayInfo.transform.gameObject);
                Debug.Log(rayInfo.transform.gameObject.name);
            }
        }
    }

	public void SelectObject(GameObject selected) {
		if (Input.GetAxis("Select (Add Modifier)") != 1) {
			SelectedObjects.Clear();
		}

        if (!SelectedObjects.Contains(selected)) {
            SelectedObjects.Add(selected);
        }
	}
}