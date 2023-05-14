using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorSwapEffect : MonoBehaviour
{
	[SerializeField] private Material _material; 	/// <summary>Effect's Material.</summary>
	private Camera _camera;

	/// <summary>Gets material property.</summary>
	public Material material { get { return _material; } }

	/// <summary>Gets and Sets camera Component.</summary>
	public Camera camera
	{ 
		get
		{
			if(_camera == null)
			{
				_camera = GetComponent<Camera>();
			}
			return _camera;
		}
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if(material != null)
        Graphics.Blit(src, dest, material);
    }
}
