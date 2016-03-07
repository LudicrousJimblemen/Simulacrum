using UnityEngine;

public class Resource : Gaia {
	private Transform[] VisualChildren;
	public ResourceType Type;

	public int MaxStock = 100;
	public int Stock = 100;

	public virtual void Awake() {
		VisualChildren = GetComponentsInChildren<Transform>();
	}

	public virtual void Update() {
		for (int i = 0; i < VisualChildren.Length; i++) {
			int stockPerChild = Stock / VisualChildren.Length;

			//do some funky magic to make each rock smaller when need to be
			//e.g.
			//INPUT: MaxStock = 100, Stock = 80, VisualChildren.Length = 5
			//OUTPUT: {0, 1, 1, 1, 1}
			//
			//INPUT: MaxStock = 100, Stock = 50, VisualChildren.Length = 3
			//OUTPUT: {0, 0.5, 1}
			//
			//INPUT: MaxStock = 100, Stock = 37, VisualChildren.Length = 5
			//OUTPUT: {0, 0, 0, 0.85, 1}
		}
	}
}
