using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[RequireComponent(typeof(Rigidbody))]
public class AccelerableRigidbodyDashingAbility : AccelerableDashingAbility
{
	private Rigidbody _rigidbody; 					/// <summary>Rigidbody's Component.</summary>
	[SerializeField] private ForceMode _forceMode; 	/// <summary>Force Mode Applied.</summary>

	/// <summary>Gets and Sets rigidbody Component.</summary>
	public new Rigidbody rigidbody
	{ 
		get
		{
			if(_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}
			return _rigidbody;
		}
	}

	/// <summary>Gets and Sets forceMode property.</summary>
	public ForceMode forceMode
	{
		get { return _forceMode; }
		set { _forceMode = value; }
	}

	/// <summary>Casts ability.</summary>
	public override void CastAbility()
	{
		CastAbility(Vector3.forward);
	}

	/// <summary>Callback invoked when the Base's Ability get's Awake callback's called. Not obligatory to override.</summary>
	public override void OnAbilityAwake()
	{
		accelerable.Reset();
	}

	/// <summary>Overloaded. Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(Vector3 _direction, Space _space = Space.Self)
	{//
		if(state == AbilityState.Available || state == AbilityState.Using)
		{
			if(accelerable.speed < accelerable.maxSpeed)
			{
				accelerable.Accelerate(Time.fixedDeltaTime);
				if(rigidbody.isKinematic)
				rigidbody.AddDesiredForce((_space == Space.Self ? transform.TransformDirection(_direction) : _direction) * accelerable.speed * Time.fixedDeltaTime);
				else
				rigidbody.MovePosition(transform.position + (_space == Space.Self ? transform.TransformDirection(_direction) : _direction) * accelerable.speed * Time.fixedDeltaTime);
			}
			else this.ChangeState(AbilityState.Fatigue);
		}
		Debug.LogWarning("[AccelerableRigidbodyDashingAbility] Ability currently unavailable...");
	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{
		if(state != AbilityState.Fatigue && state != AbilityState.Available) this.ChangeState(AbilityState.Fatigue);
		accelerable.Decelerate();
	}

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override void ResetAbility()
	{
		accelerable.speed = accelerable.minSpeed;
	}
}
}