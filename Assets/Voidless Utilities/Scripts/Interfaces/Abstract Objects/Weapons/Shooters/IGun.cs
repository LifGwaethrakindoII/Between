using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IGun<T> : IProjectileWeapon<T> where T : IProjectile
{
	float fireRate { get; set; } 			/// <summary>Gun's Fire-Rate.</summary>
	float rechargeDuration { get; set; } 	/// <summary>Gun's Recharge Duration.</summary>
	float capacity { get; set; } 			/// <summary>Gun's Projectile Capacity.</summary>
}
}