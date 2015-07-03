using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	Vector3 hinge;
	[SerializeField]
	float offset;
	[SerializeField]
	float speed;
	[SerializeField]
	float tot;
	// Use this for initialization
	void Start () {
		  hinge = this.transform.position + (this.transform.right * offset);
	}
	float tots = 0;
	bool dir = false;
	// Update is called once per frame
	void Update () {
		if(dir) {
			if(tots < tot){
				transform.RotateAround(hinge, Vector3.up, speed * Time.deltaTime);
				tots += speed * Time.deltaTime;
			} else {
				dir = false;
			}
		} else {
			if(tots > 0){
				transform.RotateAround(hinge, Vector3.up, -speed * Time.deltaTime);
				tots -= speed * Time.deltaTime;
			} else {
				dir = true;
			}
		}
	}
}
