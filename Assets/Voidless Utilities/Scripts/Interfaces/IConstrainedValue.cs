using System;

namespace VoidlessUtilities
{
public interface IConstrainedValue<T> where T : IComparable<T>
{
	T current { get; set; } 	/// <summary>Current's Value.</summary>
	T min { get; set; } 		/// <summary>Minimum's Value.</summary>
	T max { get; set; } 		/// <summary>Maximum's Value.</summary>
}
}