using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour,IRecycle {
	public Sprite[] sprites;
	public Vector2 coffset = Vector2.zero;
	public void Restart () {
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprites [Random.Range (0,sprites.Length)];
		var collider = GetComponent<BoxCollider2D>();
		var size = renderer.bounds.size;
		size.y += coffset.y;
		collider.size = size;
		collider.offset = new Vector2 (-coffset.x, collider.size.y / 2 - coffset.y);
	}
	public void Shutdown () {

	}

}
