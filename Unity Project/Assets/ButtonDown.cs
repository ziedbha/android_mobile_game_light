using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour {
	public PlayerControllerKeyTap player;

	void OnTouchDown() {		
	}

	void OnTouchUp() {
	}

	void OnTouchStay() {
		player.MovePlayerDown ();
		
	}

	void OnTouchExit() {

	}
}
