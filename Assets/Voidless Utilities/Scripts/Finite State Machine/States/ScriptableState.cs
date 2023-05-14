using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class ScriptableState<T> : ScriptableObject, IState<T> where T : IFiniteStateAgent<T>
{
	public List<StateTransition<T>> transitions { get; set; } 	/// <summary>State's Transitions.</summary>

	/// <summary>Message invoked when entering state.</summary>
	/// <param name="_delta">Optional Delta Time information.</param>
	public abstract void OnEnterState(T _instance, float _deltaTime);

	/// <summary>Message invoked when executing state.</summary>
	/// <param name="_delta">Optional Delta Time information.</param>
	public abstract IEnumerator OnExecuteState(T _instance, float _deltaTime);

	/// <summary>Message invoked when leaving state.</summary>
	/// <param name="_delta">Optional Delta Time information.</param>
	public abstract void OnExitState(T _instance, float _deltaTime);
}
}