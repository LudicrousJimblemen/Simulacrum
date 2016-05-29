using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public void CreateGame() { //called when "do it!" button is pressed
		SceneManager.LoadScene("Game"); //load game scene
	}
}