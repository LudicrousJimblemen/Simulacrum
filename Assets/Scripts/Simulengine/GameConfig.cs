using UnityEngine;
using System;
using System.Collections.Generic;

public class GameConfig : MonoBehaviour {
	public List<Player> players = List<Player>();

	public void Start() {
		DontDestroyOnLoad(gameObject);
	}
}