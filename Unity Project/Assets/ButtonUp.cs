using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUp : MonoBehaviour {
	public PlayerControllerKeyTap player;

	void OnTouchDown() {
		
	}

	void OnTouchUp() {

	}

	void OnTouchStay() {
		player.MovePlayerUp ();
	}

	void OnTouchExit() {

	}
}
