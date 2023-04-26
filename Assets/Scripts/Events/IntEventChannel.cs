using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have an int argument.
/// Example: An event to toggle a UI interface
/// </summary>

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannel : DescriptionBaseSO
{
	public event UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
