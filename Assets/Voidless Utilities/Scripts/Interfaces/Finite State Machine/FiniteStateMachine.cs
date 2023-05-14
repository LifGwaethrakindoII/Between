using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public enum StateChangeType 												/// <summary>Types of State's Change.</summary>
{
	Entered, 																/// <summary>State Entered Change.</summary>
	Left 																	/// <summary>State Left Change.</summary>
}

/// <summary>Event invoked when a state changed.</summary>
/// <param name="_state">State that changed.</param>
/// <param name="_change">Type of change.</param>
public delegate void OnStateChanged<T>(T _state, StateChangeType _changeType);

public class FiniteStateMachine<T> where T : IFiniteStateAgent<T>
{
	/// \TODO Create a clean global state change delegate...
	//private static event OnStateChanged<T> onGlobalStateChanged; 			/// <summary>Event subscription delegate invoked when the global state changes.</summary>

	private static FiniteStateMachine<T> _instance; 						/// <summary>FSM's Singleton Instance.</summary>
	private static IState<T> _globalState; 									/// <summary>Global State shared among all agent targets.</summary>
	private static IState<T> _previousGlobalState; 							/// <summary>Previous Global State shared among all agent targets.</summary>

	/// <summary>Gets and Sets Instance property.</summary>
	public static FiniteStateMachine<T> Instance
	{
		get { return _instance; }
		private set { _instance = value; }
	}

	/// <summary>Gets and Sets globalState property.</summary>
	public static IState<T> globalState
	{
		get { return _globalState; }
		set { _globalState = value; }
	}

	/// <summary>Gets and Sets previousGlobalState property.</summary>
	public static IState<T> previousGlobalState
	{
		get { return _previousGlobalState; }
		private set { _previousGlobalState = value; }
	}

	/// <summary>Finite State Machine's static constructor.</summary>
	static FiniteStateMachine() { /*...*/ }

	/// <summary>Private FiniteStateMachine constructor.</summary>
	/// <param name="_initialState">Default initial State.</param>
	private FiniteStateMachine(IState<T> _initialState = null)
	{
		previousGlobalState = null;
		globalState = _initialState;
	}

	/// <summary>FiniteStateMachine constructor.</summary>
	/// <param name="_agent">This Finite State Machine's Agent.</param>
	/// <param name="_initialState">Default initial State.</param>
	public FiniteStateMachine(T _agent, IState<T> _initialState = null)
	{
		if(_initialState != null) ChangeState(_agent, _initialState);
		_agent.FSM = this;
	}

	/// <summary>Changes current State to new one.</summary>
	/// <param name="_agent">Finite State Agent who request the State change.</param>
	/// <param name="_deltaTime">Optional Delta Time information.</param>
	/// <param name="_state">New State to switch.</param>
	public static void ChangeState(T _agent, IState<T> _state, float _deltaTime = -1.0f)
	{
		float delta = _deltaTime == -1.0f ? Time.deltaTime : _deltaTime;
		_agent.previousState = _agent.currentState;
		_agent.currentState = _state;

		if(_agent.previousState != null) _agent.previousState.OnExitState(_agent, delta);
		_agent.currentState.OnEnterState(_agent, delta);
		_agent.executionEnumerator = _agent.currentState.OnExecuteState(_agent, _deltaTime == -1.0f ? Time.deltaTime : _deltaTime);
	}

	/// <summary>Returns to previous State, if there is.</summary>
	/// <param name="_agent">Finite State Agent who request the State retrieval.</param>
	/// <param name="_deltaTime">Optional Delta Time information.</param>
	public static void ReturnToPreviousState(T _agent, float _deltaTime = -1.0f)
	{
		if(_agent.previousState != null)
		{
			float delta = _deltaTime == -1.0f ? Time.deltaTime : _deltaTime;
			IState<T> newState = _agent.previousState;

			_agent.currentState.OnExitState(_agent, delta);
			_agent.previousState = _agent.currentState;
			_agent.currentState = newState;
			_agent.currentState.OnEnterState(_agent, delta);
			_agent.executionEnumerator = _agent.currentState.OnExecuteState(_agent, _deltaTime == -1.0f ? Time.deltaTime : _deltaTime);
		}
	}

	/// <summary>Returns to previous global State, if there is.</summary>
	/// <param name="_deltaTime">Optional Delta Time information.</param>
	public static void ReturnToPreviousGlobalState(float _deltaTime = -1.0f)
	{
		if(previousGlobalState != null)
		{
			IState<T> newGlobalState = previousGlobalState;

			previousGlobalState = globalState;
			globalState = newGlobalState;
			//if(onGlobalStateChanged != null) onGlobalStateChanged(globalState, StateChangeType.Entered);
		}
	}

	/// <summary>Callback invoked when the implementer needs to be updated.</summary>
	/// <param name="_agent">Additional argument provided.</param>
	/// <param name="_delta">Optional Delta Time [-1 as default to give delta Time].</param>
	public static void OnUpdate(T _agent, float _deltaTime = -1.0f)
	{
		if(_agent != null && _agent.currentState != null)
		{
			#pragma warning disable 0642
			if(_agent.executionEnumerator.MoveNext());
			else _agent.executionEnumerator = _agent.currentState.OnExecuteState(_agent, _deltaTime == -1.0f ? Time.deltaTime : _deltaTime);
			
			if(_agent.currentState.transitions != null)
			foreach(StateTransition<T> transition in _agent.currentState.transitions)
			{
				if(transition.TransitionPredicate(_agent))
				{
					ChangeState(_agent, transition.transitionState);
					break;
				}
			}
		}
	}

	/// <summary>Resets State.</summary>
	/// <param name="_agent">Additional argument provided.</param>
	/// <param name="_delta">Optional Delta Time [-1 as default to give delta Time].</param>
	public static void ResetState(T _agent, float _deltaTime = -1.0f)
	{
		_agent.currentState.OnExecuteState(_agent, _deltaTime == -1.0f ? Time.deltaTime : _deltaTime);
	}
}
}