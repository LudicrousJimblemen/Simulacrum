using UnityEngine;

public class Storehouse : Building {
	public ResourceType Type;

	public override void Awake() {
		base.Awake();

		InteractRange = 2.5f;
		
		Cost = new Resources {
			Stone = 50
		};
	}
}
