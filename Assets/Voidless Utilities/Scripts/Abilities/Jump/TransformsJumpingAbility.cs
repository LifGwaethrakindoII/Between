using UnityEngine;

namespace VoidlessUtilities
{
public class TransformJumpingAbility : JumpingAbility
{
	/// <summary>Casts ability.</summary>
	public override void CastAbility()
	{
		
	}

	/// <summary>Overloaded. Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(Vector3 _direction, Space _space = Space.Self)
	{
		
	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{
		
	}

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override void ResetAbility()
	{
		
	}
}
}