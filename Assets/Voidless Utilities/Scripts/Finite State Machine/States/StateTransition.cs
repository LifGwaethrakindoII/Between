using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
/// <summary>Defines the signature of the State Transition's Policy [Func<bool>].</summary>
/// <param name="_target">Target's agent to evaluate in policy.</param>
/// <returns>Whether the policy is or not approved.</returns>
public delegate bool StateTransitionPolicy<T>(T _target);

/*/// <summary>Predicate that defines a State of an especific Agent.</summary>
/// <param name="_agent">Agent that contains the Subject State.</param>
/// <returns>Agent's State.</returns>
public delegate IState<T> AgentStatePredicate<T>(T _agent);*/

public struct StateTransition<T> where T : IFiniteStateAgent<T>
{
	public IState<T> transitionState; 						/// <summary>Desired's Trabsition State.</summary>
	public StateTransitionPolicy<T> TransitionPredicate; 	/// <summary>Transition Policy predicate that will determine whether the transition will be made or not.</summary>

	/// <summary>StateTransition constructor.</summary>
	/// <param name="transitionPredicate">Transition's Condition.</param>
	/// <param name="_transitionState">Transition's State.</param>
	public StateTransition(StateTransitionPolicy<T> transitionPredicate, IState<T> _transitionState)
	{
		TransitionPredicate = transitionPredicate;
		transitionState = _transitionState;
	}

	/// <summary>Returns a new State Transition [without the new keyword].</summary>
	/// <param name="transitionPredicate">Transition's Condition.</param>
	/// <param name="_transitionState">Transition's State.</param>
	/// <returns>New State Transition.</returns>
	public static StateTransition<T> Define(StateTransitionPolicy<T> transitionPredicate, IState<T> _transitionState)
	{
		return new StateTransition<T>(transitionPredicate, _transitionState);
	}
}
}