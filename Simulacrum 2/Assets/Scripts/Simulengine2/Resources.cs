using System;

public struct Resources : IEquatable<Resources> { //Comparable struct for resources (e.g. owned resources, cost, etc.)
	private int member;

	public int Stone;

	public override bool Equals(object obj) {
		if (obj is Resources) {
			return Equals((Resources) obj);
		} else {
			return false;
		}
	}

	public bool Equals(Resources other) {
		return this.member == other.member;
	}

	public override int GetHashCode() {
		return member.GetHashCode();
	}

	public static bool operator ==(Resources left, Resources right) {
		return left.Equals(right);
	}

	public static bool operator !=(Resources left, Resources right) {
		return !left.Equals(right);
	}

	public static bool operator >(Resources left, Resources right) {
		return left.Stone > right.Stone;
	}

	public static bool operator >=(Resources left, Resources right) {
		return left.Stone >= right.Stone;
	}

	public static bool operator <(Resources left, Resources right) {
		return left.Stone < right.Stone;
	}

	public static bool operator <=(Resources left, Resources right) {
		return left.Stone <= right.Stone;
	}

	public static Resources operator +(Resources left, Resources right) {
		return new Resources {
			Stone = left.Stone + right.Stone
		};
	}

	public static Resources operator -(Resources left, Resources right) {
		return new Resources {
			Stone = left.Stone - right.Stone
		};
	}
}