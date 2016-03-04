using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class UnitOrganization {
	static int FullRows;
	static int Remainder;
	public static int MaxRowWidth = 21;
	public static float UnitDistance = 1.5f;
	public static Vector3[] Organize (NavMeshAgent[] Units, Vector3 Destination) {
		int[] UnitIndices = SortIndicesByProximity (Units, Destination);
		Vector3[] Destinations = new Vector3[Units.Length];
		
		Vector3 Direction = (Destination - Units[UnitIndices[0]].transform.position).normalized;
		//Vector3 Direction = Vector3.right;
		Vector3 PerpendicularDirection = Vector3.Cross (Direction, Vector3.up);
		Destinations[UnitIndices[0]] = Destination;
		GetDimensionsFromCount (Units.Length);
		string[] DebugLines = new string[1];
		if (FullRows > 0) {
			for (int r = 0; r < FullRows; r++) {
				for (int c = 0; c < MaxRowWidth; c++) {
					Destinations[UnitIndices[r * MaxRowWidth + c]] = (Destination
						+ GetOffset (r * MaxRowWidth + c) * PerpendicularDirection * UnitDistance
						+ r * UnitDistance * -Direction);
					//DebugLines[0] += (GetOffset (r * MaxRowWidth + c) * PerpendicularDirection * UnitDistance + r * UnitDistance * -Direction).ToString () + " \n";
					//DebugLines[1] += GetOffset (r * MaxRowWidth + c).ToString () + " \n";
				}
			}
		}
		for (int i = 0; i < Remainder; i++) {
			Destinations[UnitIndices[FullRows * MaxRowWidth + i]] = (Destination
				+ GetOffset (FullRows * MaxRowWidth + i) * PerpendicularDirection * UnitDistance
				+ FullRows * UnitDistance * -Direction);
			//DebugLines[0] += (GetOffset (FullRows * MaxRowWidth + i) * PerpendicularDirection * UnitDistance + FullRows * UnitDistance * -Direction).ToString () + " \n";
			//DebugLines[1] += GetOffset (FullRows * MaxRowWidth + i).ToString () + " \n";
		}
		System.IO.File.WriteAllLines (@"C:\Users\s-ssoetomo\Desktop\test.txt",DebugLines);
		//Debug (Destinations);
		return Destinations;
	}
	
	static float SqrDistance (Vector3 a, Vector3 b) {
		float x = a.x - b.x;
		float z = a.z - b.z;
		return x * x + z * z;
	}
	static int[] SortIndicesByProximity (NavMeshAgent[] Units, Vector3 Point) {
		Dictionary<int, float> Entries = new Dictionary<int, float> ();
		for (int i = 0; i < Units.Length; i ++) {
			float CurrentCheck = SqrDistance (Units[i].transform.position, Point);
			Entries.Add (i, CurrentCheck);
		}
		int[] Temp = (from c in Entries orderby c.Value select c.Key).ToArray ();
		return Temp;
	}
	static void GetDimensionsFromCount (int Count) {
		FullRows = (Count - Count % MaxRowWidth) / MaxRowWidth;
		Remainder = Count % MaxRowWidth;
	}
	static int GetOffset (int UnitIndex) {
		int temp = UnitIndex + 1;
		int RowPosition;
		
		if (UnitIndex % MaxRowWidth == 0) {
			RowPosition = MaxRowWidth;
		} else {
			RowPosition = UnitIndex % MaxRowWidth;
		}
		if (RowPosition % 2 == 0) {
			return RowPosition / 2;
		} else {
			return -(RowPosition - 1) / 2;
		}
		//return -(RowPosition - RowPosition % 2) / 2 * (RowPosition % 2);
	}
	static void Debug (Vector3[] indices) {
		string[] lines = new string[indices.Length];
		for (int i = 0; i < indices.Length; i++) {
			lines[i] = indices[i].ToString ();
		}
		System.IO.File.WriteAllLines (@"C:\Users\s-ssoetomo\Desktop\test.txt",lines);
	}
}
