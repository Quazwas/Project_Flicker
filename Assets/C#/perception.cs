using UnityEngine;
using System.Collections;

public class perception : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	GameObject closest;
	void Update () {
		float highDist = 100000f;
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
			if(dist < highDist) {
				highDist = dist;
				closest = player;
			}
		}
		GetComponent<AstarAI> ().targetObject = closest;
		GetComponent<AstarAI> ().hasTarget = true;
	}
}
