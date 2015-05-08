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

		public Item(ItemType itemType) {
			name = itemType.name;
			damage = 100f;
			size = itemType.size;
			id = itemType.index;
		}
		public Item(ItemType itemType, float newDamage) {
			name = itemType.name;
			damage = newDamage;
			size = itemType.size;
			id = itemType.index;
		}
	}
	public class ItemType {
		public int size;
		public string name;
		public int index;
		public int prefabIndex;
		public ItemType(int newIndex, string newName, int newSize) {
			index = newIndex;
			name = newName;
			size = newSize;
		}
		public ItemType(int newIndex, string newName, int newSize, int newPrefabIndex) {
			index = newIndex;
			name = newName;
			size = newSize;
			prefabIndex = newPrefabIndex;
		}
	}
	List<ItemType> itemTypes = new List<ItemType>() {
		new ItemType(0, "Matchbox", 1, 0),						//0
		new ItemType(1, "String", 1, 1),       					// 1
		new ItemType(2, "Grenade", 1),                       //2
		new ItemType(3, "Companion Cube", 5),		//3
		new ItemType(4, "Stop Sign", 10)				
	};

	public class Container {
		public bool isExpanded = false;
		public int capacity;
		public float damage;
		public int type;
		public int size;
		public string name;
		public int id;
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
	List<ContainerType> containerTypes = new List<ContainerType>() {
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
	public bool addItemToBackpack(int itemIndex, float damage) {
		if(backpack.capacity - backpack.fullness() >= itemTypes[itemIndex].size) {
			Item i = new Item(itemTypes[itemIndex]);
			i.damage = damage;
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
	public int addContainer(int containerIndex, float damage) {
		if(backpack.capacity - backpack.fullness() >= containerTypes[containerIndex].size) {
			Container cont = new Container(containerTypes[containerIndex]);
			cont.damage = damage;
			backpack.containers.Add (cont);
			return backpack.containers.IndexOf(cont);
		}
		return -1;
	}
	public void addItemToContainer(int contIndex, int itemIndex, float newDamage) {
		Item item = new Item (itemTypes [itemIndex]);
		item.damage = newDamage;
		backpack.containers[contIndex].contents.Add(item);

	}

	[SerializeField]
	GameObject iPrefab;
	void drop(Item item) {
		GameObject o;
		Vector3 itemSpawn;
		RaycastHit hit;
		Physics.Raycast(transform.position,Vector3.down,out hit);
		itemSpawn = hit.point;

		o = Network.Instantiate(itemPrefabs[itemTypes[item.id].prefabIndex],itemSpawn,Quaternion.identity,0) as GameObject;
		o.GetComponent<itemDetails>().itemIndex = item.id;
		o.GetComponent<itemDetails>().damage = item.damage;
		o.GetComponent<itemDetails>().isContainer = false;
		o.GetComponent<itemDetails> ().initializeOthers ();
		backpack.items.Remove(item);
	}
	
	void drop(Container container) {
		GameObject o;
		Vector3 itemSpawn;
		RaycastHit hit;
		Physics.Raycast(transform.position,Vector3.down,out hit);
		itemSpawn = hit.point;

		o = Network.Instantiate(itemPrefabs[itemTypes[container.id].prefabIndex],itemSpawn,Quaternion.identity,0) as GameObject;
		itemDetails details = o.GetComponent<itemDetails> ();
		details.itemIndex = container.id;
		details.damage = container.damage;
		details.isContainer = true;
		foreach (Item item in container.contents) {
			details.subItemIndex.Add(item.id);
			details.subItemDamage.Add(item.damage);
		}
		details.initializeOthers ();
		backpack.containers.Remove(container);
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Q)) {
			if(backpack.items.Count > 0) {
				drop(backpack.items[0]);
			}
		}
		if(Input.GetKeyDown(KeyCode.X)) {
			if(backpack.containers.Count > 0) {
				drop(backpack.containers[0]);
			}
		}
	}




	public Backpack backpack = new Backpack();

	void Start() {
		if (GetComponent<NetworkView>().isMine) {
			addContainer (2);
			addItemToContainer (0, 1, 10);
			addItemToContainer (0, 2, 20);
			addItemToContainer (0, 3, 63);
			addItemToContainer (0, 4, 71);
		}
	}



}