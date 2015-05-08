using UnityEngine;
using System.Collections;

public class dayNightCycle : MonoBehaviour {
	float speed = 1;
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right*Time.deltaTime * speed);
		if (Input.GetKey (KeyCode.M)) {
			speed +=0.5f;
		}
		if (Input.GetKey (KeyCode.N)) {
			speed -=0.5f;
		}
	}
}
