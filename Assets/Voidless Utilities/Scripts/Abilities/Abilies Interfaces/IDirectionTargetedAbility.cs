using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IDirectionTargetedAbility<T> : IAbility
{
	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_origin">Origin's direction normal from the Observer.</param>
	/// <param name="_target">Target's Direction.</param>
	/// <param name="_speed">Direction change's speed.</param>
	/// <param name="_delta">Expected difference between the Target and the Observer.</param>
	/// <param name="_space">Relative to which space the direction will be.</param>
	void CastAbilityTowards(T _from,T _target, float _speed, float _delta, Space _space = Space.Self);
}
}