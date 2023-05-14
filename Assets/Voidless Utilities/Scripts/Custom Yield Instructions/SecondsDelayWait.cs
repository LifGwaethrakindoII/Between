using System.Collections;
using UnityEngine;

namespace VoidlessUtilities
{
public class SecondsDelayWait : VoidlessYieldInstruction
{
	private float _waitDuration; 	/// <summary>Wait Delay's Duration.</summary>
	private float _currentWait; 	/// <summary>Current Wait's Time.</summary>

	/// <summary>Gets and Sets waitDuration property.</summary>
	public float waitDuration
	{
		get { return _waitDuration; }
		protected set { _waitDuration = value; }
	}

	/// <summary>Gets and Sets currentWait property.</summary>
	public float currentWait
	{
		get { return _currentWait; }
		protected set { _currentWait = value; }
	}

	/// <summary>SecondsDelayWait's constructor.</summary>
	/// <param name="_waitDuration">Wait's Duration.</param>
	public SecondsDelayWait(float _waitDuration) : base()
	{
		waitDuration = _waitDuration;
		currentWait = 0.0f;
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
	public override void Reset()
	{
		currentWait = 0.0f;
		enumerator = Operation();
	}

	/// <summary>Yield Instruction's Operation.</summary>
	protected override IEnumerator Operation()
	{
		while(currentWait < (waitDuration + Mathf.Epsilon))
		{
			currentWait += Time.deltaTime;
			yield return null;
		}
	}
}
}