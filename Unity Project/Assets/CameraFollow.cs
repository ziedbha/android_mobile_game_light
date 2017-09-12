using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;

	private Vector3 velocity = Vector3.zero;
	private float offset;
	private float zDistance;
	private float xDistance;

	void Start() {
		offset = transform.position.y - target.transform.position.y;
		zDistance = transform.position.z;
		xDistance = transform.position.x;
	}

	void Update () {
		Vector3 goalPos = new Vector3 (xDistance, offset + target.position.y, zDistance);
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
	}
}