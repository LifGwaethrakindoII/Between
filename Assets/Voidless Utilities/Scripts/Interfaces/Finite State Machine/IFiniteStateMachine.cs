namespace VoidlessUtilities
{
/// \TODO Add 'On' Prefix into Interface's Callbacks. For more readability.
public interface IFiniteStateMachine<T>
{
	T state { get; set; } 			/// <summary>Current state.</summary>
	T previousState { get; set; } 	/// <summary>Previous State.</summary>

	/// <summary>Enters T State.</summary>
	/// <param name="_state">T State that will be entered.</param>
	void EnterState(T _state);

	/// <summary>Exited T State.</summary>
	/// <param name="_state">T State that will be left.</param>
	void ExitState(T _state);
}
}