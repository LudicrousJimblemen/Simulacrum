using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour {
	public void CreateGame() {
		Game createdGame = new Game();
		Player currentPlayer = new Player("TestGuy", true);
		createdGame.Players.Add(currentPlayer);
		
		createdGame.Initialize();
	}
}