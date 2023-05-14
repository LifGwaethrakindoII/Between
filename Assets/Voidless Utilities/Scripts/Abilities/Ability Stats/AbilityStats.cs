using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[Serializable]
public class AbilityStats
{
	[XmlAttribute("effectiveness")]
	public float effectiveness;

	/// <summary>AbilityStats default constructor.</summary>
	public AbilityStats()
	{
		
	}

	/// <summary>AbilityStats destructor.</summary>
	~AbilityStats()
	{
		
	}
}
}