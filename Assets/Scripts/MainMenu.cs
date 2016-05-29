using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour {
	public void CreateGame() {
		SceneManager.LoadScene("Game");
	}
}