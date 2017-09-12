using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKeyHold : MonoBehaviour {
	public float speed = 5f;
	public float tileMultip = 0.5f;
	public LayerMask obstacleMask;
	public Boundary screenBounds;

	Vector3 targetPos;
	Vector3 currentPos;
	ArrayList directionalKeys = new ArrayList();
	KeyCode currentKey;
	bool reachedPos = true;
	string faceDir = "";
	string prevFaceDir = "";
	string inputThisFrame = "";
	bool pressed = false;
	string pressedKey = "";

	void Start() {
		targetPos = transform.position;
		faceDir = "u";
		directionalKeys.Add (KeyCode.W);
		directionalKeys.Add (KeyCode.A);
		directionalKeys.Add (KeyCode.S);
		directionalKeys.Add (KeyCode.D);
	}

	void Update() {
		inputThisFrame = "";
		Debug.Log (Input.inputString);
		if (Input.inputString != "") {
			char[] input = Input.inputString.ToCharArray();
			inputThisFrame = input [input.Length - 1].ToString();
			pressedKey = inputThisFrame;
		}


		/*foreach (KeyCode k in directionalKeys) {
			if (Input.GetKey(k)) {
				switch (k) {
				case KeyCode.W:
					if (pressedKey != "W" && inputThisFrame == "") {
						inputThisFrame = "W";
					} else if (pressedKey == "W"){
						inputThisFrame = "W";
					}
					break;
				case KeyCode.A:
					if (pressedKey != "A" && inputThisFrame == "") {
						inputThisFrame = "A";
					}
					break;
				case KeyCode.S:
					if (pressedKey != "S" && inputThisFrame == "") {
						inputThisFrame = "S";
					}
					break;
				case KeyCode.D:
					if (pressedKey != "D" && inputThisFrame == "") {
						inputThisFrame = "D";
					}
					break;
				default:
					break;	
				}
			}
		}*/
	}

	// Update is called once per frame
	void FixedUpdate () {
		currentPos = transform.position;
		prevFaceDir = faceDir;

		if (transform.position == targetPos) {
			reachedPos = true;
		}
			
		if(inputThisFrame == "w" && reachedPos) {
			MovePlayerUp ();
		}

		if(inputThisFrame == "a" && reachedPos) {
			MovePlayerLeft ();
		}

		if(inputThisFrame == "s" && reachedPos) {
			MovePlayerDown ();
		}

		if(inputThisFrame == "d" && reachedPos) {
			MovePlayerRight ();
		}
		currentKey = KeyCode.None;

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
			if (prevFaceDir != faceDir) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
			}
			break;
		case "d":
			if (prevFaceDir != faceDir) {
				transform.eulerAngles = new Vector3 (0, 0, 180);
			}
			break;
		case "l":
			if (prevFaceDir != faceDir) {
				transform.eulerAngles = new Vector3 (0, 0, 90);
			}
			break;
		case "r":
			if (prevFaceDir != faceDir) {
				transform.eulerAngles = new Vector3 (0, 0, -90);
			}
			break;
		default:
			break;
		}
	}

	public void MovePlayerLeft() {
		if (!RaycastCheck (Vector3.left)) {
			targetPos += Vector3.left * tileMultip;
			reachedPos = false;
		}
		faceDir = "l";	
	}

	public void MovePlayerRight() {
		if (!RaycastCheck (Vector3.right)) {
			targetPos += Vector3.right * tileMultip;
			reachedPos = false;
		}
		faceDir = "r";
	}

	public void MovePlayerUp() {
		if (!RaycastCheck (Vector3.up)) {
			targetPos += Vector3.up * tileMultip;
			reachedPos = false;
		}
		faceDir = "u";
	}

	public void MovePlayerDown() {
		if (!RaycastCheck (Vector3.down)) {
			targetPos += Vector3.down * tileMultip;
			reachedPos = false;
		}
		faceDir = "d";
	}


}
