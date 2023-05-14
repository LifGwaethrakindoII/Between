using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IEulerRotationAbility : IRotationAbility<Vector3>
{
	Axes3D rotationConstrains { get; set; } 	/// <summary>Rotation Dimension's constrains.</summary>

	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_axisX">Axis X's Rotation.</param>
	/// <param name="_axisY">Axis Y's Rotation.</param>
	/// <param name="_axisZ">Axis Z's Rotation.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	void CastAbility(float _axisX, float _axisY, float _axisZ, Space _space = Space.Self);
}
}