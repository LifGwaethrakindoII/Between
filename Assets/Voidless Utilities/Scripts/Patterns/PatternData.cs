using System.Collections;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
[System.Serializable]
[XmlRoot(Namespace="VoidlessUtilities", IsNullable = false)]
public struct PatternData
{
	[SerializeField][XmlElement("Head_Orientation")] public OrientationSemantics headOrientation; 					/// <summary>Head's Orientation.</summary>
	[SerializeField][XmlElement("Left_Hand_Orientation")] public OrientationSemantics leftHandOrientation; 			/// <summary>LeftHand's Orientation.</summary>
	[SerializeField][XmlElement("Left_Paddle_Orientation")] public OrientationSemantics leftPaddleOrientation; 		/// <summary>Left Paddle's Orientation.</summary>
	[SerializeField][XmlElement("Right_Hand_Orientation")] public OrientationSemantics rightHandOrientation; 		/// <summary>Right Hand's Orientation.</summary>
	[SerializeField][XmlElement("Right_Paddle_Orientation")] public OrientationSemantics rightPaddleOrientation; 	/// <summary>Right Paddle's Orientation.</summary>

	/// <summary>Equals operator.</summary>
	public static bool operator == (PatternData a, PatternData b)
	{
		return (a.headOrientation.HasFlag(b.headOrientation)
		&& a.leftHandOrientation.HasFlag(b.leftHandOrientation)
		&& a.leftPaddleOrientation.HasFlag(b.leftPaddleOrientation)
		&& a.rightHandOrientation.HasFlag(b.rightHandOrientation)
		&& a.rightPaddleOrientation.HasFlag(b.rightPaddleOrientation));
	}

	/// <summary>Not Equal operator.</summary>
	public static bool operator != (PatternData a, PatternData b)
	{
		return (!a.headOrientation.HasFlag(b.headOrientation)
		|| !a.leftHandOrientation.HasFlag(b.leftHandOrientation)
		|| !a.leftPaddleOrientation.HasFlag(b.leftPaddleOrientation)
		|| !a.rightHandOrientation.HasFlag(b.rightHandOrientation)
		|| !a.rightPaddleOrientation.HasFlag(b.rightPaddleOrientation));
	}

	public PatternData(OrientationSemantics _headOrientation, OrientationSemantics _leftHandOrientation, OrientationSemantics _leftPaddleOrientation, OrientationSemantics _rightHandOrientation, OrientationSemantics _rightPaddleOrientation)
	{
		this.headOrientation = _headOrientation;
		this.leftHandOrientation = _leftHandOrientation;
		this.leftPaddleOrientation = _leftPaddleOrientation;
		this.rightHandOrientation = _rightHandOrientation;
		this.rightPaddleOrientation = _rightPaddleOrientation;
	}

	/// <summary>Compares PatterData against other PatternData.</summary>
	/// <param name="_data">PatternData to compare.</param>
	/// <returns>True if this PatternData's values has the same bit flags than the PatterData to compare. False Otherwise.</returns>
	public bool Equals(PatternData _data)
	{
		return (headOrientation.HasFlag(_data.headOrientation)
		&& leftHandOrientation.HasFlag(_data.leftHandOrientation)
		&& leftPaddleOrientation.HasFlag(_data.leftPaddleOrientation)
		&& rightHandOrientation.HasFlag(_data.rightHandOrientation)
		&& rightPaddleOrientation.HasFlag(_data.rightPaddleOrientation));
	}

	/// <returns>String representing all Orientation semantics of the User's joints.</returns>
	public override string ToString()
	{
		StringBuilder builder = new StringBuilder();

		builder.Append("Head Orientation: ");
		builder.AppendLine(headOrientation.ToString());
		builder.Append("Left Hand Orientation: ");
		builder.AppendLine(leftHandOrientation.ToString());
		builder.Append("Left Paddle Orientation: ");
		builder.AppendLine(leftPaddleOrientation.ToString());
		builder.Append("Right Hand Orientation: ");
		builder.AppendLine(rightHandOrientation.ToString());
		builder.Append("Right Paddle Orientation: ");
		builder.AppendLine(rightPaddleOrientation.ToString());

		return builder.ToString();
	}
}
}