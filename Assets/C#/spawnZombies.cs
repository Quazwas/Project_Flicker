using UnityEngine;
using System.Collections;

public class spawnZombies : MonoBehaviour {
	[SerializeField]
	GameObject zombie;
	public bool spawn;
	void OnServerInitialized()	{
		if (spawn) {
			for (int i = 0; i < 10; i++) {
				Network.Instantiate (zombie, new Vector3 (Random.Range (-45f, 45f), 2, Random.Range (-45f, 45f)), transform.rotation, 0);
			}
		}
	}
}