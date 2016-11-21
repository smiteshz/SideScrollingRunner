using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {
	private Animator animator;
	private InputState inputState;
	void Awake () {
		animator = GetComponent<Animator> ();
		inputState = GetComponent<InputState> ();
	}
	void Update () {
		var running = true;
		if (inputState.absoluteVelocityX > 0 && inputState.absoluteVelocityY < inputState.standingThreshold)
			running = false;
		animator.SetBool ("Running", running);
	}
}
