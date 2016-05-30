using System.Linq;
using UnityEngine;

public class BasicObject : MonoBehaviour {
	public bool IsGhost;

	public bool Selected;

	public float Sight = 9f;

	public Resources Cost;

	public Player Parent;

	public Texture2D Icon;

	public virtual void Awake() {
		//
	}

	public virtual void Start() {
		//
	}

	public virtual void Update() {
		IsGhost = GetComponent<Ghost>() != null;
		if (IsGhost) {
			return;
		}

		GetComponentInChildren<SkinnedMeshRenderer>().material.color = ObjectExtension.FindComponent<Game>().GetPlayerColor(Parent.PlayerInfo.PlayerNumber);
	}
}
