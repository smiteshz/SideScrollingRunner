using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {
	public Vector2 velocity = Vector2.zero;
	private Rigidbody2D rigidBody;
	void Awake()	{
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		rigidBody.velocity = velocity;
}
}
