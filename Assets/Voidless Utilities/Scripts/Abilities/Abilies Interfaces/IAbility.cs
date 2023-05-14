using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IAbility
{
	ulong abilityID { get; set; } 			/// <summary>Ability's ID.</summary>
	float cost { get; set; } 				/// <summary>Ability's cost.</summary>

	/// <summary>Casts Ability.</summary>
	void CastAbility();

	/// <summary>Reposes Ability.</summary>
	void ReposeAbility();

	/// <summary>Resets the Accelerable's Ability.</summary>
	void ResetAbility();
}
}