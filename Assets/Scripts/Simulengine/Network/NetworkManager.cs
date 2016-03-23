using UnityEngine;
using System.Linq;

class NetworkManager : MonoBehaviour {
	private const string typeName = "JimblemenSimulacrum";
	private const string gameName = "bumgame";

	private HostData[] hostList;

	private void StartServer() {
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized() {
		Debug.Log("Server Initializied");
	}

	void OnConnectedToServer() {
		Debug.Log("Server Connected To");
	}

	private void RefreshHostList() {
		MasterServer.RequestHostList(typeName);
		Debug.Log("refreshed servers");
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData) {
		Network.Connect(hostData);
		Debug.Log("joined server");
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.N) &&
			!Network.isClient &&
			!Network.isServer) {
			StartServer();
		}
		if (Input.GetKeyDown(KeyCode.R) &&
			!Network.isClient &&
			!Network.isServer) {
			RefreshHostList();

			if (hostList != null) {
				string s = " ";
				for (int i = 0; i < hostList.Length; i++) {
					s += hostList[i].gameName + " ";
				}
				Debug.Log("yes servers");
				Debug.Log(s);
			} else {
				Debug.Log("no servers");
			}
		}
		if (Input.GetKeyDown(KeyCode.J) &&
			!Network.isClient &&
			!Network.isServer) {
			RefreshHostList();

			if (hostList != null) {
				JoinServer(hostList.First());
			}
		}
	}
}