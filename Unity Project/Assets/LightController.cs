using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	Light mainLight;
	Light playerLight;
	public float decrRate = 1f;

	// Use this for initialization
	void Start () {
		mainLight = GameObject.FindGameObjectWithTag ("MainLight").GetComponent<Light> ();
		playerLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerLight.intensity -= Time.deltaTime * decrRate;
		mainLight.intensity -= Time.deltaTime * decrRate;
	}
}
