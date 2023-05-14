using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class GUIDebbuger : MonoBehaviour
{
	[SerializeField] private Rect _GUIBox; 				/// <summary>GUI Box's Attributes.</summary>
	[SerializeField] private bool _debug; 				/// <summary>Debug on GUI?.</summary>
	private string _GUIBoxContent; 						/// <summary>GUI Box's Content.</summary>

	/// <summary>Gets and Sets GUIBox property.</summary>
	public Rect GUIBox
	{
		get { return _GUIBox; }
		set { _GUIBox = value; }
	}

	/// <summary>Gets and Sets GUIBoxContent property.</summary>
	public string GUIBoxContent
	{
		get { return _GUIBoxContent; }
		set { _GUIBoxContent = value; }
	}

	/// <summary>Gets and Sets debug property.</summary>
	public bool debug
	{
		get { return _debug; }
		set { _debug = value; }
	}

#region UnityMethods:
	void OnGUI()
	{
		if(debug)
		GUI.Box(GUIBox, GUIBoxContent);
	}
#endregion
}
}