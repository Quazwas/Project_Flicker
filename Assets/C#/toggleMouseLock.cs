using UnityEngine;
using System.Collections;

public class toggleMouseLock : MonoBehaviour {

	void OnServerInitialized() {
		toggleLock (false);
	}
	void OnConnectedToServer() {
		toggleLock (false);
	}

	void Start() {
		toggleLock (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			toggleLock();
		}
		if (GetComponent<InventoryDisplay> ().inventoryOpen) {
			toggleLock(true);
		} else {
			toggleLock(false);
		}
	}

	void toggleLock() {
		Cursor.visible = !Cursor.visible;
		if(Cursor.lockState == CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.None;
			GetComponent<mouseLook>().canLook = false;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			GetComponent<mouseLook>().canLook = true;
		}
	}

	void toggleLock(bool state) {
		if(state) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			GetComponent<mouseLook>().canLook = false;
		} else {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			GetComponent<mouseLook>().canLook = true;
		}
	}
}
