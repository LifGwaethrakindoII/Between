using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class TransformEulerRotationAbility : EulerRotationAbility
{
#region AbilityImplementations:
	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(Vector3 _direction, Space _space = Space.Self)
	{
		transform.Rotate((_direction * rotationSpeed * Time.deltaTime), _space);
	}

	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_axisX">Axis X's Rotation.</param>
	/// <param name="_axisY">Axis Y's Rotation.</param>
	/// <param name="_axisZ">Axis Z's Rotation.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(float _axisX, float _axisY, float _axisZ, Space _space = Space.Self)
	{
		transform.Rotate((_axisX * Time.deltaTime), (_axisY * Time.deltaTime), (_axisZ * Time.deltaTime), _space);
	}

	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_origin">Origin's direction normal from the Observer.</param>
	/// <param name="_target">Target's Direction.</param>
	/// <param name="_speed">Direction change's speed.</param>
	/// <param name="_delta">Expected difference between the Target and the Observer.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbilityTowards(Vector3 _origin, Vector3 _target, float _speed, float _delta, Space _space = Space.Self)
	{
		Vector3 fromDirection = (_space == Space.Self ? transform.TransformDirection(_origin) : _origin).normalized;
		Vector3 newDirection = Vector3.RotateTowards(fromDirection, _target, (_speed * Time.deltaTime), _delta);
		transform.rotation = Quaternion.LookRotation(newDirection);
	}

	/// <summary>Casts ability.</summary>
	public override void CastAbility()
	{

	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{

	}

	/// <summary>Resets the Accelerable's Ability.</summary>
	public override void ResetAbility()
	{

	}
#endregion
}
}