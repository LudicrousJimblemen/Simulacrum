using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
	public PlayerInfo PlayerInfo;

	public Resources OwnedResources;

	private Game game;

	public Player(PlayerInfo playerInfo) {
		PlayerInfo = playerInfo;
	}

	void Start() {
		if (!PlayerInfo.IsHuman) {
			//gameObject.AddComponent<DefaultAI>(); //add ai to nonhuman players
		}

		this.game = FindObjectOfType<Game>(); //shortcut to reference game
	}

	void OnGUI() {
		if (PlayerInfo.IsCurrent) {
			var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			texture.SetPixels(new Color[] { new Color32(255, 255, 255, 50) });
			texture.Apply();

			GUI.DrawTexture(
				new Rect(0, 0, 2000, 50),
				texture
			);

			GUI.DrawTexture(
				new Rect(0, Screen.height - 100, 2000, 100),
				texture
			);
		}
	}

	public GameObject SummonObject(Object toSummon, Vector3 location, bool takeResource = true) {
		if (GetComponentInChildren<Ghost>() != null) {
			return null;
		}

		GameObject summonedObject = Instantiate(toSummon, location, Quaternion.identity) as GameObject;
		summonedObject.transform.parent = transform;
		summonedObject.GetComponent<BasicObject>().Parent = GetComponent<Player>();
		if (takeResource) {
			if (summonedObject.GetComponent<BasicObject>().Cost > OwnedResources) {
				Destroy(summonedObject);
				return null; //TODO add feedback to show that player cannot afford
			} else {
				if (summonedObject.GetComponent<Building>() == null || !PlayerInfo.IsHuman) {
					OwnedResources -= summonedObject.GetComponent<BasicObject>().Cost;
					return summonedObject;
				} else {
					summonedObject.AddComponent<Ghost>();
					return summonedObject;
				}
			}
		} else {
			return summonedObject;
		}
	}
}