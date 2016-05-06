using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public PlayerInfo PlayerInfo;
	
	public int Stone;

	public Player (PlayerInfo playerInfo) {
		PlayerInfo = playerInfo;
	}

	public Material GetPlayerMaterial() {
		Color playerColor = new Color();

		switch (PlayerInfo.PlayerNumber) {
			default:
			playerColor = Color.grey;
			break;
			case 1:
			playerColor = Color.red;
			break;
			case 2:
			playerColor = Color.blue;
			break;
			case 3:
			playerColor = Color.green;
			break;
			case 4:
			playerColor = Color.yellow;
			break;
			case 5:
			playerColor = new Color(1f, 0.5f, 0f); //orange
			break;
			case 6:
			playerColor = Color.magenta;
			break;
		}

		Material newMaterial = new Material(Shader.Find("Standard"));

		newMaterial.color = playerColor + new Color(0.1f, 0.1f, 0.1f);

		return newMaterial;
	}
}