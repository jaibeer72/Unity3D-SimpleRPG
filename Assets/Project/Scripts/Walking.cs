using UnityEngine;
using states;
using UnityEngine.AI;

public class Walking : State<AI>
{
	private static Walking instance;
	private bool isPlayerVisible = false;	

	private Walking()
	{
		if (instance != null)
		{
			return;
		}

		instance = this;
	}

	public static Walking Instance
	{
		get
		{
			if (instance == null)
			{
				new Walking();
			}
			return instance;
		}
	}

	public override void Enter(AI owner)
	{
		owner.GetComponent<NavMeshAgent>().isStopped = false;
	}

	public override void Exit(AI owner)
	{
		owner.GetComponent<NavMeshAgent>().isStopped = true;
	}

	public override void UpdateState(AI owner)
	{
		owner.GetComponent<NavMeshAgent>().SetDestination(owner.player.transform.position);

		RaycastHit hit;
		if(owner.debug == true)	Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * owner.range, Color.green);
		if (Vector3.Distance(owner.transform.position, owner.player.transform.position) <= owner.range)
		{
			if (owner.GetComponent<NavMeshAgent>().remainingDistance <= owner.range)
			{
				Quaternion targetRot = Quaternion.LookRotation(owner.player.transform.position - owner.transform.position);
				owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRot, owner.aiLookAtSpeed * Time.deltaTime);
			}
			if (owner.debug == true) Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * owner.range, Color.yellow);
			if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out hit, owner.range))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					if (owner.debug == true) Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
					owner.statemachine.ChangeState(Shooting.Instance);
				}
			}
		}
	}
}
