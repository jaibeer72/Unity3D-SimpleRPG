using UnityEngine;
using states;
using UnityEngine.AI;

public class Sleeping : State<AI>
{
	private static Sleeping instance;
	private bool isPlayerVisible = false;
	private float timer;
	private float shaderCutOffValue;

	private Sleeping()
	{
		if (instance != null)
		{
			return;
		}

		instance = this;
	}

	public static Sleeping Instance
	{
		get
		{
			if (instance == null)
			{
				new Sleeping();
			}
			return instance;
		}
	}

	public override void Enter(AI owner)
	{
		shaderCutOffValue = -1f;
		owner.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("Vector1_A235A670", shaderCutOffValue);
		owner.GetComponent<NavMeshAgent>().isStopped = false;
	}

	public override void Exit(AI owner)
	{
		owner.GetComponent<NavMeshAgent>().isStopped = true;
	}

	public override void UpdateState(AI owner)
	{
		//owner.GetComponent<NavMeshAgent>().SetDestination(owner.player.transform.position);
		RaycastHit hit;
		if (owner.debug == true) Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * owner.range, Color.green);
		if (Vector3.Distance(owner.transform.position, owner.player.transform.position) <= owner.range)
		{
			shaderCutOffValue += owner.fadeSpeed * Time.deltaTime;
			owner.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("Vector1_A235A670", shaderCutOffValue);
			if (shaderCutOffValue > 1.5f)
			{
				owner.statemachine.ChangeState(Walking.Instance);
			}
		}
		if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.range)
		{
			shaderCutOffValue -= owner.fadeSpeed * Time.deltaTime;
			owner.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("Vector1_A235A670", shaderCutOffValue);
			if (shaderCutOffValue <= -1f)
			{
				shaderCutOffValue = -1f;
			}
		}
	}
}
