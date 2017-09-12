using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMouse : RaycastController {
	public float speed = 5f;
	public Boundary screenBounds;
	public LayerMask obstacleMask;
	public Transform head;

	Vector3 targetPos;
	Vector3 currentPos;
	Vector3 velocity;
	bool reached = true;
	float angleZ = 0.0f;
	float offsetMagnitude;
	RaycastHit2D hit;
	Rigidbody2D rb;

	void Awake() {
		base.Awake ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Start() {
		base.Start ();
		targetPos = transform.position;
	}

	void Update() {
		GetBoxCorners ();
		if (Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			hit = Physics2D.Raycast (ray.origin, ray.direction, 10000);
			if (hit && (Vector2) transform.position != hit.point) {
				Vector3 mousePos = Input.mousePosition;
				Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
				angleZ = Mathf.Atan2 ((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg - 90;
				targetPos = new Vector3 (hit.point.x, hit.point.y, transform.position.z);

				reached = false;
			}
		}  

	}



	// Update is called once per frame
	void FixedUpdate () {
		currentPos = transform.position;
		if (currentPos.Equals(targetPos)) {
			print ("reached");
			reached = true;
		}
		targetPos.x = Mathf.Clamp (targetPos.x, screenBounds.xMin, screenBounds.xMax);
		targetPos.y = Mathf.Clamp (targetPos.y, screenBounds.yMin, screenBounds.yMax);

		Vector3 direction = targetPos - currentPos;
		Debug.Log (direction);
		if (direction.magnitude < 0.1f) {
			rb.velocity = Vector2.zero;
			reached = true;
		} else if (!reached) {
			velocity = direction.normalized * speed;
			rb.velocity = velocity;
		}


		/*Vector3 movement = targetPos - currentPos;
		Vector3 worldPos = transform.TransformPoint (collider2d.offset);
		Vector3 rayOrigin = (Vector3) raycastOrigins.topLeft;
		rayOrigin = RotatePointAroundPivot(rayOrigin, Vector3.zero, collider2d.transform.eulerAngles);
		rayOrigin = rayOrigin + worldPos;
		float rayLength = Mathf.Infinity;
		Debug.DrawRay (rayOrigin, Vector2.up, Color.red);

		if (!solved) { 
			RaycastHit2D hitObs = Physics2D.Raycast (rayOrigin, movement, rayLength, obstacleMask);
			if (hitObs) {
				rayLength = hitObs.distance - skinWidth;
				targetPos = (Vector3) hitObs.point + new Vector3(0,0, targetPos.z);
				Vector3 directionMove = movement;
				Vector3 offset = head.transform.position - currentPos;
				offsetMagnitude = offset.magnitude;
				directionMove.Normalize ();
				directionMove = directionMove * offsetMagnitude;
				Debug.Log (directionMove);
				targetPos = targetPos + offset;
				Debug.DrawLine (head.transform.position, targetPos, Color.blue);
				solved = true;
			}
		} */


		transform.eulerAngles = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 
			angleZ);
		//if (!reached) { 
			//transform.position = Vector3.MoveTowards(currentPos, targetPos, Time.deltaTime * speed);
			//rb.AddRelativeForce (Vector3.forward * speed, ForceMode2D.Force);
		//}




	}
		
	//void OnCollisionEnter2D(Collision2D other) {
		//reached = true;
		//rb.velocity = Vector2.zero;
	//}

}
