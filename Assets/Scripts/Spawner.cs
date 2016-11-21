using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject[] prefabs;
	public bool active = true;
	public float delay = 2.0f;
	public Vector2 delayRange = new Vector2 (1f,2f);
	// Use this for initialization
	void Start () {
		ResetDelay ();
		StartCoroutine (EnemyGenerator ());
	}
	IEnumerator EnemyGenerator()
	{
		yield return new WaitForSeconds (delay);
		if (active) {
			var newTransform = transform;
			GameObjectUtil.Instantiate(prefabs [Random.Range (0, prefabs.Length)], newTransform.position);
		}
		ResetDelay ();

		StartCoroutine (EnemyGenerator ());
	}
	void ResetDelay()
	{
		delay = Random.Range (delayRange.x, delayRange.y);
	}

}
