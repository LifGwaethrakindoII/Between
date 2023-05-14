using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IProjectileWeapon<T> : IWeapon where T : IProjectile
{
	T projectile { get; set; } 			/// <summary>Shooter's Projectile.</summary>
	IShootAbility shoot { get; set; } 	/// <summary>Projectile Weapon's Shoot Ability.</summary>
}
}