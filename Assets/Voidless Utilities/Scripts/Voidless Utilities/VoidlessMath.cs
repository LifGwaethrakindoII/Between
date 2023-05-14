using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace VoidlessUtilities
{
[Flags]
public enum Easings
{
	Linear = 0, 																		/// <summary>Linear Easing.</summary>
	EaseIn = 1, 																		/// <summary>Ease-In Easing.</summary>
	EaseOut = 2, 																		/// <summary>Ease-Out Easing.</summary>
	Arc = 4, 																			/// <summary>Arc Easing.</summary>
	Sigmoid = 8, 																		/// <summary>Sigmoid Easing.</summary>

	EaseInOut = EaseIn | EaseOut 														/// <summary>Ease-In-Out Easing.</summary>
}

public enum CoordinatesModes 															/// <summary>Coordinates Modes.</summary>
{
	XY, 																				/// <summary>X and Y Coordinate Mode.</summary>
	YX, 																				/// <summary>Y and X Coordinate Mode.</summary>
	XZ, 																				/// <summary>X and Z Coordinate Mode.</summary>
	ZY, 																				/// <summary>Z and Y Coordinate Mode.</summary>
	YZ, 																				/// <summary>Y and Z Coordinate Mode.</summary>
	ZX 																					/// <summary>Z and X Coordinate Mode.</summary>
}

/// <summary>Normalized Property parametric function.</summary>
/// <param name="t">Time, normalized between -1f and 1f.</param>
public delegate float NormalizedPropertyFunctionOC(float t, float x = 0.0f);

public delegate float ParameterizedNormalizedPropertyFunctionOC(float t, float x);

public static class VoidlessMath
{
	public const float PHI = 1.61803398874989484820458683436563811772030917980576f; 	/// <summary>Golden Ratio Constant.</summary>
	public const float E = 2.71828182845904523536028747135266249775724709369995f; 		/// <summary>Euler's Number Constant</summary>

	/*[DllImport("[OC]VoidlessUtilities.dll", EntryPoint = "Factorial")]
    public static extern int Factorial(int n);

    public static int[] GetAllCombinationsPossible(int[] _elements, int _index, int _size)
    {
    	for(int i = _index; i < _elements.Length; i++)
    	{
    		
    	}
    }

    public static void (int[] _elements , int _index)
    {
    	
    }*/

#region NormalizedPropertyFunctionOCs:
	/*public static float Interpolate(float _initialPoint, float _finalPoint, float _t)
	{
		return _initialPoint + (_t * (_finalPoint - _initialPoint));
	}*/

	public static NormalizedPropertyFunctionOC GetEasing(Easings _easing)
	{
		switch(_easing)
		{
			case Easings.Linear: 	return null;
			case Easings.EaseIn: 	return EaseIn;
			case Easings.EaseOut: 	return EaseOut;
			case Easings.Arc: 		return Arc;
			case Easings.Sigmoid: 	return Sigmoid;
		}

		return null;
	}

	/// <summary>Calculates a number to a given exponential.</summary>
	/// <param name="t">Number to elevate to given exponent.</param>
	/// <param name="_exponential">Exponential to raise number to [2 by default].</param>
	/// <returns>Number raised to given exponential.</returns>
	public static float EaseIn(float t, float _exponential = 2.0f)
	{
		if(_exponential == 0.0f) return 1.0f;
		else if(_exponential == 1.0f) return t;
		else return Mathf.Pow(t, Mathf.Abs(_exponential));
	}

	/// <summary>Calculates a number to a given exponential.</summary>
	/// <param name="t">Number to elevate to given exponent.</param>
	/// <param name="_exponential">Exponential to raise number to [2 by default].</param>
	/// <returns>Number raised to the inverse of the given exponential.</returns>
	public static float EaseOut(float t, float _exponential = 2.0f)
	{
		return (1.0f - Mathf.Abs(EaseIn(t - 1.0f, _exponential)));
	}

	public static float EaseInEaseOut(float t, float _exponential = 2.0f)
	{
		return t < 0.5f ? EaseIn(t * 2.0f, _exponential) * 0.5f : (EaseOut(t * 2.0f - 1.0f, _exponential) * 0.5f) + 0.5f;
	}

	public static float EaseInEaseOut(float t, float _easeInExponential = 2.0f, float _easeOutExponential = 2.0f)
	{
		return t < 0.5f ? EaseIn(t * 2.0f, _easeInExponential) * 0.5f : (EaseOut(t * 2.0f - 1.0f, _easeOutExponential) * 0.5f) + 0.5f;
	}

	/// <summary>Calculates position of Arc of a given t.</summary>
	/// <param name="t">Current time.</param>
	/// <returns>Time relative to t value.</returns>
	public static float Arc(float t, float _x = 0.0f)
	{
		return (t * (1.0f - t));
	}

	/// <summary>Interpolates linearly an initial value to a final value, on a normalized time, following the formula P = P0 + t(Pf - P0).</summary>
	/// <param name="_initialPoint">Initial value [P0].</param>
	/// <param name="_finalPoint">Destiny value [Pf].</param>
	/// <param name="_time">Normalized t, clamped internally between -1 and 1.</param>
	/// <returns>Interpolated value on given normalized time.</returns>
	public static float Lerp(float _initialPoint, float _finalPoint, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return _initialPoint + (_time * (_finalPoint - _initialPoint)); 
	}

	/// <summary>Evaluates sigmoid function by given x.</summary>
	/// <param name="_x">Number to evaluate.</param>
	/// <param name="e">Exponential, 'e' constant by default.</param>
	/// <returns>Sigmoid evaluation.</returns>
	public static float Sigmoid(float _x, float e = E)
	{
		return (1.0f / (1.0f + (1.0f / Mathf.Pow(e, _x))));
	}

	public static float FunctionsSumatories(float _t, params NormalizedPropertyFunctionOC[] _functions)
	{
		float proportion = (1 / _functions.Length);

		for(int i = 0; i < (_functions.Length - 1); i++)
		{
			if((_t >= (proportion * i)) && (_t < (proportion * (i + 1)))) return _functions[i](_t);
		}

		return _t;
	}
#endregion

	/// <summary>Calculates the Logarithm of an odd function.</summary>
	/// <param name="x">Variable to calculate the Logot.</param>
	/// <param name="b">Base [2 by default].</param>
	/// <returns>Logot function.</returns>
	public static float Logot(float x, float b = 2.0f)
	{
		return Log((x / (1.0f - x)), b);
	}

	/// <summary>Calculates the logarithm of a given number.</summary>
	/// <param name="x">Number to calculate logarithm.</param>
	/// <param name="b">Logarithm's base [2 by default].</param>
	/// <returns>Logarithm of number.</returns>
	public static float Log(float x, float b = 2.0f)
	{
		float count = 0.0f;

		while(x > (b - 1.0f))
		{
			x /= b;
			count++;
		}

		return count;
	}

#region Vector3Utilities:
	/// <summary>Interpolates linearly an initial Vector3 to a final Vector3, on a normalized time, following the formula P = P0 + t(Pf - P0).</summary>
	/// <param name="_initialPoint">Initial Vector3 [P0].</param>
	/// <param name="_finalPoint">Destiny Vector3 [Pf].</param>
	/// <param name="_time">Normalized t, clamped internally between -1 and 1.</param>
	/// <returns>Interpolated Vector3 on given normalized time.</returns>
	public static Vector3 Lerp(Vector3 _initialPoint, Vector3 _finalPoint, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return _initialPoint + (_time * (_finalPoint - _initialPoint)); 
	}

	public static Vector3 SoomthStartN(Vector3 _initialPoint, Vector3 _finalPoint, int _exponential, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return _initialPoint + (Mathf.Pow(_time, _exponential) * (_finalPoint - _initialPoint));
	}

	public static Vector3 SoomthEndN(Vector3 _initialPoint, Vector3 _finalPoint, int _exponential, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return _initialPoint + ((1f - (1f - Mathf.Pow(_time, _exponential))) * (_finalPoint - _initialPoint));
	}

	/// <summary>Calculates a Linear Beizer Curve point relative to the time, following the formula [B(t) = (1-t)P0 + tPf].</summary>
	/// <param name="_initialPoint">Initial value [P0].</param>
	/// <param name="_finalPoint">Destiny value [Pf].</param>
	/// <param name="_time">Normalized t, clamped internally between -1 and 1.</param>
	/// <returns>Linear Beizer Curve point relative to given normalized time.</returns>
	public static Vector3 LinearBeizer(Vector3 _initialPoint, Vector3 _finalPoint, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return ((1.0f - _time) * _initialPoint) + (_time * _finalPoint);
	}

	/// \TODO Clean the following Beizer Curve functions to a formula that doesn't call Linear Beizer n times.
	/// <summary>Calculates a Cuadratic Beizer Curve point relative to the time, following the formula [B(P0,P1,P2,t) = (1-t)B(P0,P1,t) + tB(P1,P2,t)].</summary>
	/// <param name="_initialPoint">Initial value [P0].</param>
	/// <param name="_finalPoint">Destiny value [Pf].</param>
	/// <param name="_tangent">Tanget vector between initialPoint and finalPoint [P1].</param>
	/// <param name="_time">Normalized t, clamped internally between -1 and 1.</param>
	/// <returns>Cuadratic Beizer Curve point relative to given normalized time.</returns>
	public static Vector3 CuadraticBeizer(Vector3 _initialPoint, Vector3 _finalPoint, Vector3 _tangent, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return LinearBeizer(LinearBeizer(_initialPoint, _tangent, _time), LinearBeizer(_tangent, _finalPoint, _time), _time);
	}

	/// <summary>Calculates a Cubic Beizer Curve point relative to the time, following the formula [B(P0,P1,P2,t) = (1-t)B(P0,P1,t) + tB(P1,P2,t)].</summary>
	/// <param name="_initialPoint">Initial value [P0].</param>
	/// <param name="_finalPoint">Destiny value [Pf].</param>
	/// <param name="_startTangent">First tanget vector between initialPoint and finalPoint [P1].</param>
	/// <param name="_endTangent">Second tangent vector petween initialPoint and finalPoint [P2].</param>
	/// <param name="_time">Normalized t, clamped internally between -1 and 1.</param>
	/// <returns>Cubic Beizer Curve point relative to given normalized time.</returns>
	public static Vector3 CubicBeizer(Vector3 _initialPoint, Vector3 _finalPoint, Vector3 _startTangent, Vector3 _endTangent, float _time)
	{
		_time = Mathf.Clamp(_time, -1.0f, 1.0f);

		return LinearBeizer(CuadraticBeizer(_initialPoint, _startTangent, _endTangent, _time), CuadraticBeizer(_startTangent, _endTangent, _finalPoint, _time), _time);
	}

	/// <summary>Gets middle point between n number of points (positions).</summary>
	/// <param name="_points">The points from where the middle point will be calculated.</param>
	/// <returns>Middle point between n points.</returns>
	public static Vector3 GetMiddlePointBetween(params Vector3[] _points)
	{
		Vector3 middlePoint = Vector3.zero;

		for(int i = 0; i < _points.Length; i++)
		{
			middlePoint += _points[i];
		}

		return (middlePoint / _points.Length);
	}

	/// <summary>Gets normalized point between n number of points (positions).</summary>
	/// <param name="_normalizedValue">The normal of the points sumatory.</param>
	/// <param name="_points">The points from where the middle point will be calculated.</param>
	/// <returns>Normalized point between n points.</returns>
	public static Vector3 GetNormalizedPointBetween(float _normalizedValue, params Vector3[] _points)
	{
		Vector3 middlePoint = Vector3.zero;

		for(int i = 0; i < _points.Length; i++)
		{
			middlePoint += _points[i];
		}

		return (middlePoint * _normalizedValue.ClampValue(-1.0f, 1.0f));
	}

	public static float DotProductAngle(Vector3 a, Vector3 b)
	{
		return Mathf.Acos(Vector3.Dot(a, b) / (a.magnitude * b.magnitude));
	}

	/// <summary>Gets interpoilation's t, given an output.</summary>
	/// <param name="_initialInput">Interpolation's initial value [P0].</param>
	/// <param name="_finalInput">Interpolation's final value [Pf].</param>
	/// <param name="_output">Interpolation's Output.</param>
	/// <returns>T deducted from the given output and original interpolation's data.</returns>
	public static float GetInterpolationTime(float _initialInput, float _finalInput, float _output)
	{
		return ((_output - _initialInput) / (_finalInput - _initialInput));
	}
#endregion

#region Ray2DOperations:
	/// <summary>Calculates a 2X2 determinant, given two bidimensional Rays.</summary>
	/// <param name="_rayA">Ray A.</param>
	/// <param name="_rayB">Ray B.</param>
	/// <returns>2X2's determinant of Ray A and Ray B.</returns>
	public static float Determinant(Ray2D _rayA, Ray2D _rayB)
	{
		return ((_rayA.direction.y * _rayB.direction.x) - (_rayA.direction.x * _rayB.direction.y));
		//return ((_rayB.direction.x * _rayA.direction.y) - (_rayB.direction.y * _rayA.direction.x));
	}

	/// <summary>Interpolates ray towards direction, given a time t.</summary>
	/// <param name="_ray">Ray to interpolate.</param>
	/// <param name="t">Time reference.</param>
	/// <returns>Interpolation between Ray's origin and direction on t time, as a Vector2.</returns>
	public static Vector2 Lerp(this Ray2D _ray, float t)
	{
		return (_ray.origin + (t * _ray.direction));
	}

	/// <summary>Calculates for intersection between Ray A and B.</summary>
	/// <param name="_rayA">Ray A.</param>
	/// <param name="_rayB">Ray B.</param>
	/// <returns>Intersection between Rays A and B if there is, null otherwise.</returns>
	public static Vector2? CalculateIntersectionBetween(Ray2D _rayA, Ray2D _rayB)
	{
		float determinant = Determinant(_rayA, _rayB);
		if(determinant == 0.0f) return null;
		float determinantMultiplicativeInverse = (1.0f / determinant);
		float deltaX = (_rayA.origin.x - _rayB.origin.x);
		float deltaY = (_rayB.origin.y - _rayA.origin.y);
		float tA = ((deltaY * _rayB.direction.x) + (deltaX * _rayB.direction.y)) * determinantMultiplicativeInverse;
		float tB = ((deltaY * _rayA.direction.x) + (deltaX * _rayA.direction.y)) * determinantMultiplicativeInverse;

		return (tA >= 0.0f && tB >= 0.0f) ? _rayA.Lerp(tA) : (Vector2?)null;
	}
#endregion

#region RandomOperations:
	/// <summary>Gets a unique [not duplicate] set of random integers.</summary>
	/// <param name="_range">Random's Range [max gets excluded].</param>
	/// <param name="_count">Size of the set.</param>
	/// <returns>Set of random sorted unique integers.</returns>
	public static int[] GetUniqueRandomSet(Range<int> _range, int _count)
	{
		HashSet<int> numbersSet = new HashSet<int>();

		for(int i = (_range.max - _count); i < _range.max; i++)
		{
			if(!numbersSet.Add(UnityEngine.Random.Range(_range.min, (i + 1))))
			numbersSet.Add(i);
		}

		int[] result = numbersSet.ToArray();

		for(int i = (result.Length - 1); i > 0; i--)
		{
			int n = UnityEngine.Random.Range(_range.min, (i + 1));
			int x = result[n];
			result[n] = result[i];
			result[i] = x;			
		}

		return result;
	}

	/// <summary>Gets a unique [not duplicate] set of random integers, from 0 to given count.</summary>
	/// <param name="_count">Size of the set.</param>
	/// <returns>Set of random sorted unique integers.</returns>
	public static int[] GetUniqueRandomSet(int _count)
	{
		HashSet<int> numbersSet = new HashSet<int>();

		for(int i = 0; i < _count; i++)
		{
			if(!numbersSet.Add(UnityEngine.Random.Range(0, (i + 1))))
			numbersSet.Add(i);
		}

		int[] result = numbersSet.ToArray();

		for(int i = (result.Length - 1); i > 0; i--)
		{
			int n = UnityEngine.Random.Range(0, (i + 1));
			int x = result[n];
			result[n] = result[i];
			result[i] = x;			
		}

		return result;
	}
#endregion

	/// <summary>Calculates Rectified Linear Unit of given number.</summary>
	/// <param name="x">Unit to rectify.</param>
	/// <returns>Number rectified.</returns>
	public static float RectifiedLinearUnit(float x)
	{
		return (x >= 0.0f ? x : 0.0f);
	}

	/// <summary>Checks if a dot product between 2 vectors is between an angle of tolerance.</summary>
	/// <param name="a">Vector A.</param>
	/// <param name="b">Vector b.</param>
	/// <param name="degreeTolerance">Degree Tolerance.</param>
	/// <returns>True if the dot product between two vectors is between given tolerance angle.</returns>
	public static bool DotProductWithinAngle(Vector3 a, Vector3 b, float degreeTolerance)
	{
		float dot = Vector3.Dot(a, b);
		float angleToDot = Mathf.Cos(degreeTolerance * Mathf.Deg2Rad);

		return dot >= 0.0f ? dot >= angleToDot : dot <= angleToDot;
	}

#region NumberUtilities:
	/// <summary>Remaps given input from map into given range.</summary>
	/// <param name="_input">Input value to remap.</param>
	/// <param name="_map">Original values mapping.</param>
	/// <param name="_range">Range to map the input to.</param>
	/// <returns>Input mapped into given range.</returns>
	public static float RemapValue(float _input, FloatRange _map, FloatRange _range)
	{
		return (((_range.max - _range.min) * (_input - _map.min)) / (_map.max - _map.min)) + _range.min;
	}

	/// <summary>Remaps given input from map into given range.</summary>
	/// <param name="_input">Input value to remap.</param>
	/// <param name="_mapMin">Original values mapping's minimum value.</param>
	/// <param name="_mapMax">Original values mapping's maximum value.</param>
	/// <param name="_rangeMin">Range's minimum value.</param>
	/// <param name="_rangeMax">Range's maximum value.</param>
	/// <returns>Input mapped into given range.</returns>
	public static float RemapValue(float _input, float _mapMin, float _mapMax, float _rangeMin, float _rangeMax)
	{
		return (((_rangeMax - _rangeMin) * (_input - _mapMin)) / (_mapMax - _mapMin)) + _rangeMin;
	}

	/// <summary>Remaps given input from map into normalized range.</summary>
	/// <param name="_input">Input value to remap.</param>
	/// <param name="_mapMin">Original values mapping's minimum value.</param>
	/// <param name="_mapMax">Original values mapping's maximum value.</param>
	/// <returns>Input mapped into normalizedRange.</returns>
	public static float RemapValueToNormalizedRange(float _input, FloatRange _map)
	{
		return ((_input - _map.min) / (_map.max - _map.min));
	}

	/// <summary>Remaps given input from map into normalized range.</summary>
	/// <param name="_input">Input value to remap.</param>
	/// <param name="_map">Original values mapping.</param>
	/// <returns>Input mapped into normalizedRange.</returns>
	public static float RemapValueToNormalizedRange(float _input, float _mapMin, float _mapMax)
	{
		return ((_input - _mapMin) / (_mapMax - _mapMin));
	}

	/// <summary>Calculates the multiplicative inverse of a number, converting it to x => 1/x.</summary>
	/// <param name="_x">Number to convert ot its multiplicative inverse.</param>
	/// <returns>Multiplicative inverse of x.</returns>
	public static float MultiplicativeInverse(ref float _x)
	{
		return _x = (1f / _x);
	}

	/// <summary>Calculates the multiplicative inverse of a number, converting it to x => 1/x.</summary>
	/// <param name="_x">Number to convert ot its multiplicative inverse.</param>
	/// <returns>Multiplicative inverse of x.</returns>
	public static float MultiplicativeInverse(float _x)
	{
		return (1f / _x);
	}

	/// <summary>Sets Integer to clamped value.</summary>
	/// <param name="_int">Integer that will be clamped.</param>
	/// <param name="_min">Minimum value clamped.</param>
	/// <param name="_max">Maximum value clamped.</param>
	/// <returns>Integer clamped (as int).</returns>
	public static int ClampSet(ref int _int, int _min, int _max)
	{
		return _int = Mathf.Clamp(_int, _min, _max);
	}

	/// <summary>Sets float to clamped value.</summary>
	/// <param name="_float">Float that will be clamped.</param>
	/// <param name="_min">Minimum value clamped.</param>
	/// <param name="_max">Maximum value clamped.</param>
	/// <returns>Float clamped (as float).</returns>
	public static float ClampSet(ref float _float, float _min, float _max)
	{
		return _float = Mathf.Clamp(_float, _min, _max);
	}

	/// <summary>Clamps integer to a maximum value.</summary>
	/// <param name="x">Integer to clamp.</param>
	/// <param name="_max">Maximum value possible.</param>
	/// <returns>Clamped integer value.</returns>
	public static int ClampMax(int x, int _max)
	{
		return x > _max ? _max : x;
	}

	/// <summary>Clamps integer to a minimum value.</summary>
	/// <param name="x">Integer to clamp.</param>
	/// <param name="_min">Minimum value possible.</param>
	/// <returns>Clamped integer value.</returns>
	public static int ClampMin(int x, int _min)
	{
		return x < _min ? _min : x;
	}

	/// <summary>Clamps float to a maximum value.</summary>
	/// <param name="x">Float to clamp.</param>
	/// <param name="_max">Maximum value possible.</param>
	/// <returns>Clamped float value.</returns>
	public static float ClampMax(float x, float _max)
	{
		return x > _max ? _max : x;
	}

	/// <summary>Clamps float to a minimum value.</summary>
	/// <param name="x">Float to clamp.</param>
	/// <param name="_min">Minimum value possible.</param>
	/// <returns>Clamped float value.</returns>
	public static float ClampMin(float x, float _min)
	{
		return x < _min ? _min : x;
	}

	/// <summary>Calculates negative absolute of given number.</summary>
	/// <param name="x">Value to convert to negative absolute.</param>
	/// <returns>Number passed to negative absolute.</returns>
	public static float NegativeAbs(float x)
	{
		return (x < 0.0f ? x : (x * -1.0f));
	}	
#endregion

/// Already Defined on IRange's Interface
/*#region RangeOperations:
	/// <summary>Gets Range's Median.</summary>
	/// <param name="_range">Range to get median.</param>
	/// <returns>Range's Median.</returns>
	public static float GetMedian(this FloatRange _range)
	{
		return (_range.min + (_range.GetLength() * 0.5f));
	}

	/// <summary>Gets Range's Length [including the 0 as a value].</summary>
	/// <param name="_range">Range to measure.</param>
	/// <returns>Range's Length.</returns>
	public static float GetLength(this FloatRange _range)
	{
		return (_range.max - _range.min + 1f);
	}
#endregion*/

#region ComparissonUtilities:
	public static int Factorial(int x)
	{
		if(x <= 0) throw new Exception("No Value below 1 can be received");
		else if(x == 1) return 1;
		else return x * Factorial((x - 1));
	}

	/// <summary>Checks if to float values are equal [below or equal the difference tolerance].</summary>
	/// <param name="_f1">First float value.</param>
	/// <param name="_f2">Second float value.</param>
	/// <param name="_differenceTolerance">Difference's Tolerance between the two float values [Epsilon by default].</param>
	/// <returns>True if both float values are equal between epsilon's range.</returns>
	public static bool Equal(float _f1, float _f2, float _differenceTolerance = 0.0f)
	{
		return Mathf.Abs(_f1 - _f2) <= (_differenceTolerance == 0.0f ? Mathf.Epsilon : _differenceTolerance);
	}

	/// <summary>Checks if to float values are different [above or equal the difference tolerance].</summary>
	/// <param name="_f1">First float value.</param>
	/// <param name="_f2">Second float value.</param>
	/// <param name="_differenceTolerance">Difference's Tolerance between the two float values [Epsilon by default].</param>
	/// <returns>True if both float values are different between epsilon's range.</returns>
	public static bool Different(float _f1, float _f2, float _differenceTolerance = 0.0f)
	{
		return Mathf.Abs(_f1 - _f2) >= (_differenceTolerance == 0.0f ? Mathf.Epsilon : _differenceTolerance);
	}
#endregion

	/// <summary>Gets 360 system angle between 2 points.</summary>
	/// <param name="_fromPoint">Point from where the angle starts.</param>
	/// <param name="_toPoint">Point the origin point is pointing towards.</param>
	/// <param name="_coordinatesMode">Coordinates Mode.</param>
	/// <returns>360 range angle (as float).</returns>
	public static float Get360Angle(Vector3 _fromPoint, Vector3 _toPoint, CoordinatesModes _coordinatesMode)
	{
		Vector2 direction = Vector2.zero;

		switch(_coordinatesMode)
		{
			case CoordinatesModes.XY:
			direction = new Vector2((_fromPoint.x - _toPoint.x), (_fromPoint.y - _toPoint.y));
			break;

			case CoordinatesModes.YX:
			direction = new Vector2((_fromPoint.y - _toPoint.y), (_fromPoint.x - _toPoint.x));
			break;

			case CoordinatesModes.XZ:
			direction = new Vector2((_fromPoint.x - _toPoint.x), (_fromPoint.z - _toPoint.z));
			break;

			case CoordinatesModes.ZY:
			direction = new Vector2((_fromPoint.z - _toPoint.z), (_fromPoint.y - _toPoint.y));
			break;

			case CoordinatesModes.YZ:
			direction = new Vector2((_fromPoint.y - _toPoint.y), (_fromPoint.z - _toPoint.z));
			break;

			case CoordinatesModes.ZX:
			direction = new Vector2((_fromPoint.z - _toPoint.z), (_fromPoint.x - _toPoint.x));
			break;
		}

		return direction.y < 0f || direction.x < 0f &&direction.y < 0f ? (Mathf.Atan2(direction.y, direction.x) + (Mathf.PI * 2)) * Mathf.Rad2Deg : Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
	}
}
}