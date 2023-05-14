using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoidlessUtilities
{
[CustomEditor(typeof(ThirdPersonCamera))]
public class ThirdPersonCameraInspector : Editor
{
	private static readonly string LABEL_REPOSITION_CAMERA = "Re-Position Camera."; 	/// <summary>Reposition camera's button label.</summary>

	private ThirdPersonCamera thirdPersonCamera; 										/// <summary>Inspector's Target.</summary>

	/// <summary>Sets target property.</summary>
	void OnEnable()
	{
		thirdPersonCamera = target as ThirdPersonCamera;
	}

	/// <summary>OnInspectorGUI override.</summary>
	public override void OnInspectorGUI()
	{	
		DrawDefaultInspector();
		if(thirdPersonCamera.ThirdPersonCharacter != null)
		if(GUILayout.Button(LABEL_REPOSITION_CAMERA))
		{
			thirdPersonCamera.CheckForFocusPoint();
			thirdPersonCamera.RepositionCamera();
		}
	}
}
}