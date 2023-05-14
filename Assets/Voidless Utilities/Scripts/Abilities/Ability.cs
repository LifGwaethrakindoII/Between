using System.Collections;
using System;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class Ability : MonoBehaviour, IAbility, IFiniteStateMachine<AbilityState>
{
	[SerializeField] private ulong _abilityID; 			/// <summary>Ability's ID.</summary>
	[SerializeField] private float _cost; 				/// <summary>Cost of the ability to be done.</summary>
	[SerializeField] private float _cooldownDuration; 	/// <summary>Ability's cooldown duration.</summary>
	protected AbilityState PreviousState; 				/// <summary>IFiniteStateMachine previousState property.</summary>
	protected AbilityState State; 						/// <summary>IFiniteStateMachine state property.</summary>
	protected Behavior abilityCooldown; 				/// <summary>Cooldown's Coroutine controller.</summary>

	/// <summary>Gets and Sets abilityID property.</summary>
	public ulong abilityID
	{
		get { return _abilityID; }
		set { _abilityID = value; }
	}

	/// <summary>Gets and Sets cost property.</summary>
	public float cost
	{
		get { return _cost; }
		set { _cost = value; }
	}

	/// <summary>Gets and Sets cooldownDuration property.</summary>
	public float cooldownDuration
	{
		get { return _cooldownDuration; }
		set { _cooldownDuration = value; }
	}

	/// <summary>Gets and Sets previousState property.</summary>
	public AbilityState previousState
	{
		get { return PreviousState; }
		set { PreviousState = value; }
	}

	/// <summary>Gets and Sets state property.</summary>
	public AbilityState state
	{
		get { return State; }
		set { State = value; }
	}

	private void OnEnable()
	{
		OnAbilityEnabled();
	}

	private void OnDisable()
	{
		OnAbilityDisabled();
	}

	private void Awake()
	{
		OnAbilityAwake();
	}

#region FiniteStateMachine:
	/// <summary>Enters AbilityState State.</summary>
	/// <param name="_state">AbilityState State that will be entered.</param>
	public void EnterState(AbilityState _state)
	{
		switch(_state)
		{
			case AbilityState.Available:
			OnAvailable();
			break;

			case AbilityState.Unavailable:
			OnUnavailable();
			break;

			case AbilityState.Using:
			OnUsing();
			break;

			case AbilityState.Fatigue:
			OnFatigue();
			break;
	
			default:
			break;
		}
	}
	
	/// <summary>Leaves AbilityState State.</summary>
	/// <param name="_state">AbilityState State that will be left.</param>
	public void ExitState(AbilityState _state)
	{
		switch(_state)
		{
			case AbilityState.Available:
			break;

			case AbilityState.Unavailable:
			break;

			case AbilityState.Using:
			break;

			case AbilityState.Fatigue:
			if(abilityCooldown != null) abilityCooldown = null;
			break;
	
			default:
			break;
		}
	}
#endregion

	/// <summary>Casts ability.</summary>
	public abstract void CastAbility();

	/// <summary>Reposes ability.</summary>
	public abstract void ReposeAbility();

	/// <summary>Resets the Accelerable's Ability.</summary>
	public abstract void ResetAbility();

	/// <summary>Callback invoked when the Available state is entered. Override if you want to do something particular when this state is entered</summary>
	public virtual void OnAvailable() { /*...*/ }

	/// <summary>Callback invoked when the Unavailable state is entered. Override if you want to do something particular when this state is entered</summary>
	public virtual void OnUnavailable() { /*...*/ }

	/// <summary>Callback invoked when the Using state is entered. Override if you want to do something particular when this state is entered</summary>
	public virtual void OnUsing() { /*...*/ }

	/// <summary>Callback invoked when the Fatigue state is entered. Overridable.</summary>
	public virtual void OnFatigue()
	{
		VoidlessCoroutines.DispatchBehavior(ref abilityCooldown);
		abilityCooldown = new Behavior(this, Cooldown());
	}

	/// <summary>Callback invoked when the Base's Ability get's OnEnable callback's called. Not obligatory to override.</summary>
	public virtual void OnAbilityEnabled()
	{
		this.ChangeState(AbilityState.Available);
	}

	/// <summary>Callback invoked when the Base's Ability get's OnDisable callback's called. Not obligatory to override.</summary>
	public virtual void OnAbilityDisabled()
	{
		this.ChangeState(AbilityState.Unavailable);
	}

	/// <summary>Callback invoked when the Base's Ability get's Awake callback's called. Not obligatory to override.</summary>
	public virtual void OnAbilityAwake() { /*...*/ }

	/// <summary>Callback invoked when the Ability's Cooldown finishes. Not obligatory to override.</summary>
	public virtual void OnCooldownFinished()
	{
		VoidlessCoroutines.DispatchBehavior(ref abilityCooldown);
	}

	/// <summary>Waits for Cooldown to cease, then invokes callbacks.</summary>
	public virtual IEnumerator Cooldown()
	{
		WaitForSeconds wait = new WaitForSeconds(cooldownDuration);
		yield return wait;
		this.ChangeState(AbilityState.Available);
		OnCooldownFinished();
	}
}
}