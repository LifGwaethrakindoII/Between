using UnityEngine;

namespace VoidlessUtilities
{
public class AccelerableTransformJumpingAbility : AccelerableJumpingAbility
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
		/*//if(state == AbilityState.Available || state == AbilityState.Using)
		{*/
			//if(accelerable.speed > accelerable.minSpeed)
			{
				state = AbilityState.Using;
				accelerable.Accelerate(Time.deltaTime);
				transform.Translate((_space == Space.Self ? transform.TransformDirection(_direction) : _direction) * accelerable.speed * Time.deltaTime);
			}
		//}
		//else this.ChangeState(AbilityState.Fatigue);
	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{
		accelerable.Decelerate(Time.deltaTime);
	}

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override void ResetAbility()
	{
		accelerable.Reset();
	}
}
}