using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class ShootAbility : Ability, IShootAbility
{
	/// <summary>Casts ability.</summary>
	public override void CastAbility() { }

	/// <summary>Reposes ability.</summary>
	public override void ReposeAbility() { }

	/// <summary>Resets the Accelerable's Ability.</summary>
	public override void ResetAbility() { }

	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_origin">Origin's direction normal from the Observer.</param>
	/// <param name="_target">Target's Direction.</param>
	/// <param name="_speed">Direction change's speed.</param>
	/// <param name="_delta">Expected difference between the Target and the Observer.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	public virtual void CastAbilityTowards(Vector3 _from, Vector3 _target, float _speed, float _delta, Space _space = Space.Self) {  }

	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_from">Origin point to shoot.</param>
	/// <param name="_proyectile">Projectile to shoot.</param>
	/// <param name="_target">Target's Direction.</param>
	public virtual void CastAbility(IProjectile _projectile, Vector3 _from, Vector3 _direction) {  }

	public virtual T CastAbilityAndReturn<T>(T _projectile, Vector3 _from, Quaternion _direction) where T : MonoBehaviour, IProjectile, IPoolObject
	{
		if(state == AbilityState.Available || state == AbilityState.Using)
		{
			T projectile = ObjectPool.Instance.RecyclePoolObject(_projectile, _from, _direction);
			this.ChangeState(AbilityState.Fatigue);
			return projectile;
		}
		else return null;
	}
}
}