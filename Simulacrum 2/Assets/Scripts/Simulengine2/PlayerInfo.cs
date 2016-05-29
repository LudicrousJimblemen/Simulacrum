using System;

public struct PlayerInfo {
	private int member;

	public override bool Equals(object obj) {
		if (obj is PlayerInfo) {
			return Equals((PlayerInfo) obj);
		} else {
			return false;
		}
	}

	public bool Equals(PlayerInfo other) {
		return this.member == other.member;
	}

	public override int GetHashCode() {
		return member.GetHashCode();
	}

	public string Username;
	public bool IsHuman;
	public bool IsCurrent;

	public int PlayerNumber;
}