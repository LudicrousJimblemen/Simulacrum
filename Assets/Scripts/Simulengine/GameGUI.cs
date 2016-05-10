using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameGUI : MonoBehaviour {
	public Player CurrentPlayer;
	
	void Start() {
		CurrentPlayer = Util.GetCurrentPlayer();
	}
	
	void Update() {
		GetComponentsInChildren<Text>().First(x => x.tag == "UIStoneText").text = CurrentPlayer.OwnedResources.Stone.ToString();
	}

	public void SummonPerson() {
		CurrentPlayer.SummonUnit<Citizen>();
	}

	public void SummonStorehouse() {
		CurrentPlayer.SummonBuilding<Storehouse>();
	}
}