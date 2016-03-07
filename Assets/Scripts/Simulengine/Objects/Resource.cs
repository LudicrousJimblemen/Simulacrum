using UnityEngine;
using System.Collections;
using Simulengine;

public class Resource : MonoBehaviour {
	ResourceType resourceType;
	[Range (0,100)]
	public float collectionRadius; //How far away should a unit be able to collect from this resource
	[Range (0,100)]
	public float veinRadius; //How far away another of this same type of resource can be to register as being in the same vein
	[Range (0,100)]
	public int availableResource; //Max/starting amount of resource


}
