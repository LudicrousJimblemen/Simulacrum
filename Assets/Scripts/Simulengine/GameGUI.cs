using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameGUI : MonoBehaviour {
	public UnityEngine.Object personPrefab;
	
	public Material GetPlayerMaterial() {
		Color playerColor = new Color();
		
		switch (GetCurrentPlayer().PlayerInfo.PlayerNumber) {
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
<<<<<<< HEAD
		
		return newMaterial;
=======
>>>>>>> 6da7a56c030a60e34dfad477609c53b8cbf43e05
	}
	
	public Player GetCurrentPlayer() {
		return FindObjectsOfType<Player>().Where(
<<<<<<< HEAD
			x => x.PlayerInfo.IsCurrent
=======
			x => x.GetComponent<Player>().PlayerInfo.IsCurrent
>>>>>>> 6da7a56c030a60e34dfad477609c53b8cbf43e05
		).ToList().First();
	}
	
	public void SummonPerson() {
		GameObject createdPerson = Instantiate(
			personPrefab, Vector3.zero, Quaternion.identity
		) as GameObject;
		
		createdPerson
			.GetComponentInChildren<SkinnedMeshRenderer>()
			.material = GetPlayerMaterial();
	}
}