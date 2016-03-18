using UnityEngine;

public class StoneMine : Resource {
	public override void Awake() {
		base.Awake();

		Range = 1.6f;
	}

	public override void Update() {
		base.Update();
		
		transform.localScale = new Vector3(
			StockPercentage,
			StockPercentage,
			StockPercentage
		);
	}
}
