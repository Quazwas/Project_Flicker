using UnityEngine;
using System.Collections;

public class killSwitch : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
				if(!player.GetComponent<NetworkView>().isMine) {
					player.GetComponent<takeDamage>().health = 0;
				}
			}
			foreach(GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie")) {
				zombie.GetComponent<takeDamage>().health = 0;
			}
		}
	}
}
