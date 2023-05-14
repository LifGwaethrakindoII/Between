using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public abstract class BaseCamera : MonoBehaviour
{
	[SerializeField] private Transform _focusPoint; 			/// <summary>Camera's Focus Point.</summary>
	[Space(5f)]
	[SerializeField] private Vector3 _gravity; 					/// <summary>Gravity's direction vector.</summary>
	[SerializeField] private Axes2D _ignoreAxes; 	/// <summary>Axes to ignore during camera following a target.</summary>
	[Space(5f)]
	[SerializeField] private CameraGrid _cameraGridAttributes; 	/// <summary>Camera Grid's Attributes.</summary>
	[Space(5f)]
	[SerializeField] private float _distance; 					/// <summary>Distance from focus point and Camera.</summary>
	[Space(5f)]
	[Header("Shake Attributes:")]
	[SerializeField] private float _shakeStrength; 				/// <summary>Shake's strength.</summary>
	[SerializeField] private float _shakeDuration; 				/// <summary>Shaking's duration.</summary>
	protected CameraViewportPlane cameraGridPlane; 				/// <summary>Camera Grid's viewport plane.</summary>
	protected CameraViewportPlane nearPlane; 					/// <summary>Camera's near viewport plane.</summary>
	protected CameraViewportPlane farPlane; 					/// <summary>Camera's far viewport plane.</summary>
	private Behavior _cameraEffect; 							/// <summary>Coroutine controller for the actual Camera's effect.</summary>
	private Vector3 _relativeUp; 								/// <summary>Forward normal relative to the gravity's vector.</summary>
	private Vector3 _relativeForward; 							/// <summary>Forward normal relative to the cross product of the gravity and the right vector.</summary>
	private Vector3 _relativeNormal;
	private Camera _camera; 									/// <summary>Camera's Component.</summary>

#if UNITY_EDITOR
	[Space(5f)]
	[Header("Gizmo's Attributes:")]
	[SerializeField] private bool _drawWhenSelected; 			/// <summary>Draw Gizmos only when selected?.</summary>
	[SerializeField] private Color _onRangeColor; 				/// <summary>Gizmos' color if focus point is on range.</summary>
	[SerializeField] private Color _offRangeColor; 				/// <summary>Gizmos' color if focus point is off range.</summary>
	[SerializeField] private float _normalLength; 				/// <summary>Gizmoss normal ray length.</summary>
#endif

#region Getters/Setters:
	/// <summary>Gets and Sets focusPoint property.</summary>
	public Transform focusPoint
	{
		get { return _focusPoint; }
		set { _focusPoint = value; }
	}

	/// <summary>Gets and Sets distance property.</summary>
	public float distance
	{
		get { return _distance; }
		set { _distance = value; }
	}

	/// <summary>Gets and Sets shakeStrength property.</summary>
	public float shakeStrength
	{
		get { return _shakeStrength; }
		set { _shakeStrength = value; }
	}

	/// <summary>Gets and Sets shakeDuration property.</summary>
	public float shakeDuration
	{
		get { return _shakeDuration; }
		set { _shakeDuration = value; }
	}

	/// <summary>Gets and Sets ignoreAxes property.</summary>
	public Axes2D ignoreAxes
	{
		get { return _ignoreAxes; }
		set { _ignoreAxes = value; }
	}

	/// <summary>Gets and Sets cameraGridAttributes property.</summary>
	public CameraGrid cameraGridAttributes
	{
		get { return _cameraGridAttributes; }
		set { _cameraGridAttributes = value; }
	}

	/// <summary>Gets and Sets CameraGridPlane property.</summary>
	public CameraViewportPlane CameraGridPlane
	{
		get { return cameraGridPlane; }
		set { cameraGridPlane = value; }
	}

	/// <summary>Gets and Sets NearPlane property.</summary>
	public CameraViewportPlane NearPlane
	{
		get { return nearPlane; }
		set { nearPlane = value; }
	}

	/// <summary>Gets and Sets FarPlane property.</summary>
	public CameraViewportPlane FarPlane
	{
		get { return farPlane; }
		set { farPlane = value; }
	}

	/// <summary>Gets and Sets cameraEffect property.</summary>
	public Behavior cameraEffect
	{
		get { return _cameraEffect; }
		set { _cameraEffect = value; }
	}

	/// <summary>Gets and Sets gravity property.</summary>
	public Vector3 gravity
	{
		get { return _gravity; }
		set { _gravity = value; }
	}

	/// <summary>Gets and Sets relativeUp property.</summary>
	public Vector3 relativeUp
	{
		get { return _relativeUp; }
		set { _relativeUp = value; }
	}

	/// <summary>Gets and Sets relativeForward property.</summary>
	public Vector3 relativeForward
	{
		get { return _relativeForward; }
		set { _relativeForward = value; }
	}

	/// <summary>Gets and Sets relativeNormal property.</summary>
	public Vector3 relativeNormal
	{
		get { return _relativeNormal; }
		set { _relativeNormal = value; }
	}

	/// <summary>Gets and Sets camera Component.</summary>
	public new Camera camera
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
#endregion

#if UNITY_EDITOR
	/// <summary>Gets and Sets drawWhenSelected property.</summary>
	public bool drawWhenSelected
	{
		get { return _drawWhenSelected; }
		set { _drawWhenSelected = value; }
	}

	/// <summary>Gets and Sets onRangeColor property.</summary>
	public Color onRangeColor
	{
		get { return _onRangeColor; }
		set { _onRangeColor = value; }
	}

	/// <summary>Gets and Sets offRangeColor property.</summary>
	public Color offRangeColor
	{
		get { return _offRangeColor; }
		set { _offRangeColor = value; }
	}

	/// <summary>Gets and Sets normalLength property.</summary>
	public float normalLength
	{
		get { return _normalLength; }
		set { _normalLength = value; }
	}
#endif

#region UnityMethods:
	void Reset()
	{
#if UNITY_EDITOR
		onRangeColor = Color.green;
		offRangeColor = Color.red;
#endif
		gravity = new Vector3(0f, 1f, 0f);
		cameraGridAttributes = new CameraGrid(0.5f, 0.5f, 0.5f, 0.5f);
	}

	/// <summary>BaseCamera's' instance initialization.</summary>
	void Awake()
	{
		
	}

	/// <summary>BaseCamera's starting actions before 1st Update frame.</summary>
	void Start()
	{
		
	}

	void Update()
	{
		CalculateRelativeNormals();
	}
	
	/// <summary>BaseCamera's tick at the end of each frame.</summary>
	void LateUpdate()
	{
		UpdateNearViewportPlane();
		UpdateCamera();
	}
#endregion

	/// <summary>Updates Camera.</summary>
	protected abstract void UpdateCamera();

	/// <summary>Shakes Camera.</summary>
	/// <param name="_shakePercentage">Shakes intensity measured in a normalized percentage.</param>
	protected abstract IEnumerator ShakeCamera(float _shakePercentage);

	protected virtual void CalculateRelativeNormals()
	{
		relativeUp = (transform.position + gravity) - transform.position;
		relativeForward = Vector3.Cross(transform.right, relativeUp);
		relativeNormal = (transform.right + relativeUp + relativeForward);
	}

	/// <summary>Shakes Camera.</summary>
	/// <param name="_shakePercentage">Shakes intensity measured in a normalized percentage.</param>
	public void Shake(float _shakePercentage)
	{
		cameraEffect = new Behavior(this, ShakeCamera(_shakePercentage));
	}

	protected void UpdateNearViewportPlane()
	{
		float z = camera.nearClipPlane;
		float x = Mathf.Tan(camera.fieldOfView * 0.5f) * z;
		float y = x / camera.aspect;

		nearPlane.centerPoint = transform.position + transform.TransformDirection(new Vector3(0f, 0f, z));
		nearPlane.topLeftPoint = nearPlane.centerPoint + transform.TransformDirection(new Vector3(-x, y, 0f));
		nearPlane.topRightPoint = nearPlane.centerPoint + transform.TransformDirection(new Vector3(x, y, 0f));
		nearPlane.bottomLeftPoint = nearPlane.centerPoint + transform.TransformDirection(new Vector3(-x, -y, 0f));
		nearPlane.bottomRightPoint = nearPlane.centerPoint + transform.TransformDirection(new Vector3(x, -y, 0f));
	}

	/*protected void UpdateCameraGridPlane()
	{
		float z = camera.nearClipPlane;
	}*/

	protected bool CheckIfWithinGridFocusArea()
	{
		if(focusPoint != null)
		{
			Vector3 transformView = Camera.main.WorldToViewportPoint(focusPoint.position);
			return 		(transformView.x > cameraGridAttributes.GetMappedVerticalLeft()
					&& 	transformView.x < cameraGridAttributes.GetMappedVerticalRight()
					&& 	transformView.y > cameraGridAttributes.GetMappedHorizontalDown()
					&& 	transformView.y < cameraGridAttributes.GetMappedHorizontalUp());
		}
		else return false;
	}
#if UNITY_EDITOR
	/// <summary>Draws Gizmos, even if not selected.</summary>
	void OnDrawGizmos()
	{
		if(!drawWhenSelected) DrawGizmos();
	}

	/// <summary>Draws Gizmos only when selected.</summary>
	void OnDrawGizmosSelected()
	{
		if(drawWhenSelected) DrawGizmos();
	}

	/// <summary>Draws Gizmos.</summary>
	protected virtual void DrawGizmos()
	{
		DrawRelativeNormals();
		UpdateNearViewportPlane();
		DrawCameraGrid();
	}

	protected void DrawCameraGrid()
	{
		Gizmos.color = CheckIfWithinGridFocusArea() ? onRangeColor : offRangeColor;

		/// Vertical Left Line:
		Gizmos.DrawLine(
			Vector3.Lerp(nearPlane.topRightPoint, nearPlane.topLeftPoint, cameraGridAttributes.GetMappedVerticalLeft()),
			Vector3.Lerp(nearPlane.bottomRightPoint, nearPlane.bottomLeftPoint, cameraGridAttributes.GetMappedVerticalLeft()));
		/// Vertical Right Line:
		Gizmos.DrawLine(
			Vector3.Lerp(nearPlane.topRightPoint, nearPlane.topLeftPoint, cameraGridAttributes.GetMappedVerticalRight()),
			Vector3.Lerp(nearPlane.bottomRightPoint, nearPlane.bottomLeftPoint, cameraGridAttributes.GetMappedVerticalRight()));
		/// Horizontal Up Line:
		Gizmos.DrawLine(
			Vector3.Lerp(nearPlane.topRightPoint, nearPlane.bottomRightPoint, cameraGridAttributes.GetMappedHorizontalUp()),
			Vector3.Lerp(nearPlane.topLeftPoint, nearPlane.bottomLeftPoint, cameraGridAttributes.GetMappedHorizontalUp()));
		/// Horizontal Down Line:
		Gizmos.DrawLine(
			Vector3.Lerp(nearPlane.topRightPoint, nearPlane.bottomRightPoint, cameraGridAttributes.GetMappedHorizontalDown()),
			Vector3.Lerp(nearPlane.topLeftPoint, nearPlane.bottomLeftPoint, cameraGridAttributes.GetMappedHorizontalDown()));
	}

	protected virtual void DrawRelativeNormals()
	{
		if(!Application.isPlaying)CalculateRelativeNormals();
		VoidlessGizmos.DrawGizmoRay(transform.position, relativeUp, normalLength, Color.green);
		VoidlessGizmos.DrawGizmoRay(transform.position, relativeForward, normalLength, Color.blue);
	}
#endif

}
}