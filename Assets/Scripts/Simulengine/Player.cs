using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {
	public PlayerInfo PlayerInfo;
	
	public int Stone;
	
	public Player(PlayerInfo playerInfo) {
		PlayerInfo = playerInfo;
	}
}