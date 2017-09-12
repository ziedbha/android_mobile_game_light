using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour {
	protected const float skinWidth = 0.15f;

	[Header ("Raycast Parameters")]
	public float distBetweenVRays = 0.25f;

	[Header ("Collision Masks")]
	public LayerMask obstacleCollisionMask; //collisions mask specific for obstacles

	internal int raycastVerticalCount = 8;
	internal float verticalRaySpacing;
	internal RaycastOrigins raycastOrigins;
	internal BoxCollider2D collider2d; //collider of object extending this class
	const float distanceBetweenRays = 0.25f;

	public virtual void Awake() {
		collider2d = GetComponent<BoxCollider2D> ();
	}

	public virtual void Start () {
		CalculateRaySpacing ();
	}
		
	internal void CalculateRaySpacing() {
		Bounds bounds = GetBounds ();
		float boundsWidth = bounds.size.x;
		raycastVerticalCount = Mathf.RoundToInt(boundsWidth / distBetweenVRays);
		verticalRaySpacing = bounds.size.x / (raycastVerticalCount - 1);	
	}

	Bounds GetBounds() {
		Bounds bounds = collider2d.bounds;
		bounds.Expand (skinWidth* -2);
		return bounds;
	}

	public void GetBoxCorners() {
		Vector2 size = collider2d.size;
		//Vector2 centerPoint = new Vector2(collider2d.offset.x, collider2d.offset.y);
		Vector3 worldPos = transform.TransformPoint (collider2d.offset);

		float top = (size.y * Mathf.Abs (transform.localScale.y) / 2f) - skinWidth;
		float left = - (size.x * Mathf.Abs(transform.localScale.x) / 2f);

		Vector3 topMid = new Vector2 (0, top);
		raycastOrigins.topLeft = topMid;
	}

	public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
		Vector3 dir = point - pivot; // get point direction relative to pivot
		dir = Quaternion.Euler(angles) * dir; // rotate it
		point = dir + pivot; // calculate rotated point
		return point; // return it
	}

	internal struct RaycastOrigins {
		public Vector2 topLeft;
	}
}
