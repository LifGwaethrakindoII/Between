using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public static class VoidlessRect
{
	public static Rect WithX(this Rect _rect, float _x)
	{
		Rect result = _rect;
		result.x = _x;

		return result;
	}

	public static Rect WithY(this Rect _rect, float _y)
	{
		Rect result = _rect;
		result.y = _y;
		
		return result;
	}

	public static Rect WithWidth(this Rect _rect, float _width)
	{
		Rect result = _rect;
		result.width = _width;
		
		return result;
	}

	public static Rect WithHeight(this Rect _rect, float _height)
	{
		Rect result = _rect;
		result.height = _height;
		
		return result;
	}

	public static Rect WithAddedX(this Rect _rect, float _x)
	{
		Rect result = _rect;
		result.x += _x;

		return result;
	}

	public static Rect WithAddedY(this Rect _rect, float _y)
	{
		Rect result = _rect;
		result.y += _y;
		
		return result;
	}

	public static Rect WithAddedWidth(this Rect _rect, float _width)
	{
		Rect result = _rect;
		result.width += _width;
		
		return result;
	}

	public static Rect WithAddedHeight(this Rect _rect, float _height)
	{
		Rect result = _rect;
		result.height += _height;
		
		return result;
	}
}
}