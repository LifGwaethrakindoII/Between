using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
/// <summary>Event invoked when a Pool Object is beong deactivated.</summary>
/// <param name="_poolObject">Pool Object that has been deactivated.</param>
public delegate void OnPoolObjectDeactivation(IPoolObject _poolObject);

public interface IPoolObject
{
	event OnPoolObjectDeactivation onPoolObjectDeactivation; 	/// <summary>Event invoked when this Pool Object is being deactivated.</summary>

	int poolDictionaryID { get; set; } 							/// <summary>Key's ID of this Pool Object on its respectrive pool dictionary.</summary>
	bool dontDestroyOnLoad { get; set; } 						/// <summary>Is this Pool Object going to be destroyed when changing scene? [By default it destroys it].</summary>
	bool active { get; set; } 									/// <summary>Is this Pool Object active [preferibaly unavailable to recycle]?.</summary>

	/// <summary>Independent Actions made when this Pool Object is being created.</summary>
	void OnObjectCreation();

	/// <summary>Actions made when this Pool Object is being reseted.</summary>
	void OnObjectReset();

	/// <summary>Actions made when this Pool Object is being destroyed.</summary>
	void OnObjectDestruction();
}
}