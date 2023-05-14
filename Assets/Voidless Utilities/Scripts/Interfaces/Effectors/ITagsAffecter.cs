using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public interface ITagsAffecter
{
	string[] affectedTags { get; set; } 	/// <summary>Affected Tags.</summary>
}
}