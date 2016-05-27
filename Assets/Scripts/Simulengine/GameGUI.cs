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
		try {
		GetComponentsInChildren<Storehouse>()
			.ElementAt(
				Random.Range(0, GetComponentsInChildren<Storehouse>().Count())
			);
		} catch {}
		
		CurrentPlayer.SummonUnit<Citizen>().transform.Translate(new Vector3(
			Random.Range(-2.5f, 2.5f),
			0,
			Random.Range(-2.5f, 2.5f)
		));
	}

	public void SummonStorehouse() {
		CurrentPlayer.SummonBuilding<Storehouse>();
	}
}