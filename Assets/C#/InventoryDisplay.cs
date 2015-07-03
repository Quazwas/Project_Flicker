using UnityEngine;
using System.Collections;

public class InventoryDisplay : MonoBehaviour {

	public bool inventoryOpen;

	Inventory inv;
	Inventory.Backpack backpack;
	InventoryUse invUse;

	public Texture2D[] icons;

	void Start() {
		inv = GetComponent<Inventory>();
		invUse = GetComponent<InventoryUse> ();
		backpack = inv.backpack;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.I)) {
			inventoryOpen = !inventoryOpen;
		}
	}

	int UIheight = 50;
	int UIGap = 5;
	int UIWIdth = 250;
	int UIOffset = 30;
	int UIx = 100;
	int UIy = 100;
	float index = 0;
	float extraY = 10;
	[SerializeField]
	GUISkin menuSkin;
	Inventory.Container selectedContainer;
	Inventory.Item selectedItem;
	bool itemSelected;
	bool containerSelected;
	bool overBackpack;


	void OnGUI() {
		if (inventoryOpen) {
			float mouseY = Screen.height - Input.mousePosition.y;
			float mouseX = Input.mousePosition.x;
			index = (mouseY - UIy) / (UIheight + UIGap);
			index = (int)Mathf.Round (index);
			extraY = 0;

			GUI.skin = menuSkin;
			newBackpack (backpack);

			if(mouseX <  650) {
				overBackpack = true;
			} else {
				overBackpack = false;
			}



			int i = displayBackpack();

			if (itemSelected) {


				GUI.Button (new Rect (mouseX-2, mouseY - 25, UIWIdth, UIheight), selectedItem.name);
				GUI.DrawTexture(new Rect(mouseX - 2, mouseY - 25, 50, 50), icons[selectedItem.iconIndex], ScaleMode.StretchToFill, true, 10.0F);

				if (Input.GetButtonUp ("Fire2")) {
					if(overBackpack){
						placeInBackpack(i);
					} else {
						drop (selectedItem);
						itemSelected = false;
					}
				}
			}
		}
	}

	int check;
	int displayBackpack() {
		int i = 1;
		foreach (Inventory.Container container in backpack.containers) {
			newContainer (i, container);
			i++;
			if (container.isExpanded) {
				foreach (Inventory.Item item in container.contents) {
					if (itemSelected && index == i && overBackpack) {
						seperator (i, 2);
						checkPlace (item, container);
					}
					check = newSubItem (i, item);
					if (check == 1) {
						if (!itemSelected && !containerSelected) {
							selectedItem = item;
							itemSelected = true;
							container.contents.Remove(item);
						}
					} else if(check == 0) {
						if (!itemSelected && !containerSelected) {
							if(invUse.useItem(item)) {
								container.contents.Remove(item);
							}
						}
					}
					i++;
				}
				if (itemSelected && index == i && overBackpack) {
					seperator (i, 2);
					checkPlace (container);
				}
			}
		}
		if (itemSelected && index <= 1 && overBackpack) {
			seperator (i, 1);
		}
		foreach (Inventory.Item item in backpack.items) {
			if (itemSelected && index == i && overBackpack) {
				seperator (i, 1);
				checkPlace (item);
			}
			check = newItem (i, item);
			if (check == 1) {
				if (!itemSelected && !containerSelected) {
					selectedItem = item;
					itemSelected = true;
					backpack.items.Remove(item);
				}
			} else if(check == 0) {
				if (!itemSelected && !containerSelected) {
					if(invUse.useItem(item)) {
						backpack.items.Remove(item);
					}
				}
			}
			i++;
		}
		if (itemSelected && index > i - 1 && overBackpack) {
			seperator (i, 1);
		}
		return i;
	}

	void placeInBackpack(int i) {
		if (index > i - 1) {
			backpack.items.Add (selectedItem);
			itemSelected = false;
			return;
		} else if (index < 1) {
			backpack.items.Insert (0, selectedItem);
			itemSelected = false;
			return;
		} else {
			int x = 1;
			bool hasPassed = false;

			foreach (Inventory.Container cont in backpack.containers) {
				if (!hasPassed) {
					x++;
					if (i == x) {
						hasPassed = true;
						cont.contents.Insert (0, selectedItem);
						itemSelected = false;
						return;
					}
					if (cont.isExpanded && cont.contents.Count != 0) {
						x++;
						if (i == x) {
							hasPassed = true;
							cont.contents.Insert (0, selectedItem);
							itemSelected = false;
							return;
						}
						foreach (Inventory.Item it in cont.contents) {
							if (!hasPassed) {
								x++;
								if (i == x) {
									hasPassed = true;
									cont.contents.Insert(cont.contents.IndexOf (it) + 1, selectedItem);

									itemSelected = false;
									return;
								}
							}
						}
					}
				}
			}

		}
	}

	void checkPlace(Inventory.Item item) {
		if (Input.GetButtonUp ("Fire2")) {
				backpack.items.Insert (backpack.items.IndexOf (item), selectedItem);
				itemSelected = false;
		}
	}
	void checkPlace(Inventory.Item item, Inventory.Container cont) {
		if (Input.GetButtonUp ("Fire2")) {
			cont.contents.Insert (cont.contents.IndexOf (item), selectedItem);
			itemSelected = false;
		}
	}
	void checkPlace(Inventory.Container cont) {
		if (Input.GetButtonUp ("Fire2")) {
			cont.contents.Add( selectedItem);
			itemSelected = false;
		}
	}

	void newBackpack(Inventory.Backpack backpack) {
		GUI.Button (new Rect (UIx, UIy, 220, 50), "Backpack  ~  " + backpack.fullness() + "/" + backpack.capacity.ToString());
	}



	bool newContainer(int i, Inventory.Container container) {
		bool pressed = false;
		if(GUI.Button(new Rect(UIx+UIOffset,UIy+i*(UIheight+UIGap)+extraY,UIWIdth,UIheight), container.name + "  ~  "+container.fullness() + "/" + container.capacity.ToString())) {
			container.isExpanded = !container.isExpanded;
			pressed =  true;
		}
		return pressed;
	}

	int newItem(int i, Inventory.Item item) {
		int pressed = -1;
		if (GUI.Button (new Rect (UIx+UIOffset, UIy + i * (UIheight + UIGap)+extraY, UIWIdth, UIheight), item.name + item.value.ToString()+ item.itemClass)) {
			if(pressed == -1) {
				pressed = Event.current.button;
			}
		}
		GUI.DrawTexture(new Rect(UIx+UIOffset, UIy+i*(UIheight+UIGap)+extraY, 50,50), icons[item.iconIndex], ScaleMode.StretchToFill, true, 10.0F);
		return pressed;
	}

	int newSubItem(int i, Inventory.Item item) {
		int pressed = -1;
		if(GUI.Button(new Rect(UIx+UIOffset*2,UIy+i*(UIheight+UIGap)+extraY,UIWIdth,UIheight), item.name)) {
			int index = backpack.items.IndexOf(item);
			if(pressed == -1) {
				pressed = Event.current.button;
			}
		}
		GUI.DrawTexture(new Rect(UIx+UIOffset*2, UIy+i*(UIheight+UIGap)+extraY, 50,50), icons[item.iconIndex], ScaleMode.StretchToFill, true, 10.0F);
		return pressed;
	}

	void seperator(int i, int xOff) {
		GUI.Button(new Rect(UIx+UIOffset*xOff,UIy+i*(UIheight+UIGap)+extraY,UIWIdth,20), "");
		extraY = 25;
	}

	[SerializeField]
	GameObject iPrefab;
	void drop(Inventory.Item item) {
		GameObject o;
		Vector3 itemSpawn;
		RaycastHit hit;
		Physics.Raycast(transform.position,Vector3.down,out hit);
		itemSpawn = hit.point;
		
		o = Network.Instantiate(inv.itemPrefabs[inv.itemTypes[item.id].prefabIndex],itemSpawn,Quaternion.identity,0) as GameObject;
		itemDetails details = o.GetComponent<itemDetails> ();
		details.itemIndex = item.id;
		details.damage = item.damage;
		details.isContainer = false;
		details.value = item.value;
		details.initializeOthers ();
	}
	
	void drop(Inventory.Container container) {
		GameObject o;
		Vector3 itemSpawn;
		RaycastHit hit;
		Physics.Raycast(transform.position,Vector3.down,out hit);
		itemSpawn = hit.point;
		
		o = Network.Instantiate(inv.itemPrefabs[inv.itemTypes[container.id].prefabIndex],itemSpawn,Quaternion.identity,0) as GameObject;
		itemDetails details = o.GetComponent<itemDetails> ();
		details.itemIndex = container.id;
		details.damage = container.damage;
		details.isContainer = true;
		details.value = container.value;
		foreach (Inventory.Item item in container.contents) {
			details.subItemIndex.Add(item.id);
			details.subItemDamage.Add(item.damage);
			details.subItemValue.Add(item.value);
		}
		details.initializeOthers ();
	}


}
