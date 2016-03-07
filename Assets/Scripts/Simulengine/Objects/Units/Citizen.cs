using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Citizen : Unit {
	public BehaviourType Behaviour;
	public List<KeyValuePair<ResourceType, int>> Load;
	public int MaxLoad;

	public CitizenState CurrentAction;
}
