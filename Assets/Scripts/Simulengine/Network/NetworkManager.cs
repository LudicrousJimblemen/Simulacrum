using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Net;

class NetworkManager : MonoBehaviour {
	public string Ip;
	
	void Start() {
		try {
			Ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
			Debug.Log(Ip);
		} catch (Exception) {
			Ip = "127.0.0.1";
		}
		
		FindObjectOfType<IPText>().GetComponent<Text>().text = "IP: " + Ip;
	}
}