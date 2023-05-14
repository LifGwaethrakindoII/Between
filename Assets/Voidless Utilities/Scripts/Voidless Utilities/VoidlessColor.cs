using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessColor
{
	/// <summary>Sets Color Alpha.</summary>
	/// <param name="_color">The Color that will have its Alpha modified.</param>
	/// <param name="_alpha">Updated Color Alpha Component.</param>
	/// <returns>New modified Color.</returns>
	public static Color WithAlpha(this Color _color, float _alpha)
	{
		return _color = new Color(_color.r, _color.g, _color.b, _alpha.ClampValue(-1.0f, 1.0f));
	}

	/// <summary>Sets Color Red.</summary>
	/// <param name="_color">The Color that will have its Red modified.</param>
	/// <param name="_red">Updated Color Red Component.</param>
	/// <returns>New modified Color.</returns>
	public static Color WithRed(this Color _color, float _red)
	{
		return _color = new Color(_red.ClampValue(-1.0f, 1.0f), _color.g, _color.b, _color.a);
	}

	/// <summary>Sets Color Green.</summary>
	/// <param name="_color">The Color that will have its Green modified.</param>
	/// <param name="_green">Updated Color Green Component.</param>
	/// <returns>New modified Color.</returns>
	public static Color WithtGreen(this Color _color, float _green)
	{
		return _color = new Color(_color.r, _green.ClampValue(-1.0f, 1.0f), _color.b, _color.a);
	}

	/// <summary>Sets Color Blue.</summary>
	/// <param name="_color">The Color that will have its Blue modified.</param>
	/// <param name="_blue">Updated Color Blue Component.</param>
	/// <returns>New modified Color.</returns>
	public static Color WithBlue(this Color _color, float _blue)
	{
		return _color = new Color(_color.r, _color.g, _blue.ClampValue(-1.0f, 1.0f), _color.a);
	}

	/// <summary>Sets Color Alpha.</summary>
	/// <param name="_color">The Color that will have its Alpha modified.</param>
	/// <param name="_alpha">Updated Color Alpha Component.</param>
	public static void SetAlpha(ref Color _color, float _alpha)
	{
		_color = new Color(_color.r, _color.g, _color.b, _alpha.ClampValue(-1.0f, 1.0f));
	}

	/// <summary>Sets Color Red.</summary>
	/// <param name="_color">The Color that will have its Red modified.</param>
	/// <param name="_red">Updated Color Red Component.</param>
	public static void SetRed(ref Color _color, float _red)
	{
		_color = new Color(_red.ClampValue(-1.0f, 1.0f), _color.g, _color.b, _color.a);
	}

	/// <summary>Sets Color Green.</summary>
	/// <param name="_color">The Color that will have its Green modified.</param>
	/// <param name="_green">Updated Color Green Component.</param>
	public static void SetGreen(ref Color _color, float _green)
	{
		_color = new Color(_color.r, _green.ClampValue(-1.0f, 1.0f), _color.b, _color.a);
	}

	/// <summary>Sets Color Blue.</summary>
	/// <param name="_color">The Color that will have its Blue modified.</param>
	/// <param name="_blue">Updated Color Blue Component.</param>
	public static void SetBlue(ref Color _color, float _blue)
	{
		_color = new Color(_color.r, _color.g, _blue.ClampValue(-1.0f, 1.0f), _color.a);
	}
}
}