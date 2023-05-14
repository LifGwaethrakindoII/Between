using UnityEngine;

namespace VoidlessUtilities
{
public class AccelerableRigidbodyJumpingAbility : RigidbodyJumpingAbility, IAccelerable<Vector3>
{
	[Space(5f)]
	[SerializeField] private float _gravityMultiplier; 	/// <summary>Gravity Force's Multiplier.</summary>
	[SerializeField] private Accelerable _accelerable; 	/// <summary>Ability's Accelerable.</summary>
	private Vector3 _velocity; 							/// <summary>Ability's Velocity.</summary>

	/// <summary>Gets and Sets accelerable property.</summary>
	public Accelerable accelerable
	{
		get { return _accelerable; }
		set { _accelerable = value; }
	}

	/// <summary>Gets and Sets gravityMultiplier property.</summary>
	public float gravityMultiplier
	{
		get { return _gravityMultiplier; }
		set { _gravityMultiplier = value; }
	}

	/// <summary>Gets and Sets velocity property.</summary>
	public Vector3 velocity
	{
		get { return _velocity; }
		set { _velocity = value; }
	}

	/// <summary>Callback invoked when the Base's Ability get's Awake callback's called. Not obligatory to override.</summary>
	public override void OnAbilityAwake()
	{
		displacementSpeed = accelerable.maxSpeed;
	}

	/// <summary>Casts ability.</summary>
	public override void CastAbility()
	{

	}

	/// <summary>Overloaded. Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override void CastAbility(Vector3 _direction, Space _space = Space.Self)
	{//
		if(state == AbilityState.Available || state == AbilityState.Using)
		{
			if(accelerable.speed < accelerable.maxSpeed)
			{
				accelerable.Accelerate(Time.fixedDeltaTime);
				rigidbody.AddForce((_space == Space.Self ? transform.TransformDirection(_direction) : _direction) * accelerable.speed * Time.fixedDeltaTime, forceMode);
			}
			else this.ChangeState(AbilityState.Fatigue);
		}
		Debug.LogWarning("[AccelerableRigidbodyJumpingAbility] Ability currently unavailable...");
	}

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility()
	{
		rigidbody.AddForce(Physics.gravity * gravityMultiplier * Time.fixedDeltaTime, forceMode);
		accelerable.Accelerate();
	}

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override void ResetAbility()
	{
		accelerable.speed = accelerable.maxSpeed;
	}
}
}