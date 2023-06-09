using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

namespace VoidlessUtilities.EditorNodes
{
[System.Serializable]
public class BaseConnectionPoint
{
	[SerializeField] private Rect _rect; 											/// <summary>Connection Point's Rect.</summary>
	[SerializeField] private List<Connection> _connections; 							/// <summary>Connection Point's Connections attached.</summary>
	[SerializeField] private PointTypes _pointType; 									/// <summary>Connection Point's type [whether Input or Output].</summary>
	[SerializeField] private PointAllignmentTypes _allignmentType; 					/// <summary>Connection Point's orientation type.</summary>
	[SerializeField] private Node _node; 												/// <summary>Node where this Connection Point belongs.</summary>
	[SerializeField] private GUIStyle _connectionStyle; 								/// <summary>Connection Point's GUIStyle.</summary>
	[SerializeField] private float _pointWidth; 										/// <summary>Connection Point's width.</summary>
	[SerializeField] private float _pointHeight; 									/// <summary>Connection Point's height.</summary>
	[SerializeField] private float _rectWidthOffset; 								/// <summary>Rect's width offset.</summary>
	[SerializeField] private float _rectHeightOffset; 								/// <summary>Rect's height offset.</summary>

	[SerializeField] private Action<BaseConnectionPoint> _OnClickConnectionPoint; 	/// <summary>Action called when this Connection Point is being clicked.</summary>

#region Getters/Setter:
	/// <summary>Gets and Sets rect property.</summary>
	public Rect rect
	{
		get { return _rect; }
		set { _rect = value; }
	}

	/// <summary>Gets and Sets connections property.</summary>
	public List<Connection> connections
	{
		get { return _connections; }
		set { _connections = value; }
	}

	/// <summary>Gets and Sets pointType property.</summary>
	public PointTypes pointType
	{
		get { return _pointType; }
		set { _pointType = value; }
	}

	/// <summary>Gets and Sets allignmentType property.</summary>
	public PointAllignmentTypes allignmentType
	{
		get { return _allignmentType; }
		set { _allignmentType = value; }
	}

	/// <summary>Gets and Sets node property.</summary>
	public Node node
	{
		get { return _node; }
		set { _node = value; }
	}

	/// <summary>Gets and Sets connectionStyle property.</summary>
	public GUIStyle connectionStyle
	{
		get { return _connectionStyle; }
		set { _connectionStyle = value; }
	}

	/// <summary>Gets and Sets OnClickConnectionPoint property.</summary>
	public Action<BaseConnectionPoint> OnClickConnectionPoint
	{
		get { return _OnClickConnectionPoint; }
		set { _OnClickConnectionPoint = value; }
	}

	/// <summary>Gets and Sets pointWidth property.</summary>
	public float pointWidth
	{
		get { return _pointWidth; }
		set { _pointWidth = value; }
	}

	/// <summary>Gets and Sets pointHeight property.</summary>
	public float pointHeight
	{
		get { return _pointHeight; }
		set { _pointHeight = value; }
	}

	/// <summary>Gets and Sets rectWidthOffset property.</summary>
	public float rectWidthOffset
	{
		get { return _rectWidthOffset; }
		set { _rectWidthOffset = value; }
	}

	/// <summary>Gets and Sets rectHeightOffset property.</summary>
	public float rectHeightOffset
	{
		get { return _rectHeightOffset; }
		set { _rectHeightOffset = value; }
	}
#endregion

	/// <summary>Default BaseConnectionPoint constructor.</summary>
	public BaseConnectionPoint()
	{
		//...
	}

	/// <summary>BaseConnectionPoint constructor.</summary>
	/// <param name="_node">Node where the Connection Point beloings.</param>
	/// <param name="_pointWidth">Connection Point's width.</param>
	/// <param name="_pointHeight">Connection Point's heigth.</param>
	/// <param name="_pointWidthOffset">Positioning Offset on the X axis relative to the Node's position.</param>
	/// <param name="_pointHeightOffset">Positioning Offset on the Y axis relative to the Node's position.</param>
	/// <param name="_pointAllingmentType">Type of allingment [whether Vertical or Horizontal].</param>
	/// <param name="_pointType">Type of Connection Point [Whether In or Out].</param>
	/// <param name="_connectionStyle">Connection Point's GUI Style.</param>
	/// <param name="onClickConnectionPoint">Action called when the Connection Point is clicked.</param>
	public BaseConnectionPoint(Node _node, float _pointWidth, float _pointHeight, float _rectWidthOffset, float _rectHeightOffset, PointAllignmentTypes _allignmentType, PointTypes _pointType, GUIStyle _connectionStyle, Action<BaseConnectionPoint> onClickConnectionPoint)
	{
		node = _node;
		allignmentType = _allignmentType;
		pointType = _pointType;
		connectionStyle = _connectionStyle;
		pointWidth = _pointWidth;
		pointHeight = _pointHeight;
		rectWidthOffset = _rectWidthOffset;
		rectHeightOffset = _rectHeightOffset;
		rect = new Rect(0, 0, pointWidth, pointHeight);
		OnClickConnectionPoint = onClickConnectionPoint;

		connections = new List<Connection>();
	}

	/// <summary>Draws connection on the Editor Window relative to this Node's position.</summary>
	public virtual void Draw()
	{
		Vector2 movePosition = Vector2.zero;

		switch(allignmentType)
		{
			case PointAllignmentTypes.Horizontal:
				movePosition.y = (node.rect.y + ((node.rect.height - rect.height) * (0.5f)));
				switch(pointType)
				{
					case PointTypes.In:
						movePosition.x = node.rect.x - (rect.width + rectWidthOffset);
					break;

					case PointTypes.Out:
						movePosition.x = node.rect.x + (node.rect.width - rectWidthOffset);
					break;

					default:
						Debug.LogWarning("[ConnectionPoint] No Point Type defined!");
					break;
				}
			break;

			case PointAllignmentTypes.Vertical:
				movePosition.x= (node.rect.x + ((node.rect.width - rect.width) * (0.5f)));
				switch(pointType)
				{
					case PointTypes.In:
						movePosition.y = node.rect.y - (rect.height + rectHeightOffset);
					break;

					case PointTypes.Out:
						movePosition.y = node.rect.y + (node.rect.height - rectHeightOffset);
					break;

					default:
						Debug.LogWarning("[ConnectionPoint] No Point Type defined!");
					break;
				}
			break;

			default:
			Debug.LogWarning("[ConnectionPoint] No Allingment Type defined!");
			break;
		}

		Rect updatedRect = new Rect(movePosition.x, movePosition.y, rect.width, rect.height);
		rect = updatedRect;

		if (GUI.Button(rect, "", connectionStyle))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
	}
}
}