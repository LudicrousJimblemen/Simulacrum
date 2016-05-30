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
}