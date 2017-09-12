using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : MonoBehaviour {
	public PlayerControllerKeyTap player;

	void OnTouchDown() {
	}

	void OnTouchUp() {
	}

	void OnTouchStay() {
		player.MovePlayerLeft ();
		
	}

	void OnTouchExit() {

	}
}
