using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	
	Inventory inventory;
	int currentWeapon = 0;
	int currentChambered = 0;
	Inventory.Item currentClip =null;
	string weaponName = "No Weapon";
	public GameObject currentWeaponObject;
	public GameObject[] guns;
	[SerializeField]
	Transform weaponPosition;
	[SerializeField]
	GameObject viewNull;
	/*
	0 = fists
	1 = m1911
	 */

	void Start() {
		inventory = GetComponent<Inventory>();
	}

	int magazines() {

		int i = 0;
		foreach (Inventory.Item item in inventory.backpack.items) {
			if(item.itemClass == "magazine") {
				i++;
			}
		}
		foreach (Inventory.Container container in inventory.backpack.containers) {
			foreach(Inventory.Item item in container.contents) {
				if(item.itemClass == "magazine") {
					i++;
				}
			}
		}
		return i;
	}
	float totalAmmo() {
		float i = 0;
		foreach (Inventory.Item item in inventory.backpack.items) {
			if (item.itemClass == "magazine") {
				i += item.value;
			}
		}
		foreach (Inventory.Container container in inventory.backpack.containers) {
			foreach (Inventory.Item item in container.contents) {
				if (item.itemClass == "magazine") {
					i += item.value;
				}
			}
		}
		return i;
	}
	int[] weaponTypes;
	
	void changeWeapon(Inventory.Item weapon) {
		int weaponType = weaponTypes [weapon.type];
	}
	
	public void switchWeapon(int weaponIndex, int chambered, int itemIndex) {
		currentWeapon = weaponIndex;
		currentChambered = chambered;
		print (itemIndex);
		weaponName = inventory.itemTypes[itemIndex].name;
		refreshWeapon();
		return;
	}
	
	public void refreshWeapon() {
		Network.Destroy (currentWeaponObject);
		currentWeaponObject = Network.Instantiate (guns[currentWeapon], weaponPosition.position, weaponPosition.rotation, 0) as GameObject;
		NetworkView nView = currentWeaponObject.GetComponent<NetworkView> ();
		NetworkViewID nViewID = Network.AllocateViewID ();
		NetworkViewID nViewID2 = weaponPosition.GetComponent<NetworkView> ().viewID;
		nView.RPC ("parentize", RPCMode.AllBuffered, nViewID, nViewID2);
	}

	public void displayWeaponDetails() {
		GUI.Label (new Rect(300,300,300,300), weaponName);
		if (currentClip != null) {
			GUI.Label (new Rect (300, 330, 300, 300), currentClip.value + "  |  " + magazines ());
		} else {
			GUI.Label (new Rect (300, 330, 300, 300), 0 + "  |  " + magazines ());
		}
	}

	public void OnGUI() {
		displayWeaponDetails ();
	}


	[SerializeField]
	public GameObject hitObject;	
	//[SerializeField]
	//GameObject muzzleFlash;
	//[SerializeField]
	//Transform flashPoint;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if(currentChambered >0) {
				shoot();
			}
		}
		if (Input.GetButtonDown ("Reload")) {
			reload ();
		}
	}

	void reload() {
		Inventory.Item bestClip = null;
		bool hasValue = false;
		foreach (Inventory.Item item in inventory.backpack.items) {
			if(item.itemClass == "magazine") {
				if(hasValue) {
					if (item.value > bestClip.value) {
						bestClip = item;
					}
				} else {
					hasValue=true;
					bestClip = item;

				}
			}
		}
		foreach (Inventory.Container container in inventory.backpack.containers) {
			foreach(Inventory.Item item in container.contents) {
				if(item.itemClass == "magazine") {
					if(hasValue) {
						if (item.value >bestClip.value) {
							bestClip = item;
						}
					} else {
						hasValue=true;
						bestClip = item;

					}
				}
			}
		}
		if (hasValue) {
			currentChambered = (int)bestClip.value;
			currentClip = bestClip;
		}
	}

	void shoot() {
		if (currentClip.value > 0) {
			currentClip.value--;
			RaycastHit hit;
			if (Physics.Raycast (viewNull.transform.position, viewNull.transform.TransformDirection (Vector3.forward), out hit, 100f)) {
				Debug.DrawLine (viewNull.transform.position, hit.point);
				Network.Instantiate (hitObject, hit.point, Quaternion.identity, 0);
				if (hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "Zombie") {
					if (hit.transform.gameObject.GetComponent<NetworkView> () as NetworkView != null) {
						NetworkViewID viewID = Network.AllocateViewID ();
						hit.transform.gameObject.GetComponent<NetworkView> ().RPC ("bullet", RPCMode.AllBuffered, viewID, 10.0f);
					}
				}
			}
			//Network.Instantiate (muzzleFlash, flashPoint.position, flashPoint.rotation, 0);
		}

	}
}





