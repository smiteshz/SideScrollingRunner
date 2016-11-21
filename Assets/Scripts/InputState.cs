using UnityEngine;
using System.Collections;
public class InputState : MonoBehaviour {
	public bool actionButton;
	public float absoluteVelocityX = 0f;
	public float absoluteVelocityY = 0f;
	public bool standing = false;
	public float standingThreshold = 1f;
	private Rigidbody2D body2D;
	void Awake () {
		body2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		actionButton = Input.anyKeyDown;
	}
	void FixedUpdate() {
		absoluteVelocityX = System.Math.Abs(body2D.velocity.x);
		absoluteVelocityY = System.Math.Abs(body2D.velocity.y);
		standing = absoluteVelocityY <= standingThreshold;
	}
}
