using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public Transform obj;
	public float moveSpeed;
	float moveTime = 1;

	bool move = false;
	float startTime = 0.0f;
	RaycastHit2D hit;
	Vector3 targetPos;

	void Start() {
		targetPos = obj.transform.position;
	}

	void Update () {
		

	}
}
