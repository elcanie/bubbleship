﻿using UnityEngine;
using System.Collections;

public class Spaceship_script : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	public bu bubble;
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (speed.x * inputX, speed.y * inputY, 0);

		movement *= Time.deltaTime;

		transform.Translate (movement);


		GameObject bubble = (GameObject) Instantiate(wreck, transform.position, transform.rotation);

	}
}
