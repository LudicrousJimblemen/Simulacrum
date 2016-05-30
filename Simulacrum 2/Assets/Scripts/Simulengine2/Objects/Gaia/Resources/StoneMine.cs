using UnityEngine;

public class StoneMine : Resource {
	public override void Awake() {
		base.Awake();

		InteractRange = 2f;
	}

	public override void Update() {
		base.Update();

		float scale = MaxStock/20f * (Mathf.Ceil (StockPercentage * 8) / 8);

		transform.localScale = new Vector3(
			scale,
			scale,
			scale
		);
	}
}
