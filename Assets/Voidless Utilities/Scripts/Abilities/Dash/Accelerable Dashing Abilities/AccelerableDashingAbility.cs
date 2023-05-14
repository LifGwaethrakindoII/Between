using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class AccelerableDashingAbility : DashingAbility, IAccelerable<Vector3>
{
	[SerializeField] private Accelerable _accelerable; 	/// <summary>Ability's Accelerable.</summary>
	private Vector3 _velocity; 							/// <summary>Ability's Velocity.</summary>

	/// <summary>Gets and Sets accelerable property.</summary>
	public Accelerable accelerable
	{
		get { return _accelerable; }
		set { _accelerable = value; }
	}

	/// <summary>Gets and Sets velocity property.</summary>
	public Vector3 velocity
	{
		get { return _velocity; }
		set { _velocity = value; }
	}

	/// <summary>Casts ability.</summary>
	public override abstract void CastAbility();

	/// <summary>Overloaded. Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override abstract void CastAbility(Vector3 _direction, Space _space = Space.Self);

	/// <summary>Reposes ability.</summary>
	public override abstract void ReposeAbility();

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override abstract void ResetAbility();
}
}