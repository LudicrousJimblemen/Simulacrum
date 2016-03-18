public class Resource : Gaia {
	public ResourceType Type;

	public int MaxStock = 20;
	public int Stock = 20;

	public float StockPercentage;

	public float Range = 2f;

	public override void Awake() {
		//
	}

	public override void Update() {
		if (Stock <= 0) {
			Destroy(this.gameObject);
		} else {
			StockPercentage = (float) Stock / MaxStock;
		}
	}
}
