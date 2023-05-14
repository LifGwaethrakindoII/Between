using System.Collections;

namespace VoidlessUtilities
{
public interface IFiniteStateAgent<T> where T : IFiniteStateAgent<T>
{
	FiniteStateMachine<T> FSM { get; set; } 		/// <summary>Agent's Finite State Machine.</summary>
	IState<T> currentState { get; set; } 			/// <summary>Agent's Current State.</summary>
	IState<T> previousState { get; set; } 			/// <summary>Agent's Previous State.</summary>
	IEnumerator executionEnumerator { get; set; } 	/// <summary>Agent's Execution Enumerator.</summary>
}	
}