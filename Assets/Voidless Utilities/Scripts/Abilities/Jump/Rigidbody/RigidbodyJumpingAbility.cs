using UnityEngine;

namespace VoidlessUtilities
{
[RequireComponent(typeof(Rigidbody))]
public abstract class RigidbodyJumpingAbility : JumpingAbility, IRigidbodyAbility
{
	[SerializeField] private ForceMode _forceMode; 	/// <summary>Force Mode Applied.</summary>
	private Rigidbody _rigidbody; 					/// <summary>Rigidbody's Component.</summary>

	/// <summary>Gets and Sets rigidbody Component.</summary>
	public Rigidbody rigidbody
	{ 
		get
		{
			if(_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}
			return _rigidbody;
		}
		set { _rigidbody = value; }
	}

	/// <summary>Gets and Sets forceMode property.</summary>
	public ForceMode forceMode
	{
		get { return _forceMode; }
		set { _forceMode = value; }
	}

	/// <summary>Casts ability.</summary>
	public override abstract void CastAbility();

	/// <summary>Overloaded. Casts ability.</summary>
	/// <param name="_direction">Direction where this ability will be casted upon [as Vector3].</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public override abstract void CastAbility(Vector3 _direction, Space _space = Space.Self);

	/// <summary>Reposes ability.</summary>
	public override abstract void ReposeAbility();

	/// <summary>Accelerable Ability 's own reset method. Use this to reset its own respective attributes.	</summary>
	public override abstract void ResetAbility();
}
}