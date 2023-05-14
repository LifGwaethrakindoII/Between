using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IDisplacementAbility : IAbility
{
	Axes3D movementConstrains { get; set; } 	/// <summary>Movement Dimension's constrains.</summary>
	float displacementSpeed { get; set; } 		/// <summary>Scalar that represents the displacement's speed.</summary>

	/// <summary>Casts Displacement Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	void CastAbility(Vector3 _direction, Space _space = Space.Self);
}
}