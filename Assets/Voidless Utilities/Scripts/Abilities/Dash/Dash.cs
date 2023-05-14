using UnityEngine;

namespace VoidlessUtilities
{
public class Dash/* : AccelerableAbility*/
{
	/*void Awake()
	{
		attributes.speed = attributes.minSpeed;
	}

	/// <summary>Casts ability.</summary>
	public override void CastAbility()
	{
		Cast(Vector3.forward);
	}

	/// <summary>Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	public override void CastAbility(Vector3 _direction)
	{
		//if(state == AbilityState.Available || state == AbilityState.Using)
		{
			if(attributes.speed < attributes.maxSpeed)
			{
				state = AbilityState.Using;
				attributes.Accelerate(Thread.Main);
				Vector3 newVelocity = transform.TransformDirection(
					_direction.x,
					_direction.y,
					(_direction.z  * (attributes.speed)));

				accumulatedVelocity +=  newVelocity;
				velocityAccumulator.AccumulateVelocity(newVelocity);

			}else if(++currentRepeatTimes < repeatTimes)
			{
				attributes.speed = attributes.minSpeed;
			}
			else
			{
				this.ChangeState(AbilityState.Fatigue);
			}
		}
	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{
		if(attributes.speed > 0)
		{
			attributes.DecelerateToZero();
			Vector3 newVelocity = transform.TransformDirection(
					0f,
					0f,
					((attributes.speed)));
			//rigidbody.velocity -=  newVelocity;
			velocityAccumulator.AccumulateVelocity(newVelocity);
		}
	}

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override void ResetAbility()
	{
		attributes.speed = attributes.minSpeed;
	}*/
}
}