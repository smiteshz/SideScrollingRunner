﻿using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	public float jumpSpeed = 240;
	public float forwardSpeed = 20;
	private Rigidbody2D body2D;
	private InputState inputState;
	void Awake(){
		body2D = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}

	void Update () {
		if (inputState.actionButton) {
			if (inputState.standing) {
				body2D.velocity = new Vector2 (transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
			}
		}
	}
}