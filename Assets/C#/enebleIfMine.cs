using UnityEngine;
using System.Collections;

public class enebleIfMine : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Awake () {
		if(target.GetComponent<NetworkView>().isMine) {
			gameObject.active = true;
		} else {
			gameObject.active = false;
		}
	}
}
