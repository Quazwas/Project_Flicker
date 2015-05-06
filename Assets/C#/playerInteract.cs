using UnityEngine;
using System.Collections;

public class playerInteract : MonoBehaviour {
<<<<<<< HEAD
	
	public float useRange = 5f;
	public GameObject cam;
	Inventory inventory;

	void Start() {
		inventory = GameObject.Find ("Operator").GetComponent<Inventory>();
	}
=======

	public float useRange = 5f;
	public GameObject Operator;
	public GameObject cam;

	// Use this for initialization
	void Start () {
	
	}
	
>>>>>>> origin/master
	// Update is called once per frame
	void Update () {
		if(GetComponent<NetworkView>().isMine) {
			interact();
		}
	}
<<<<<<< HEAD
	
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
=======

	void interact() {
		if(Input.GetKeyDown(KeyCode.E)) {
			Debug.Log ("RAYCAST");
			RaycastHit hit;
			if(Physics.Raycast (transform.position, cam.transform.TransformDirection (Vector3.forward), out hit, useRange)) {
				if(hit.transform.gameObject.GetComponent<ItemDetails>.isContainer) {
					Operator.GetComponent<Inventory>().addContainer();
				} else {
					if(Operator.GetComponent<Inventory>().addItem()) {
						Debug.Log ("PICK UP");
						Network.Destroy(hit.transform.gameObject);
					}
				}
				Debug.Log (hit.transform.gameObject);
			}
		}
	}
}
>>>>>>> origin/master
