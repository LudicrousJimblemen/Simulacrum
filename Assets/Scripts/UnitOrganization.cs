using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class UnitOrganization {
	static int FullRows;
	static int Remainder;
	public static int MaxRowWidth = 7;
	public static float UnitDistance = 1f;
	public static List<Vector3> Organize (List<NavMeshAgent> Units, Vector3 Destination) {
		List<int> UnitIndices = SortIndicesByProximity (Units, Destination);
		List<Vector3> Destinations = new List<Vector3> (Units.Count);
		//Vector3 Direction = (Destination - Units[UnitIndices[0]].transform.position).normalized;
		Vector3 Direction = Vector3.right;
		Vector3 PerpendicularDirection = Vector3.Cross (Direction, Vector3.up);
		Units[UnitIndices[0]].destination = Destination;
		GetDimensionsFromCount (Units.Count);
		for (int r = 0; r < FullRows; r ++) {
			for (int c = 0; c < MaxRowWidth; c ++) {
				Destinations[UnitIndices[r*MaxRowWidth + c]] = (Destination
					+ GetOffset (r*MaxRowWidth + c) * Direction * UnitDistance 
					+ r * UnitDistance * PerpendicularDirection);
			}
		}
		
		for (int i = 0; i < Remainder; i ++) {
			Destinations[UnitIndices[FullRows*MaxRowWidth + i]] = (Destination
				+ GetOffset (FullRows * MaxRowWidth + i) * Direction * UnitDistance
				+ FullRows * UnitDistance * PerpendicularDirection);
		}
		
		return Destinations;
	}
	
	static float SqrDistance (Vector3 a, Vector3 b) {
		float x = a.x - b.x;
		float z = a.z - b.z;
		return x * x + z * z;
	}
	static List<int> SortIndicesByProximity (List<NavMeshAgent> Units, Vector3 Point) {
		Dictionary<int, float> Entries = new Dictionary<int, float> ();
		List<int> Temp = new List<int> ();
		for (int i = 0; i < Units.Count; i ++) {
			float CurrentCheck = SqrDistance (Units[i].transform.position, Point);
			Entries.Add (i, CurrentCheck);
		}
		
		foreach (KeyValuePair<int, float> Unit in Entries.OrderBy (key => key.Value)) {
			Temp.Add (Unit.Key);
		}
		return Temp;
	}
	static void GetDimensionsFromCount (int Count) {
		FullRows = (Count-Count%MaxRowWidth)/MaxRowWidth;
		Remainder = Count%MaxRowWidth;
	}
	static int GetOffset (int UnitIndex) {
		int RowPosition;
		if (UnitIndex != MaxRowWidth) {
			RowPosition = UnitIndex%MaxRowWidth;
		} else {
			RowPosition = UnitIndex;
		}
		return (RowPosition-RowPosition%2)/2 * -(UnitIndex%2);
	}
}
