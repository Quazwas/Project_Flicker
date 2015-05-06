using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public class Item {
		public int size;
		public string name;
		public float damage;
		public int type;

		public Item(ItemType itemType) {
			name = itemType.name;
			damage = 100f;
			size = itemType.size;
		}
		public Item(ItemType itemType, float newDamage) {
			name = itemType.name;
			damage = newDamage;
			size = itemType.size;
		}
	}
	public class ItemType {
		public int size;
		public string name;
		public ItemType(string newName, int newSize) {
			name = newName;
			size = newSize;
		}
	}
	List<ItemType> itemTypes = new List<ItemType>() {
		new ItemType("Matchbox", 1),						//0
		new ItemType("String", 1),       					// 1
		new ItemType("Grenade", 1),                       //2
		new ItemType("Companion Cube", 5),		//3
		new ItemType("Stop Sign", 10),					//4
		new ItemType("Pavement", 10)					//5
	};
	public bool addItemToBackpack(Item newItem) {
		if(backpack.capacity - backpack.fullness() >= newItem.size) {
			backpack.items.Add (newItem);
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
	public class Container {
		public bool isExpanded = false;
		public int capacity;
		public float damage;
		public int type;
		public int size;
		public string name;
		public List<Item> contents = new List<Item>();

		public Container(ContainerType contType) {
			name = contType.name;
			capacity = contType.capacity;
			damage = 100f;
			size = contType.size;
		}
		public Container(ContainerType contType, int newDamage) {
			name = contType.name;
			capacity = contType.capacity;
			damage = newDamage;
			size = contType.size;
		}
		public int fullness() {
			int f = 0;
			foreach (Item item in contents) {
				f+=item.size;
			}
			return f;
		}

	}
	public bool addContainer(Container newCont) {
		if(backpack.capacity - backpack.fullness() >= newCont.size) {
			backpack.containers.Add (newCont);
			return true;
		}
		return false;
	}
	public bool addContainer(int containerIndex) {
		if(backpack.capacity - backpack.fullness() >= containerTypes[containerIndex].size) {
			backpack.containers.Add (new Container(containerTypes[containerIndex]));
			return true;
		}
		return false;
	}
	public class ContainerType {
		public int capacity;
		public int type;
		public string name;
		public int size;
		public ContainerType(string newName, int newCap, int newSize) {
			name = newName;
			capacity = newCap;
			size = newSize;
		}
		public ContainerType(string newName, int newCap, int newSize, int newType) {
			name = newName;
			capacity = newCap;
			type = newType;
			size = newSize;
		}
	}
	List<ContainerType> containerTypes = new List<ContainerType>() {
		new ContainerType("Small Pouch", 2, 1),
		new ContainerType("Large Pouch", 3,1)
	};
	public class Backpack {
		public int capacity = 20;
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

	Backpack backpack = new Backpack();



	int UIheight = 30;
	int UIGap = 5;
	int UIWIdth = 150;
	int UIOffset = 30;
	int UIx = 100;
	int UIy = 100;

	[SerializeField]
	GUISkin menuSkin;

	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.Button (new Rect (UIx - 10, UIy - 10, UIWIdth + 20, UIheight), "Backpack  ~  " + backpack.fullness() + "/" + backpack.capacity.ToString());
		int i = 1;
		foreach (Container container in backpack.containers) {
			if(GUI.Button(new Rect(UIx,UIy+i*(UIheight+UIGap),UIWIdth,UIheight), container.name + "  ~  "+container.fullness() + "/" + container.capacity.ToString())) {
				container.isExpanded = !container.isExpanded;
			}
			if (container.isExpanded) {
				foreach (Item item in container.contents) {
					GUI.Button(new Rect(UIx+UIOffset,UIy+i*(UIheight+UIGap),UIWIdth-UIOffset,UIheight), item.name);
					i++;
				}
			}
			i++;
		}
		foreach (Item item in backpack.items) {
			GUI.Button(new Rect(UIx,UIy+i*(UIheight+UIGap),UIWIdth,UIheight), item.name);
			i++;
		}
	}

}