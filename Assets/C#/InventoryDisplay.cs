using UnityEngine;
using System.Collections;

public class InventoryDisplay : MonoBehaviour {
	Inventory inv;
	Inventory.Backpack backpack;
	void Start() {
		inv = GetComponent<Inventory>();
		backpack = inv.backpack;
	}
	int UIheight = 50;
	int UIGap = 5;
	int UIWIdth = 200;
	int UIOffset = 30;
	int UIx = 100;
	int UIy = 100;
	
	[SerializeField]
	GUISkin menuSkin;
	Inventory.Container selectedContainer;
	Inventory.Item selectedItem;
	bool itemSelected;
	bool containerSelected;
	void OnGUI() {
		GUI.skin = menuSkin;
		newBackpack(backpack);

		int i = 1;
		foreach (Inventory.Container container in backpack.containers) {
			newContainer(i, container);
			i++;
			if (container.isExpanded) {
				foreach (Inventory.Item item in container.contents) {
					if(newSubItem (i, item)) {
						if(!itemSelected && !containerSelected) {
							selectedItem = item;
							itemSelected = true;
							container.contents.Remove(item);
						}
					}
					i++;
				}
			}
		}
		foreach (Inventory.Item item in backpack.items) {
			if(newItem (i, item)) {
				if(!itemSelected && !containerSelected) {
					selectedItem = item;
					itemSelected = true;
					backpack.items.Remove(item);
				}
			}
			i++;
		}
		if (itemSelected) {
			float mouseY = Screen.height - Input.mousePosition.y;
			GUI.Button (new Rect (UIx+UIOffset*3, mouseY-25, 50, 50), "[IMG]");
			GUI.Button (new Rect (UIx+UIOffset*3+50, mouseY-25, UIWIdth, UIheight), selectedItem.name);
			if(Input.GetButtonUp ("Fire1")) {
				float index = (mouseY - UIy)/(UIheight+UIGap) ;
				index = (int)Mathf.Round(index);


				if(index > i-1) {
					backpack.items.Add (selectedItem);
					itemSelected = false;
				} else if(index < 1) {
					backpack.items.Insert (0, selectedItem);
					itemSelected = false;
				} else {
					int x  = 1;
					bool hasPassed = false;
					foreach(Inventory.Container cont in backpack.containers) {
						if(!hasPassed) {
							x++;
							if(i==x) {
								hasPassed = true;
								cont.contents.Insert(0, selectedItem);
								itemSelected = false;
							}
							if(cont.isExpanded) {
								x++;
								if(i==x) {
									hasPassed = true;
									cont.contents.Insert(0, selectedItem);
									itemSelected = false;
								}
								foreach(Inventory.Item it in cont.contents) {
									if(!hasPassed) {
										x++;
										if(i==x) {
											hasPassed = true;
											cont.contents.Insert(cont.contents.IndexOf(it), selectedItem);
											itemSelected = false;
										}
									}
								}
							}
						}
					}
					if(!hasPassed) {
						foreach(Inventory.Item it in backpack.items) {
							if(!hasPassed) {
								x++;
								if(i==x) {
									hasPassed = true;
									backpack.items.Insert(backpack.items.IndexOf(it), selectedItem);
									itemSelected = false;
								}
							}
						}
					}
				}
			}
		}
	}

	void newBackpack(Inventory.Backpack backpack) {
		GUI.Button (new Rect (UIx, UIy, 50, 50), "[IMG]");
		GUI.Button (new Rect (UIx+50, UIy, 220, 50), "Backpack  ~  " + backpack.fullness() + "/" + backpack.capacity.ToString());
	}



	bool newContainer(int i, Inventory.Container container) {
		bool pressed = false;
		if(GUI.Button (new Rect (UIx+UIOffset, UIy+i*(UIheight+UIGap), 50, 50), "[IMG]")) {
			container.isExpanded = !container.isExpanded;
			pressed =  true;
		}
		if(GUI.Button(new Rect(UIx+UIOffset+50,UIy+i*(UIheight+UIGap),UIWIdth,UIheight), container.name + "  ~  "+container.fullness() + "/" + container.capacity.ToString())) {
			container.isExpanded = !container.isExpanded;
			pressed =  true;
		}
		return pressed;
	}

	bool newItem(int i, Inventory.Item item) {
		bool pressed = false;
		if (GUI.Button (new Rect (UIx+UIOffset, UIy+i*(UIheight+UIGap), 50, 50), "[IMG]")) {
			pressed = true;
		}
		if (GUI.Button (new Rect (UIx+UIOffset+50, UIy + i * (UIheight + UIGap), UIWIdth, UIheight), item.name)) {
			pressed = true;
		}
		return pressed;
	}

	bool newSubItem(int i, Inventory.Item item) {
		bool pressed = false;
		if (GUI.Button (new Rect (UIx+UIOffset*2, UIy+i*(UIheight+UIGap), 50, 50), "[IMG]")) {
			pressed = true;
		}
		if(GUI.Button(new Rect(UIx+UIOffset*2+50,UIy+i*(UIheight+UIGap),UIWIdth,UIheight), item.name)) {
			int index = backpack.items.IndexOf(item);
			pressed = true;
		}
		return pressed;
	}

}
