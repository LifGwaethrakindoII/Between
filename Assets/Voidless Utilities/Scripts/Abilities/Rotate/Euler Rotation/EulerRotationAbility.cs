using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class EulerRotationAbility : RotationAbility<Vector3>
{
	[SerializeField] private Axes3D _rotationConstrains; 	/// <summary>Rotation Dimension's constraints.</summary>

	/// <summary>Gets and Sets rotationConstrains property.</summary>
	public Axes3D rotationConstrains
	{
		get { return _rotationConstrains; }
		set { _rotationConstrains = value; }
	}

#region AbilityImplementations:
	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override abstract void CastAbility(Vector3 _direction, Space _space = Space.Self);

	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_axisX">Axis X's Rotation.</param>
	/// <param name="_axisY">Axis Y's Rotation.</param>
	/// <param name="_axisZ">Axis Z's Rotation.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public abstract void CastAbility(float _axisX, float _axisY, float _axisZ, Space _space = Space.Self);

	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_origin">Origin's direction normal from the Observer.</param>
	/// <param name="_target">Target's Direction.</param>
	/// <param name="_speed">Direction change's speed.</param>
	/// <param name="_delta">Expected difference between the Target and the Observer.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override abstract void CastAbilityTowards(Vector3 _origin, Vector3 _target, float _speed, float _delta, Space _space = Space.Self);
#endregion
}
}