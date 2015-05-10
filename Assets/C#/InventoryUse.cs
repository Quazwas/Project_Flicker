using UnityEngine;
using System.Collections;

public class InventoryUse : MonoBehaviour {
	Sustainment sustainment;
	Inventory inventory;
	void Start() {
		sustainment = GetComponent<Sustainment> ();
		inventory = GetComponent<Inventory> ();
	}

	public bool useItem(Inventory.Item item) {
		switch (item.functionIndex) {
		case 1:
			sustainment.modThirst(item.value);
			return true;
			break;
		case 2:
			sustainment.modHunger(item.value);
			return true;
			break;
		case 3:
			inventory.backpack.capacity = (int)item.value;
			return true;
			break;
		}
		return false;
	}

}
