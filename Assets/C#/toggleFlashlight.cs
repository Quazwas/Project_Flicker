using UnityEngine;
using System.Collections;

public class toggleFlashlight : MonoBehaviour {

	[SerializeField]
	GameObject light;
	bool isOn = true;
	// Update is called once per frame
	void Update () {
		if (GetComponent<NetworkView> ().isMine) {
			if(Input.GetKeyDown(KeyCode.F)) {
				NetworkViewID viewID = Network.AllocateViewID();
				GetComponent<NetworkView>().RPC ("toggleFlashLight",
				                                 RPCMode.AllBuffered,viewID, !isOn);
			}
		}
	}
	[RPC]
	void toggleFlashLight(NetworkViewID viewID, bool state) {
		isOn = state;
		if (isOn) {
			light.GetComponent<Light> ().intensity = 5;
		} else {
			light.GetComponent<Light> ().intensity = 0;
		}
	}
}
