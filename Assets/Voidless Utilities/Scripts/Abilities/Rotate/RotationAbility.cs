using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class RotationAbility<T> : Ability, IRotationAbility<T>
{
	[SerializeField] private float _rotationSpeed; 	/// <summary>Rotation's Speed.</summary>

	/// <summary>Gets and Sets rotationSpeed property.</summary>
	public float rotationSpeed
	{
		get { return _rotationSpeed; }
		set { _rotationSpeed = value; }
	}

#region AbilityImplementations:
	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public abstract void CastAbility(T _direction, Space _space = Space.Self);

	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_origin">Origin's direction normal from the Observer.</param>
	/// <param name="_target">Target's Direction.</param>
	/// <param name="_speed">Direction change's speed.</param>
	/// <param name="_delta">Expected difference between the Target and the Observer.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public abstract void CastAbilityTowards(T _origin, T _target, float _speed, float _delta, Space _space = Space.Self);
#endregion
}
}