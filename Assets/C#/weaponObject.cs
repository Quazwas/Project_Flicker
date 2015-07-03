using UnityEngine;
using System.Collections;

public class weaponObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] wNulls = GameObject.FindGameObjectsWithTag("weaponNull");
		foreach (GameObject n in wNulls) {
			print ("yolo");
			if (n.transform.parent.parent.gameObject.GetComponent<NetworkView>().isMine) {
				transform.parent = n.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
