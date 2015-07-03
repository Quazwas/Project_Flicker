using UnityEngine;
using System.Collections;

public class weaponObject : MonoBehaviour {

	// Use this for initialization
	[RPC]
	void parentize (NetworkViewID nViewID, NetworkViewID nViewID2) {
		NetworkView view = NetworkView.Find(nViewID2);
		transform.parent = view.gameObject.transform;
	}

}
