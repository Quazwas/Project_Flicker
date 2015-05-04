using UnityEngine;
using System.Collections;

public class takeDamage : MonoBehaviour {
	public float health = 100f;
	bool isPlayer = false;
	void Start() {
		if (gameObject.tag == "Player") {
			isPlayer = true;
		}
	}
	[RPC]
	void bullet(NetworkViewID viewID, float damage) {
		if (GetComponent<NetworkView>().isMine) {
			health-=damage;
			if (health<=0) {
				health = 0;
				if(gameObject.tag=="Player") {
					GameObject.Find ("Operator").GetComponent<NetworkManager>().SpawnPlayer();
				}
				Network.Destroy(this.gameObject);
			}
			if(transform.position.y < -10) {
				health = 0;
			}
		}
	}
	void OnGUI() {
		if(GetComponent<NetworkView>().isMine && isPlayer) {
			GUI.Label (new Rect (10, 10, 100, 100), "HP:  "+health.ToString());
		}
	}
}
