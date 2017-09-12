using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKeyTap : MonoBehaviour {
	public float speed = 5f;
	public float tileMultip = 0.5f;
	public LayerMask obstacleMask;
	public Boundary screenBounds;

	Vector3 targetPos;
	Vector3 currentPos;
	bool reachedPos = true;
	string faceDir = "";

	void Start() {
		targetPos = transform.position;
		faceDir = "u";
	}
		

	// Update is called once per frame
	void FixedUpdate () {
		currentPos = transform.position;

		if (transform.position == targetPos) {
			reachedPos = true;
		}
		targetPos.x = Mathf.Clamp (targetPos.x, screenBounds.xMin, screenBounds.xMax);
		targetPos.y = Mathf.Clamp (targetPos.y, screenBounds.yMin, screenBounds.yMax);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
		RotatePlayer ();
	}

	bool RaycastCheck(Vector3 direction) {
		float rayLength = 0.01f + tileMultip;
		Vector3 rayOrigin = transform.position;
		Debug.DrawLine (rayOrigin, rayOrigin + direction * (rayLength));
		if (Physics.Raycast (rayOrigin, direction, rayLength, obstacleMask)) {
			return true;
		} else {
			return false;
		}
	}

	void RotatePlayer() {
		switch (faceDir) {
		case "u":
			transform.eulerAngles = new Vector3 (0, 0, 0);
			break;
		case "d":
			transform.eulerAngles = new Vector3 (0, 0, 180);
			break;
		case "l":
			transform.eulerAngles = new Vector3 (0, 0, 90);
			break;
		case "r":
			transform.eulerAngles = new Vector3 (0, 0, -90);
			break;
		default:
			break;
		}
	}

	public void MovePlayerLeft() {
		if (!RaycastCheck (Vector3.left) && reachedPos) {
			targetPos += Vector3.left * tileMultip;
			reachedPos = false;
		}
		faceDir = "l";	
	}

	public void MovePlayerRight() {
		if (!RaycastCheck (Vector3.right) && reachedPos) {
			targetPos += Vector3.right * tileMultip;
			reachedPos = false;
		}
		faceDir = "r";
	}

	public void MovePlayerUp() {
		if (!RaycastCheck (Vector3.up) && reachedPos) {
			targetPos += Vector3.up * tileMultip;
			reachedPos = false;
		}
		faceDir = "u";
	}

	public void MovePlayerDown() {
		if (!RaycastCheck (Vector3.down) && reachedPos) {
			targetPos += Vector3.down * tileMultip;
			reachedPos = false;
		}
		faceDir = "d";
	}


}
