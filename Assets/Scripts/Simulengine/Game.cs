using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {
	public List<Player> Players;
	public int Size = 8;
	
	public Game() {
		Players = new List<Player>();
	}
}