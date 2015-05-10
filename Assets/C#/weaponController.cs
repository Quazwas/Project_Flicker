using UnityEngine;
using System.Collections;

public class weaponController : MonoBehaviour {

	Inventory inventory;

	int magazines() {
		int i = 0;
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
		foreach (Inventory.Container container in inventory.backpack.containers) {
			foreach(Inventory.Item item in container.contents) {
				if(item.itemClass == "magazine") {
					i+=item.value;
				}
			}
		}
		return i;
	}

	int[] weaponTypes;

	void changeWeapon(Inventory.Item weapon) {
		int weaponType = weaponTypes [weapon.type];
	}

}
