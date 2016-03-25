using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public string Username;
	public bool IsHuman;
	
	public int Stone;
	
	public Player(string username, bool isHuman) {
		this.Username = username;
		this.IsHuman = isHuman;
	}
}