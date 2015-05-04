using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed = 10f;
 
	void Update() {
    	if (GetComponent<NetworkView>().isMine) {
        	InputMovement();
    	}
	}
 
    void InputMovement() {
        if (Input.GetKey(KeyCode.W)) {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);
        }
 
        if (Input.GetKey(KeyCode.S)) {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - transform.forward * speed * Time.deltaTime);
        }
 
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime*20);
        }
 
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up * speed * Time.deltaTime*20);
        }
    }

    
}