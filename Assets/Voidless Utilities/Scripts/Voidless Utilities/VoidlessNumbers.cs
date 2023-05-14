using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessNumbers
{
	/// <summary>Gets clamped value (int).</summary>
	/// <param name="_value">Value that will be clamped.</param>
	/// <param name="_min">Minimum value clamped.</param>
	/// <param name="_max">Maximum value clamped.</param>
	/// <returns>New Value clamped (as int).</returns>
	public static int ClampValue(this int _value, int _min, int _max)
	{
		return _value = Mathf.Clamp(_value, _min, _max);
	}

	/// <summary>Gets clamped value (float).</summary>
	/// <param name="_value">Value that will be clamped.</param>
	/// <param name="_min">Minimum value clamped.</param>
	/// <param name="_max">Maximum value clamped.</param>
	/// <returns>New Value clamped (as float).</returns>
	public static float ClampValue(this float _value, float _min, float _max)
	{
		return _value = Mathf.Clamp(_value, _min, _max);
	}
}
}