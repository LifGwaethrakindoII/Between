using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IRotationAbility<T> : IDirectionTargetedAbility<T>
{
	float rotationSpeed { get; set; } 	/// <summary>Scalar that represents the rotation's speed.</summary>

	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	void CastAbility(T _direction, Space _space = Space.Self);
}
}