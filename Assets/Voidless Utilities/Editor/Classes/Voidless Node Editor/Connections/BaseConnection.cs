using System;
using UnityEditor;
using UnityEngine;

namespace VoidlessUtilities.EditorNodes
{
[System.Serializable]
public class BaseConnection
{
	[SerializeField] private Color _beizerLineColor; 					/// <summary>Beizer Line's color.</summary>
	[SerializeField] private float _beizerLineTangent; 					/// <summary>Beizer line's tangent, determines the smoothness of the line between Input and Output points.</summary>
	[SerializeField] private float _beizerLineWidth; 					/// <summary>Beizer line's width.</summary>
	[SerializeField] private float _handlesButtonSize; 					/// <summary>Handles's button size.</summary>
	[SerializeField] private float _handlesButtonPickSize; 				/// <summary>Handles's button pick size.</summary>
	[SerializeField] private ConnectionPoint _inPoint; 					/// <summary>Connection's Input Point.</summary>
	[SerializeField] private ConnectionPoint _outPoint; 				/// <summary>Connection's Output Point.</summary>
	[SerializeField] private PointAllignmentTypes _pointAllignmentType; /// <summary>How the connection draws the Beizer relative to the Node's allingment type.</summary>

	/// <summary>Action called when this Connection has to be removed.</summary>
	/// <param name="_connection">Connection [this] to be removed.</param>
	[SerializeField] private Action<BaseConnection> _OnClickRemoveConnection;

#region Getters/Setters:
	/// <summary>Gets and Sets beizerLineColor property.</summary>
	public Color beizerLineColor
	{
		get { return _beizerLineColor; }
		set { _beizerLineColor = value; }
	}

	/// <summary>Gets and Sets beizerLineTangent property.</summary>
	public float beizerLineTangent
	{
		get { return _beizerLineTangent; }
		set { _beizerLineTangent = value; }
	}

	/// <summary>Gets and Sets beizerLineWidth property.</summary>
	public float beizerLineWidth
	{
		get { return _beizerLineWidth; }
		set { _beizerLineWidth = value; }
	}

	/// <summary>Gets and Sets handlesButtonSize property.</summary>
	public float handlesButtonSize
	{
		get { return _handlesButtonSize; }
		set { _handlesButtonSize = value; }
	}

	/// <summary>Gets and Sets handlesButtonPickSize property.</summary>
	public float handlesButtonPickSize
	{
		get { return _handlesButtonPickSize; }
		set { _handlesButtonPickSize = value; }
	}

	/// <summary>Gets and Sets inPoint property.</summary>
	public ConnectionPoint inPoint
	{
		get { return _inPoint; }
		set { _inPoint = value; }
	}

	/// <summary>Gets and Sets outPoint property.</summary>
	public ConnectionPoint outPoint
	{
		get { return _outPoint; }
		set { _outPoint = value; }
	}

	/// <summary>Gets and Sets pointAllignmentType property.</summary>
	public PointAllignmentTypes pointAllignmentType
	{
		get { return _pointAllignmentType; }
		set { _pointAllignmentType = value; }
	}

	/// <summary>Gets and Sets OnClickRemoveConnection property.</summary>
	public Action<BaseConnection> OnClickRemoveConnection
	{
		get { return _OnClickRemoveConnection; }
		set { _OnClickRemoveConnection = value; }
	}
#endregion

	/// <summary>Default BaseConnection constructor.</summary>
	public BaseConnection()
	{
		//...
	}

	/// <summary>BaseConnection's constructor.</summary>
	/// <param name="_inPoint">BaseConnection;s Input Point.</param>
	/// <param name="_outPoint">BaseConnection's Output Point.</param>
	/// <param name="onClickRemoveConnection">Action called when this BaseConnection has to be removed.</param>
	public BaseConnection(ConnectionPoint _inPoint, ConnectionPoint _outPoint, Action<BaseConnection> onClickRemoveConnection)
	{
		inPoint = _inPoint;
		outPoint = _outPoint;
		beizerLineColor = Color.white;
		OnClickRemoveConnection = onClickRemoveConnection;
	}

	/// <summary>BaseConnection's constructor.</summary>
	/// <param name="_inPoint">BaseConnection;s Input Point.</param>
	/// <param name="_outPoint">BaseConnection's Output Point.</param>
	/// <param name="_pointAllignmentType">How the Beizer will be oriented.</param>
	/// <param name="_beizerLineColor">Color of the Beizer Line.</param>
	/// <param name="_beizerLineTangent">Beizer line's tangent.</param>
	/// <param name="_beizerLineWidth">Beizerline's width.</param>
	/// <param name="_handlesButtonSize">Handles's button size.</param>
	/// <param name="_handlesButtonPickSize">Handles's button pick size.</param>
	/// <param name="onClickRemoveConnection">Action called when this BaseConnection has to be removed.</param>
	public BaseConnection(ConnectionPoint _inPoint, ConnectionPoint _outPoint, PointAllignmentTypes _pointAllignmentType, Color _beizerLineColor, float _beizerLineTangent, float _beizerLineWidth, float _handlesButtonSize, float _handlesButtonPickSize,Action<BaseConnection> onClickRemoveConnection)
	{
		inPoint = _inPoint;
		outPoint = _outPoint;
		pointAllignmentType = _pointAllignmentType;
		beizerLineColor = _beizerLineColor;
		beizerLineTangent = _beizerLineTangent;
		beizerLineWidth = _beizerLineWidth;
		handlesButtonSize = _handlesButtonSize;
		handlesButtonPickSize = _handlesButtonPickSize;
		OnClickRemoveConnection = onClickRemoveConnection;
	}

	/// <summary>Draws Connection between Input Point and Output Point.</summary>
	public virtual void Draw()
	{
		switch(pointAllignmentType)
		{
			case PointAllignmentTypes.Vertical:
				Handles.DrawBezier
				(
					inPoint.rect.center,
					outPoint.rect.center,
					(inPoint.rect.center + (Vector2.up * beizerLineTangent)),
					(outPoint.rect.center - (Vector2.up * beizerLineTangent)),
					beizerLineColor,
					null,
					beizerLineWidth
				);
			break;

			case PointAllignmentTypes.Horizontal:
				Handles.DrawBezier
				(
					inPoint.rect.center,
					outPoint.rect.center,
					(inPoint.rect.center + (Vector2.left * beizerLineTangent)),
					(outPoint.rect.center - (Vector2.left * beizerLineTangent)),
					beizerLineColor,
					null,
					beizerLineWidth
				);
			break;
		}
		

		if(Handles.Button( ((inPoint.rect.center + outPoint.rect.center) * 0.5f), Quaternion.identity, handlesButtonSize, handlesButtonPickSize, Handles.RectangleCap ))
		{
			if(OnClickRemoveConnection != null) OnClickRemoveConnection(this);
		}
	}
}
}