using UnityEngine;
using System.Collections;

public class mouseLook : MonoBehaviour {
	
	float lookSpeed = 5;
	float verticalRot = 0f;
	float horizontalRot = 0f;
	float verticalLimit = 90f;
	public GameObject camera;
	public bool networkOverride;

	void Start() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		if (GetComponent<NetworkView>().isMine || networkOverride) {
			horizontalRot = Input.GetAxis ("Mouse X") * lookSpeed;
			transform.Rotate (0, horizontalRot, 0);
			verticalRot -= Input.GetAxis ("Mouse Y") * lookSpeed;
			verticalRot = Mathf.Clamp (verticalRot, -verticalLimit, verticalLimit);
			camera.transform.localRotation = Quaternion.Euler (verticalRot, 0, 0);
		}
	}
}
