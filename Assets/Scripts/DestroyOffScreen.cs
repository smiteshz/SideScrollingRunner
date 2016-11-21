using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {
	public float offset = 16f;
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;
	private bool offScreen = true;
	private float offscreenX = 0;
	private Rigidbody2D body2D;

	void Awake(){
		body2D = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
			offscreenX = (Screen.width/PixelPerfectCamera.pixelsToUnit)/2 + offset;
	}
	
	// Update is called once per frame
	void Update () {
		var posX = transform.position.x;
		var dirX = body2D.velocity.x;
			if(Mathf.Abs(posX)>offscreenX)
			{
			if (dirX < 0 && posX < -offscreenX) {
				offScreen = true;
			} else if (dirX > 0 && posX > offscreenX) {
				offScreen = true;
			} else {
				offScreen = false;
			}
				if (offScreen)
				{
				OutofBounds ();
				}

	}
}
		public void OutofBounds()
	{
		offScreen = false;
		GameObjectUtil.Destroy (gameObject);
		if (DestroyCallback != null) {
			DestroyCallback ();
		}
	}
}
