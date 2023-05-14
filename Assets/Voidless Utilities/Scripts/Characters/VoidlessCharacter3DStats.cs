using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[CreateAssetMenu(menuName = VoidlessString.PATH_SCRIPTABLE_OBJECTS + " / Voidless Character's Stats")]
public class VoidlessCharacter3DStats : ScriptableObject
{

#region Properties:
	[Header("Movement Stats:")]
	[SerializeField] private Accelerable _movement; 		/// <summary>Movement Accelerable's Attributes.</summary>
	[Space(5f)]
	[Header("Jump Stats:")]
	[SerializeField] private Accelerable _jump; 			/// <summary>Jump Accelerable's Attributes.</summary>
	[SerializeField] private LayerMask[] _jumpableLayers; 	/// <summary>Jumpable layer masks.</summary>
	[SerializeField] private LayerMask[] _walkableLayers; 	/// <summary>Walkable layer masks.</summary>
#endregion

#region Getters:
	/// <summary>Gets movement property.</summary>
	public Accelerable movement { get { return _movement; } }

	/// <summary>Gets jump property.</summary>
	public Accelerable jump { get { return _jump; } }

	/// <summary>Gets jumpableLayers property.</summary>
	public LayerMask[] jumpableLayers { get { return _jumpableLayers; } }

	/// <summary>Gets walkableLayers property.</summary>
	public LayerMask[] walkableLayers { get { return _walkableLayers; } }
#endregion

}
}