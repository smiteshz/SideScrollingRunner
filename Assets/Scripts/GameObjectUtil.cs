using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameObjectUtil {
	private static Dictionary<RecycleGameObject, ObjectsPool> pools = new Dictionary<RecycleGameObject, ObjectsPool> (); 
	public static GameObject Instantiate (GameObject prefab, Vector3  pos)	{
		GameObject instance = null;
		var recycledScript = prefab.GetComponent<RecycleGameObject> ();
		if (recycledScript != null) {
			var pool = GetObjectPool (recycledScript);
			instance = pool.NextObj (pos).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}
	public static void Destroy (GameObject gameObject)	{
		var recycleGameObject = gameObject.GetComponent<RecycleGameObject> ();
		if (recycleGameObject != null)
			recycleGameObject.ShutDown ();
		else
		GameObject.Destroy (gameObject);
	}
	private static ObjectsPool GetObjectPool (RecycleGameObject reference) {
		ObjectsPool pool = null;
		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else {
			var poolContainer = new GameObject (reference.gameObject.name + "ObjectsPool");
			pool = poolContainer.AddComponent<ObjectsPool> ();
			pool.prefab = reference;
			pools.Add (reference, pool);
		}
		return pool;			
	}
}