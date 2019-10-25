using UnityEngine;
using states;

public class Shooting : State<AI>
{
	private static Shooting instance;
	private ObjectPooler objPooler;
	float timer = 0f;

	private Shooting()
	{
		if (instance != null)
		{
			return;
		}

		instance = this;
	}

	public static Shooting Instance
	{
		get
		{
			if (instance == null)
			{
				new Shooting();
			}
			return instance;
		}
	}

	public override void Enter(AI owner)
	{
		objPooler = ObjectPooler.Instance;
		timer = owner.firerate;
	}

	public override void Exit(AI owner)
	{

	}

	public override void UpdateState(AI owner)
	{
		if (owner.debug == true) Debug.DrawRay(owner.transform.position, owner.transform.TransformDirection(Vector3.forward) * owner.range, Color.red);
		Quaternion targetRot = Quaternion.LookRotation(owner.player.transform.position - owner.transform.position);
		owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRot, owner.aiLookAtSpeed * Time.deltaTime);
		timer += Time.deltaTime;
		if(timer > owner.firerate)
		{
			objPooler.SpwanfromPool(owner.type, owner.Gun.position, owner.Gun.rotation);
			timer = 0;
		}
		if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > owner.range)
		{
			owner.statemachine.ChangeState(Walking.Instance);
		}
		RaycastHit hit;
		if (Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out hit, owner.range))
		{
			if (hit.collider.gameObject.tag != "Player")
			{
				owner.statemachine.ChangeState(Walking.Instance);
			}
		}
	}
}
