using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : MonoBehaviour {
	public PlayerControllerKeyTap player;

	void OnTouchDown() {
		
	}

	void OnTouchUp() {

	}

	void OnTouchStay() {
		player.MovePlayerRight ();
		
	}

	void OnTouchExit() {

	}
}
