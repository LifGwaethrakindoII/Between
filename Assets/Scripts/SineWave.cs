using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class SineWave : MonoBehaviour
{
	public const float TWO_PI = 360.0f; 				/// <summary>2 Pi represented on degrees.</summary>

	[SerializeField] private int _pointsCount; 			/// <summary>Wave's points count.</summary>
	[SerializeField] private float _amplitude; 			/// <summary>Wave's Amplitude.</summary>
	[SerializeField] private float _frequency; 			/// <summary>Wave's Frequency.</summary>
	[SerializeField] private float _horizontalShift; 	/// <summary>Wave's Horizontal Shift.</summary>
	[SerializeField] private float _verticalShift; 		/// <summary>Wave's Vertical Shift.</summary>
	[SerializeField] private float _speed; 				/// <summary>Wave's Speed.</summary>
	private LineRenderer _lineRenderer; 				/// <summary>LineRenderer's Component.</summary>
	private Vector3 position;
	private float _time;

	/// <summary>Gets pointsCount property.</summary>
	public int pointsCount { get { return _pointsCount; } }

	/// <summary>Gets and Sets amplitude property.</summary>
	public float amplitude
	{
		get { return _amplitude; }
		set { _amplitude = value; }
	}

	/// <summary>Gets and Sets frequency property.</summary>
	public float frequency
	{
		get { return _frequency; }
		set { _frequency = value; }
	}

	/// <summary>Gets horizontalShift property.</summary>
	public float horizontalShift { get { return _horizontalShift; } }

	/// <summary>Gets verticalShift property.</summary>
	public float verticalShift { get { return _verticalShift; } }

	/// <summary>Gets speed property.</summary>
	public float speed { get { return _speed; } }

	/// <summary>Gets and Sets time property.</summary>
	public float time
	{
		get { return _time; }
		set { _time = value; }
	}

	/// <summary>Gets and Sets lineRenderer Component.</summary>
	public LineRenderer lineRenderer
	{ 
		get
		{
			if(_lineRenderer == null)
			{
				_lineRenderer = GetComponent<LineRenderer>();
			}
			return _lineRenderer;
		}
	}

	private void Awake()
	{
		lineRenderer.positionCount = pointsCount;
		time = 0.0f;
	}

	private void Update()
	{
		time = time < (TWO_PI * speed) ? time + Time.deltaTime * speed : 0.0f;

		for(int i = 0; i < lineRenderer.positionCount; i++)
		{
			position.x = i;
			position.y = amplitude * Mathf.Sin(frequency * (time + i - horizontalShift) * Mathf.Deg2Rad) + verticalShift;
			lineRenderer.SetPosition(i, transform.position + position);
		}
	}
}

/*
* y = A * Sin(F*(x - H)) + V
* 
* A = Amplitude
* F = Frequency
* H = Horizontal Shift
* V = Vertical Shift
*/