using UnityEngine;
using System.Collections;

public class InventoryUse : MonoBehaviour {
	Sustainment sustainment;
	Inventory inventory;
	WeaponController weaponController;
	void Start() {
		sustainment = GetComponent<Sustainment> ();
		inventory = GetComponent<Inventory> ();
		weaponController = GetComponent<WeaponController> ();
	}

	public bool useItem(Inventory.Item item) {
		switch (item.functionIndex) {
		case 1:
			sustainment.modThirst (item.value);
			return true;
			break;
		case 2:
			sustainment.modHunger (item.value);
			return true;
			break;
		case 3:
			inventory.backpack.capacity = (int)item.value;
			return true;
			break;
		case 4:
			weaponController.switchWeapon (1, (int)item.value, item.id);
			return true;
			break;
		}
		return false;
	}

}
