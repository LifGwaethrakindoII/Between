using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
/// <summary>Event invoked when the projectile hits something.</summary>
/// <param name="_target">Target that was hit.</param>
public delegate void OnProjectileHit(GameObject _target);

/// <summary>Event invoked when the projectile hits a set of objects.</summary>
/// <param name="_targets">Targets that were hit.</param>
public delegate void OnProjectileHits(GameObject[] _targets);

public interface IProjectile
{
	event OnProjectileHit onProjectileHit; 		/// <summary>OnProjectileHit's delegate event.</summary>
	event OnProjectileHits onProjectileHits; 	/// <summary>OnProjectileHits' delegate event.</summary>

	float speed { get; set; } 					/// <summary>Projectile's Speed.</summary>
}
}