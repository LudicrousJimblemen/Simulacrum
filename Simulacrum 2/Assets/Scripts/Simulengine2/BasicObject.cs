using System.Linq;
using UnityEngine;

public class BasicObject : MonoBehaviour {
	public bool IsGhost;
	
	public bool Selected;

	public float Sight = 9f;
	public float InteractRange = 2f;
	
	public Resources Cost;
	
	public Player Parent;

	public Sprite Icon;

	public virtual void Awake() {
		//
	}
	
	public virtual void Start() {
		//
	}

	public virtual void Update() {
		//
	}
}
