using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoidlessUtilities
{
public class VoidlessEditorDataWindow : EditorWindow
{
	protected const string VOIDLESSEDITORDATAWINDOW_PATH = "Voidless Tools/Editor's Data"; 	/// <summary>VoidlessEditorDataWindow's path.</summary>

	public static VoidlessEditorDataWindow voidlessEditorDataWindow; 						/// <summary>VoidlessEditorDataWindow's static reference</summary>
	private static SerializedProperty editorDictionary;

	/// <summary>Creates a new VoidlessEditorDataWindow window.</summary>
	/// <returns>Created VoidlessEditorDataWindow window.</summary>
	[MenuItem(VOIDLESSEDITORDATAWINDOW_PATH)]
	public static VoidlessEditorDataWindow CreateVoidlessEditorDataWindow()
	{
		voidlessEditorDataWindow = GetWindow<VoidlessEditorDataWindow>("Editor's Data");
		return voidlessEditorDataWindow;
	}

	/// <summary>Use OnGUI to draw all the controls of your window.</summary>
	private void OnGUI()
	{
		VoidlessEditorData.ShowDictionary();
	}
}
}