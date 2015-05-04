using UnityEngine;
using System.Collections;

public class destroyAfter : MonoBehaviour {
	public float time;
	float curTime = 0;
	// Update is called once per frame
	void Update () {
		if(GetComponent<NetworkView>().isMine){
			curTime += Time.deltaTime;
			if (curTime > time) {
				Network.Destroy(this.gameObject);
			}
		}
	}
}
