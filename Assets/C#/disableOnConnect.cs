using UnityEngine;
using System.Collections;

public class disableOnConnect : MonoBehaviour {
	void OnConnectedToServer() {
		disable();
	}
	void OnServerInitialized() {
		disable();
	}
	void disable() {
		gameObject.active = false;
	}
}
