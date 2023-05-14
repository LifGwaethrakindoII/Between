using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface IWeaponBearer
{
	IWeapon weapon { get; set; } 	/// <summary>Bearer's Weapon.</summary>

	/// <summary>Default Weapon use activator.</summary>
	void UseWeapon();
}
}