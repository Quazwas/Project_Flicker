using UnityEngine;
using System.Collections;

public class playerInteract : MonoBehaviour {
	
	public float useRange = 5f;
	public GameObject cam;
	[SerializeField]
	LayerMask lMask;
	Inventory inventory;

	void Start() {
		inventory = GetComponent<Inventory>();
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
			if(Physics.Raycast (cam.transform.position, cam.transform.TransformDirection (Vector3.forward), out hit, useRange, lMask)) {
				if(hit.transform.gameObject.tag == "Pickup") {
					itemDetails details = hit.transform.gameObject.GetComponent<itemDetails>();
					if(details.isContainer) {
						int newContI = inventory.addContainer(details.itemIndex, details.damage);
						if(newContI != -1) {
							for(int i = 0; i < details.subItemIndex.Count; i++) {
								inventory.addItemToContainer(newContI, details.subItemIndex[i], details.subItemDamage[i]);
							}
							Network.Destroy(hit.transform.gameObject);
						}
					} else {
						if(inventory.addItemToBackpack(hit.transform.gameObject.GetComponent<itemDetails>().itemIndex, hit.transform.gameObject.GetComponent<itemDetails>().damage)) {
							Network.Destroy(hit.transform.gameObject);
						}
					}
				}
			}
		}
	}
}