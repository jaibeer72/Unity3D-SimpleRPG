namespace states
{
	public class Statemachine<T>
	{
		public State<T> currentState { get; private set; }
		public T Owner;

		public Statemachine(T o)
		{
			Owner = o;
			currentState = null;
		}

		public void ChangeState(State<T> newstate)
		{
			if (currentState != null)
			{
				currentState.Exit(Owner);
			}
			currentState = newstate;
			currentState.Enter(Owner);
		}

		public void Update()
		{
			if (currentState != null)
			{
				currentState.UpdateState(Owner);
			}
		}
	}

	public abstract class State<T>
	{
		public abstract void Enter(T owner);
		public abstract void Exit(T owner);
		public abstract void UpdateState(T owner);
	}
}
