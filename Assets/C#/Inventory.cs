using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public GameObject[] itemPrefabs;

	public class Item {
		public int size;
		public string name;
		public float damage;
		public int type;
		public int id;
		public float value;
		public float maxValue;
		public string itemClass = "fuck you";
		public int functionIndex;
		public int iconIndex;


		public Item(ItemType itemType) {
			name = itemType.name;
			damage = 100f;
			size = itemType.size;
			id = itemType.index;
			functionIndex = itemType.functionIndex;
			value = itemType.value;
			iconIndex = itemType.iconIndex;
			itemClass = itemType.itemClass;
		}
		public Item(ItemType itemType, float newDamage, string newClass) {
			name = itemType.name;
			damage = newDamage;
			size = itemType.size;
			id = itemType.index;
			functionIndex = itemType.functionIndex;
			value = itemType.value;
			iconIndex = itemType.iconIndex;
			itemClass = newClass;
		}
	}
	public class ItemType {

		public int size;
		public string name;
		public int index;
		public int prefabIndex;
		public int functionIndex;
		public float value;
		public int iconIndex;
		public string itemClass;

		public ItemType(int newIndex, string newName, int newSize, int newPrefabIndex, int newFunIndex, float newValue, int newIcon, string newClass) {
			index = newIndex;
			name = newName;
			size = newSize;
			prefabIndex = newPrefabIndex;
			functionIndex = newFunIndex;
			value = newValue;
			iconIndex = newIcon;
			itemClass = newClass;

		}
	}
	public List<ItemType> itemTypes = new List<ItemType>() {
		//new ItemType(index, name, size, prefabIndex, functionIndex, value, icon, class)
		new ItemType(0, "Baked Beans", 1, 1, 2, 15, 1, "food"),
		new ItemType(1, "Bottled Water", 1, 2, 1, 15, 0, "food"),
		new ItemType(2, "Old Photograph", 1, 0, 0, 0, 0, "misc"),
		new ItemType(3, "Large Backpack", 4, 3, 3, 25, 0, "backpack"),
		new ItemType(4, "M1911 (Pistol)", 4, 5, 4, 3, 0, "weapon"),
		new ItemType(5, "M1911 Magazine", 1, 5, 0, 10, 0, "magazine")
	};

	public class Container {
		public bool isExpanded = false;
		public int capacity;
		public float damage;
		public int type;
		public int size;
		public string name;
		public int id;
		public float value = 10f;
		public List<Item> contents = new List<Item>();
		public int prefabIndex;

		public Container(ContainerType contType) {
			name = contType.name;
			capacity = contType.capacity;
			damage = 100f;
			size = contType.size;
			id = contType.index;
		}
		public Container(ContainerType contType, int newDamage) {
			name = contType.name;
			capacity = contType.capacity;
			damage = newDamage;
			size = contType.size;
			id = contType.index;
		}
		public Container(ContainerType contType, int newDamage, int newPrefabIndex) {
			name = contType.name;
			capacity = contType.capacity;
			damage = newDamage;
			size = contType.size;
			id = contType.index;
			prefabIndex = newPrefabIndex;
		}
		public int fullness() {
			int f = 0;
			foreach (Item item in contents) {
				f+=item.size;
			}
			return f;
		}

	}
	
	public class ContainerType {
		public int capacity;
		public int type;
		public string name;
		public int size;
		public int index;
		public ContainerType(int newIndex, string newName, int newCap, int newSize) {
			name = newName;
			capacity = newCap;
			size = newSize;
			index = newIndex;
		}
		public ContainerType(int newIndex, string newName, int newCap, int newSize, int newType) {
			name = newName;
			capacity = newCap;
			type = newType;
			size = newSize;
			index = newIndex;
		}
	}
	public List<ContainerType> containerTypes = new List<ContainerType>() {
		new ContainerType(0, "Small Pouch", 2, 1, 2), //0
		new ContainerType(1, "Large Pouch", 3,1), // 1
		new ContainerType(2, "Potato Sack", 10, 3)
	};
	public class Backpack {
		public int capacity =100;
		public List<Container> containers = new List<Container>();
		public List<Item> items = new List<Item>();
		public int fullness() {
			int f = 0;
			foreach (Container cont in containers) {
				f+=cont.size;
			}
			foreach (Item item in items) {
				f+=item.size;
			}
			return f;
		}
	}


	public bool addItemToBackpack(Item newItem) {
		if(backpack.capacity - backpack.fullness() >= newItem.size) {
			backpack.items.Add (newItem);
			return true;
		}
		return false;
	}
	public bool addItemToBackpack(int itemIndex, float damage, float value) {
		if(backpack.capacity - backpack.fullness() >= itemTypes[itemIndex].size) {
			Item i = new Item(itemTypes[itemIndex]);
			i.damage = damage;
			i.value = value;
			backpack.items.Add(i);
			return true;
		}
		return false;
	}
	public bool addItemToBackpack(int itemIndex) {
		if(backpack.capacity - backpack.fullness() >= itemTypes[itemIndex].size) {
			backpack.items.Add (new Item(itemTypes[itemIndex]));
			return true;
		}
		return false;
	}
	public bool addContainer(Container newCont) {
		if(backpack.capacity - backpack.fullness() >= newCont.size) {
			backpack.containers.Add (newCont);
			return true;
		}
		return false;
	}
	public int addContainer(int containerIndex) {
		if(backpack.capacity - backpack.fullness() >= containerTypes[containerIndex].size) {
			Container cont = new Container(containerTypes[containerIndex]);
			backpack.containers.Add (cont);
			return backpack.containers.IndexOf(cont);
		}
		return -1;
	}
	public int addContainer(int containerIndex, float damage, float value) {
		if(backpack.capacity - backpack.fullness() >= containerTypes[containerIndex].size) {
			Container cont = new Container(containerTypes[containerIndex]);
			cont.damage = damage;
			cont.value = value;
			backpack.containers.Add (cont);
			return backpack.containers.IndexOf(cont);
		}
		return -1;
	}
	public void addItemToContainer(int contIndex, int itemIndex, float newDamage, float value) {
		Item item = new Item (itemTypes [itemIndex]);
		item.damage = newDamage;
		item.value = value;
		backpack.containers[contIndex].contents.Add(item);

	}

	public Backpack backpack = new Backpack();

	void Start() {
		if (GetComponent<NetworkView>().isMine) {
			addContainer (2);
		}
	}



}