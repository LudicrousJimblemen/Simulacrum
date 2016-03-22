using UnityEngine;

public class StoneMine : Resource {
	public override void Awake() {
		base.Awake();

		InteractRange = 2f;
	}

	public override void Update() {
		base.Update();
		
		float scale = Mathf.Ceil(StockPercentage * 5) / 5;
		
		transform.localScale = new Vector3(
			scale,
			scale,
			scale
		);
	}
}
