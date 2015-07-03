using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public bool isOpen = false;
	Vector3 hinge;
	[SerializeField]
	float offset;
	[SerializeField]
	float speed;
	[SerializeField]
	float rotate;

	public float currentRotation = 0;



	// Use this for initialization
	void Start () {
		hinge = this.transform.position + (this.transform.right * offset);
	}

	void Update() {
		if(isOpen && Mathf.Abs(currentRotation) < Mathf.Abs(rotate)) {
			if(rotate > 0) {
				transform.RotateAround(hinge, Vector3.up, speed * Time.deltaTime);
				currentRotation += speed * Time.deltaTime;
			} else {
				transform.RotateAround(hinge, Vector3.up, -speed * Time.deltaTime);
				currentRotation -= speed * Time.deltaTime;
			}
		}

		if(!isOpen) {
			if(rotate > 0 && currentRotation > 0) {
				transform.RotateAround(hinge, Vector3.up, -speed * Time.deltaTime);
				currentRotation -= speed * Time.deltaTime;
			} else if (rotate < 0 && currentRotation < 0) {
				transform.RotateAround(hinge, Vector3.up, speed * Time.deltaTime);
				currentRotation += speed * Time.deltaTime;
			}
		}
	}
	
	// Update is called once per frame
	[RPC]
	public void setState(bool state) {
		isOpen = state;
		GetComponent<NetworkView>().RPC("setState",RPCMode.OthersBuffered,state);
	}
}
