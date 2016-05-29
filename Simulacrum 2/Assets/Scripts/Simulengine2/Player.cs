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
}