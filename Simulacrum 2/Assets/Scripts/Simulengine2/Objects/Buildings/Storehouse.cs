using UnityEngine;

public class Storehouse : Building {
	public ResourceType Type;

	public override void Awake() {
		base.Awake();

		Cost = new Resources {
			Stone = 50
		};
	}
}
