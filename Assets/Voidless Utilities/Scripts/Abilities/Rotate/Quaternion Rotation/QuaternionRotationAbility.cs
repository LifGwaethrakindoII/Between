using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class QuaternionRotationAbility : RotationAbility<Quaternion>
{
#region AbilityImplementations:
	/// <summary>Casts Rotation Ability at given direction.</summary>
	/// <param name="_direction">Direction to cast the ability.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(Quaternion _direction, Space _space = Space.Self)
	{
		throw new NotImplementedException();
	}
#endregion
}
}