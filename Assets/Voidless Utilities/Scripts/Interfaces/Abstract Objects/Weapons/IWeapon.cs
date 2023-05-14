using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IWeapon
{
	IWeaponBearer bearer { get; set; } 	/// <summary>Weapon's Bearer.</summary>

	/// <summary>Default Weapon use activator.</summary>
	void UseWeapon();
}
}