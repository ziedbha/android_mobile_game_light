using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {
	public LayerMask touchInputMask;
	RaycastHit hit;
	List<GameObject> touchList = new List<GameObject> ();
	GameObject[] touchesOld;

	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) {
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast(ray, out hit, touchInputMask)) {
					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);
				if (Input.GetMouseButtonDown(0)) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}

				if (Input.GetMouseButtonUp(0)) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}

				if (Input.GetMouseButton(0)) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}

			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains(g)) {
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}

		}
#endif
		if (Input.touchCount > 0) {
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();
			foreach (Touch t in Input.touches) {
				Ray ray = Camera.main.ScreenPointToRay (t.position);


				if (Physics.Raycast(ray, out hit, touchInputMask)) {
					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);
					if (t.phase == TouchPhase.Began) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}

					if (t.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}

					if (t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Moved) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}

					if (t.phase == TouchPhase.Canceled) {
						recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}

				}
			}
			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains(g)) {
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}

		
	}
}
