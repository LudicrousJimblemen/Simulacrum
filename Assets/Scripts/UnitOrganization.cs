using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace UnitOrganization {
	public static class FighterOrganization {
		static int FullRows;
		static int Remainder;
		public static int MaxRowWidth = 12;
		public static float UnitDistance = 1.5f;

		public static Vector3[] OrganizeFighters(NavMeshAgent[] Units, Vector3 Destination) {
			int[] UnitIndices = SortIndicesByProximity(Units, Destination);
			Vector3[] Destinations = new Vector3[Units.Length];

			Vector3 Direction = (Destination - Units[UnitIndices[0]].transform.position).normalized;
			Vector3 PerpendicularDirection = Vector3.Cross(Direction, Vector3.up);
			Destinations[UnitIndices[0]] = Destination;
			GetDimensionsFromCount(Units.Length);
			if (FullRows > 0) {
				for (int r = 0; r < FullRows; r++) {
					for (int c = 0; c < MaxRowWidth; c++) {
						Destinations[UnitIndices[r * MaxRowWidth + c]] = (Destination
							+ GetOffset(GetRowPosition(r * MaxRowWidth + c)) * PerpendicularDirection * UnitDistance
							+ r * UnitDistance * -Direction);
					}
				}
			}
			for (int i = 0; i < Remainder; i++) {
				Destinations[UnitIndices[FullRows * MaxRowWidth + i]] = (Destination
					+ GetOffset(GetRowPosition(FullRows * MaxRowWidth + i)) * PerpendicularDirection * UnitDistance
					+ FullRows * UnitDistance * -Direction);
			}
			return Destinations;
		}

		public static Vector3[] OrganizeWorkers (NavMeshAgent[] Units,Vector3 Destination) {
			Vector3[] destinations = new Vector3[Units.Length];
			for (int i = 0; i < Units.Length; i++) {
				Vector3 tempDestination = Destination;
				Vector3 offset = new Vector3 (((Random.value * 2) - 1) * Units.Length/(UnitDistance*20f),0,((Random.value * 2) - 1) * Units.Length/(UnitDistance*20f));
				//offset.Normalize ();
				offset *= Random.value * UnitDistance + UnitDistance;
				tempDestination += offset;
				destinations[i] = tempDestination;
			}
			return destinations;
		}
		static float SqrDistance(Vector3 a, Vector3 b) {
			float x = a.x - b.x;
			float z = a.z - b.z;
			return x * x + z * z;
		}
		static int[] SortIndicesByProximity(NavMeshAgent[] Units, Vector3 Point) {
			Dictionary<int, float> Entries = new Dictionary<int, float>();
			for (int i = 0; i < Units.Length; i++) {
				float CurrentCheck = SqrDistance(Units[i].transform.position, Point);
				Entries.Add(i, CurrentCheck);
			}
			int[] Temp = (from c in Entries orderby c.Value select c.Key).ToArray();
			return Temp;
		}
		static void GetDimensionsFromCount(int Count) {
			string[] lines = {""};
			int i = 1;
			while (Count < i * i) {
				i++;
			}
			i--;
			FullRows = i-1;
			
			Remainder = Count % FullRows;
			
			MaxRowWidth = (Count-Remainder)/FullRows;
			
			lines[0] = FullRows + ", " + MaxRowWidth + ", " + Remainder;
			System.IO.File.WriteAllLines ("C:/Users/s-ssoetomo/Desktop/output.txt", lines);
			
			//FullRows = (Count - Count % MaxRowWidth) / MaxRowWidth;
			//Remainder = Count % MaxRowWidth;
		}
		static int GetRowPosition(int UnitIndex) {
			int temp = UnitIndex + 1;
			if (temp % MaxRowWidth == 0) {
				return MaxRowWidth;
			} else {
				return temp % MaxRowWidth;
			}
		}
		static int GetOffset(int RowPosition) {
			if (RowPosition % 2 == 0) {
				return RowPosition / 2;
			} else {
				return -(RowPosition - 1) / 2;
			}
		}
	}
}
