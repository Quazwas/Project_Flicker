using UnityEngine;
using System.Collections;

public class spawnZombie : MonoBehaviour {

	[SerializeField]
	GameObject zombie;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			Network.Instantiate (zombie, new Vector3 (Random.Range (-45f, 45f), 2, Random.Range (-45f, 45f)), transform.rotation, 0);
		}
	}
}
