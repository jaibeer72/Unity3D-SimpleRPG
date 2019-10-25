using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	public static ObjectPooler Instance;
	private void Awake()
	{
		Instance = this;
	}

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	void Start () {

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);

				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpwanfromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with this " + tag + " doesnt exist");
			return null;
		}

		GameObject objectToSpwan = poolDictionary[tag].Dequeue();

		//objectToSpwan.SetActive(true);
		objectToSpwan.transform.position = position;
		objectToSpwan.transform.rotation = rotation;

		PooledObjectStart pooledObj = objectToSpwan.GetComponent<PooledObjectStart>();

		if(pooledObj != null)
		{
			pooledObj.ObjSpwanStart();
		}

		poolDictionary[tag].Enqueue(objectToSpwan);
		return objectToSpwan;
	}
}
