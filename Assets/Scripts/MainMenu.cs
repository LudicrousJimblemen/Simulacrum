using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour {
	public void Start() {
		
	}
	
	public void CreateGame() {
		Application.LoadLevel("Game");
	}
}