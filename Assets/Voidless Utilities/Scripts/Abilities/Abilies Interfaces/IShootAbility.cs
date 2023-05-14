using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IShootAbility : IDirectionTargetedAbility<Vector3>
{
	/// <summary>Casts Direction Targeted's Ability.</summary>
	/// <param name="_from">Origin point to shoot.</param>
	/// <param name="_proyectile">Projectile to shoot.</param>
	/// <param name="_target">Target's Direction.</param>
	void CastAbility(IProjectile _projectile, Vector3 _from, Vector3 _direction);
}
}