using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface ICooldownAbility : IAbility
{
	float cooldownDuration { get; set; } 	/// <summary>Ability's Cooldown duration.</summary>

	/// <summary>Activates Cooldown's routine.</summary>
	IEnumerator Cooldown();
}
}