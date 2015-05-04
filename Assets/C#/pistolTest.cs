using UnityEngine;
using System.Collections;

public class pistolTest : MonoBehaviour {
	[SerializeField]
	public GameObject hitObject;	
	[SerializeField]
	GameObject muzzleFlash;
	[SerializeField]
	Transform flashPoint;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			shoot();
		}
	}
	void shoot() {
		RaycastHit hit;
		if(Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, 100f)) {
			Debug.DrawLine(transform.position, hit.point);
			Network.Instantiate(hitObject, hit.point, Quaternion.identity,0);
			if(hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "Zombie") {
				if(hit.transform.gameObject.GetComponent<NetworkView>() as NetworkView != null) {
					NetworkViewID viewID = Network.AllocateViewID();
					hit.transform.gameObject.GetComponent<NetworkView>().RPC("bullet", RPCMode.AllBuffered, viewID, 10.0f);
				}
			}
		}
		Network.Instantiate (muzzleFlash, flashPoint.position, flashPoint.rotation, 0);
	}
}
