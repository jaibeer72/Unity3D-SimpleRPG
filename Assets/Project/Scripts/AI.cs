using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using states;

public class AI : MonoBehaviour {
	
	public GameObject player;
	public Transform Gun;
	public float speed = 3f;
	public float range = 3f;
	public int firerate;
	public string type;
	public float aiLookAtSpeed = 3f;
	public float fadeSpeed;

	[Header("Debugging")]
	public bool debug = false;
	public Color clr;
	public float liftUp;

	public Statemachine<AI> statemachine { get; set; }

	void Start ()
	{
		Debug.Log(fadeSpeed * Time.deltaTime);
		statemachine = new Statemachine<AI>(this);
		statemachine.ChangeState(Sleeping.Instance);
	}
	
	void Update ()
	{
		statemachine.Update();
	}

	void OnDrawGizmos()
	{
		if (debug == true)
		{
			Gizmos.color = clr;
			Gizmos.DrawSphere(new Vector3(player.transform.position.x, liftUp, player.transform.position.z), 0.25f);
		}
	}
}
