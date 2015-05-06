using UnityEngine;
using System.Collections;

public class playerInteract : MonoBehaviour {

	public float useRange = 5f;
	public GameObject Operator;
	public GameObject cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<NetworkView>().isMine) {
			interact();
		}
	}

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
