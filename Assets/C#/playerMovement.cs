using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
	float speed = 6f;
	float jump = 8f;
	float gravity = 20f;
	public bool networkOverride;
	Vector3 moveDirection = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<NetworkView>().isMine || networkOverride) {
			CharacterController controller = GetComponent<CharacterController> ();
			if (controller.isGrounded) {
				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				if (Input.GetButton ("Jump")) {
					moveDirection.y = jump;
				}
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
		}
	}
}
