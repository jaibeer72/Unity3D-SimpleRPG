using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	public Camera camera;

	public NavMeshAgent agent;

	// Use this for initialization
	void Start () {

		agent = this.GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
			}
		}
	}
}
