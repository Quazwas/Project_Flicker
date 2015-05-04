using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour {
	public GameObject targetObject;
	public Vector3 targetPosition;
	Seeker seeker;
	private CharacterController controller;
	public Path path;
	public float speed = 4000f;
	public float nextWaypointDistance = 3f;
	private int currentWaypoint = 0;
	public bool hasTarget = false;
	public void Start() {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

	}
	bool pathComplete=true;
	public void OnPathComplete (Path p) {
		print ("Path returned. Error?: "+p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
			pathComplete = true;
		}
	}
	float passTime = 0f;
	Vector3 nextPoint;
	bool pathPass = false;
	float curTime = 0f;
	void Update() {
		if (targetObject != null) {
			if(Vector3.Distance (targetObject.transform.position, transform.position) >3.2f) {
				passTime += Time.deltaTime;
				targetPosition = targetObject.transform.position;
				transform.LookAt(targetPosition);
				if (passTime >= 0.4f && pathComplete) {
					seeker.StartPath (transform.position, targetPosition, OnPathComplete);
					passTime = 0f;
					pathComplete = false;
				}
			} else {
				if(curTime > 1f) {
					curTime = Time.deltaTime;
					attack();
				} else {
					curTime+=Time.deltaTime;
				}
			}
		}
	}
	public void FixedUpdate() {
		pathPass = false;

		if (path == null) {
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			print("End of path reached");
			return;
		}
		pathPass = true;
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.SimpleMove (dir);
		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
	void attack() {
		NetworkViewID viewID = Network.AllocateViewID();
		targetObject.GetComponent<NetworkView>().RPC("bullet", RPCMode.AllBuffered, viewID, 10.0f);
	}
}
