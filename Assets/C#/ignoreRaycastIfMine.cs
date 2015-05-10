using UnityEngine;
using System.Collections;

public class ignoreRaycastIfMine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GetComponent<NetworkView>().isMine) {
			gameObject.layer = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
