using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemDetails : MonoBehaviour {
	public int itemIndex = 0;
	public bool isContainer = false;
	public bool isBackpack = false;
	public float damage = 100f;
	public float value = 10;
	public List<int> subItemIndex = new List<int>();
	public List<float> subItemDamage = new List<float>();
	public List<float>subItemValue = new List<float>();

	public void initializeOthers() {
		NetworkView nView = GetComponent<NetworkView> ();
		NetworkViewID viewID = Network.AllocateViewID();
		nView.RPC ("initOthersCalled", RPCMode.OthersBuffered, viewID, itemIndex, isContainer, isBackpack, damage, value);
		for (int i = 0; i < subItemIndex.Count; i++) {
			nView.RPC ("addItem", RPCMode.OthersBuffered, viewID, subItemIndex [i], subItemDamage [i], subItemValue[i]);
		}
	}

	[RPC]
	void initOthersCalled(NetworkViewID vID, int index, bool isCont, bool isBack, float newDamage, float newValue) {
		itemIndex = index;
		isContainer = isCont;
		isBackpack = isBack;
		damage = newDamage;
		value = newValue;
	}
	[RPC]
	void addItem(NetworkViewID vID, int index, float newDamage, float newValue) {
		subItemIndex.Add (index);
		subItemDamage.Add(newDamage);
	}

}
