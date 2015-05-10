using UnityEngine;
using System.Collections;

public class mouseLook : MonoBehaviour {
	
	float lookSpeed = 5;
	float verticalRot = 0f;
	float horizontalRot = 0f;
	float verticalLimit = 90f;
	public GameObject camera;
	public bool networkOverride;
	public bool canLook = true;

	int mody = 1;
	float changey = 2;
	float tTimey = 0;
	int modx = 1;
	float changex = 2;
	float tTimex = 0;
	bool drunk =false;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.K)) {
			mody= -mody;
		}
		if(Input.GetKeyDown(KeyCode.Escape)) {
			print ("Lock");
			if(modx==0) {
				modx = 1;
				mody = 1;
			} else {
				modx = 0;
				mody = 0;
			}
		}

		if (GetComponent<NetworkView>().isMine || networkOverride) {
			if(canLook) {
				if(!GetComponent<InventoryDisplay>().inventoryOpen) {
					horizontalRot = Input.GetAxis ("Mouse X") * modx* lookSpeed;
					transform.Rotate (0, horizontalRot, 0);
					verticalRot -= Input.GetAxis ("Mouse Y") * mody*lookSpeed;
					verticalRot = Mathf.Clamp (verticalRot, -verticalLimit, verticalLimit);
					camera.transform.localRotation = Quaternion.Euler (verticalRot, 0, 0);
				}
			}
		}
	}
}
