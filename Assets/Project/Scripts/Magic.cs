using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour,PooledObjectStart {

	public float speed;
	public float timeOfActive = 2f;
	float timer = 0f;
	
	public void ObjSpwanStart()
	{
		timer = timeOfActive;
		this.gameObject.SetActive(true);
	}

	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			timer = timeOfActive;
			this.gameObject.SetActive(false);
		}
	}
}
