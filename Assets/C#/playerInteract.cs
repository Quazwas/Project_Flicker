using UnityEngine;
using System.Collections;

public class playerInteract : MonoBehaviour {
	
	public float useRange = 5f;
	public GameObject cam;
	Inventory inventory;

	void Start() {
		inventory = GameObject.Find ("Operator").GetComponent<Inventory>();
	}
	// Update is called once per frame
	void Update () {
		if(GetComponent<NetworkView>().isMine) {
			interact();
		}
	}
	
	void interact() {
		if(Input.GetKeyDown(KeyCode.E)) {
			RaycastHit hit;
			if(Physics.Raycast (cam.transform.position, cam.transform.TransformDirection (Vector3.forward), out hit, useRange)) {
				if(hit.transform.gameObject.tag == "Pickup") {
					if(hit.transform.gameObject.GetComponent<itemDetails>().isContainer) {
						if(inventory.addContainer(hit.transform.gameObject.GetComponent<itemDetails>().itemIndex)) {
							Network.Destroy(hit.transform.gameObject);
						}
					} else {
						if(inventory.addItemToBackpack(hit.transform.gameObject.GetComponent<itemDetails>().itemIndex)) {
							Network.Destroy(hit.transform.gameObject);
						}
					}
				}
			}
		}
	}
}