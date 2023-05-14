using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_N3DS
using UnityEngine.N3DS;
using N3DS = UnityEngine.N3DS;
#endif

using Unity = UnityEngine;

namespace VoidlessUtilities
{
public enum InputState 														/// <summary>Input States.</summary>
{
	None, 																	/// <summary>No input state [default].</summary>
	Begins, 																/// <summary>Input begins state.</summary>
	Stays, 																	/// <summary>Input stays state.</summary>
	Ended 																	/// <summary>Input ends state.</summary>
}

/// <summary>Event called when an input is received.</summary>
/// <param name="_inputID">ID of the input that was received.</param>
/// <param name="_state">State of the input received.</param>
public delegate void OnInputReceived(int _inputID, InputState _state);

/// <summary>Event called when the Right Axes changes.</summary>
/// <param name="_xAxis">X Axis.</param>
/// <param name="_yAxis">Y Axis.</param>
public delegate void OnRightAxesChange(float _xAxis, float _yAxis);

/// <summary>Event called when the Left Axes changes.</summary>
/// <param name="_xAxis">X Axis.</param>
/// <param name="_yAxis">Y Axis.</param>
public delegate void OnLeftAxesChange(float _xAxis, float _yAxis);

/// <summary>Event called when the D-Pad Axes changes.</summary>
/// <param name="_xAxis">X Axis.</param>
/// <param name="_yAxis">Y Axis.</param>
public delegate void OnDPadAxesChanges(float _xAxis, float _yAxis);

/// <summary>Event called when the Right Trigger Axis changes.</summary>
/// <param name="_axis">Trigger's axis.</param>
public delegate void OnRightTriggerAxisChange(float _axis);

/// <summary>Event called when the Left Trigger Axis changes.</summary>
/// <param name="_axis">Trigger's axis.</param>
public delegate void OnLeftTriggerAxisChange(float _axis);

/// <summary>Event called when the mouse's axes change.</summary>
/// <param name="_xAxis">X Axis.</param>
/// <param name="_yAxis">Y Axis.</param>
public delegate void OnMouseAxesChange(float _xAxis, float _yAxis);

/// <summary>Event called when the Touch's axis changes.</summary>
/// <param name="_touch">Touch that happened.</param>
/// <param name="_index">Index of the touch.</param>
public delegate void OnTouch(Touch _touch, int _index);

/// <summary>event invoked when a Mouse Button is Press.</summary>
/// <param name="_mouseButtonID">Mouse Button's ID.</param>
/// <param name="_state">State of the Input received.</param>
public delegate void OnMouseInput(int _mouseButtonID, InputState _state);

public class InputController : Singleton<InputController>
{
	[SerializeField] private Camera _camera; 								/// <summary>Input Controller's Camera reference for touch input.</summary>
	[SerializeField] private DetectableControllers _detectableControllers; 	/// <summary>Detectable Controllers on Play Mode.</summary>
	[SerializeField] private LayerMask _layerMask; 							/// <summary>Layer Mask.</summary>
	[Space(5f)]
	[Header("Controller Setups:")]
	[SerializeField] private XBoxControllerSetup XBoxController; 			/// <summary>XBox's controller setup.</summary>
	[SerializeField] private PCControllerSetup PCController; 				/// <summary>PC's controller setup.</summary>
#if UNITY_N3DS
	[SerializeField] private N3DSControllerSetup N3DSController; 			/// <summary>Nintendo 3DS's controller setup.</summary>
#endif

	public static event OnInputReceived onInputReceived; 					/// <summary>OnInputReceived event's delegate.</summary>
	public static event OnRightAxesChange onRightAxesChange; 				/// <summary>OnRightAxesChange event's delegate.</summary>
	public static event OnLeftAxesChange onLeftAxesChange; 					/// <summary>OnLeftAxesChange event's delegate.</summary>
	public static event OnDPadAxesChanges onDPadAxesChanges; 				/// <summary>OnDPadAxesChange event's delegate.</summary>
	public static event OnRightTriggerAxisChange onRightTriggerAxisChange; 	/// <summary>OnRightTriggerAxisChange event's delegate.</summary>
	public static event OnLeftTriggerAxisChange onLeftTriggerAxisChange; 	/// <summary>OnLeftTriggerAxisChange event's delegate.</summary>
	public static event OnMouseAxesChange onMouseAxesChange; 				/// <summary>OnMouseAxesChange event's delegate.</summary>
	public static event OnMouseInput onMouseInput; 							/// <summary>OnMouseInput event's delegate.</summary>
	public static event OnTouch onTouch; 									/// <summary>OnTouch event's delegate.</summary>

	/// <summary>Gets and Sets camera property.</summary>
	public Camera camera
	{
		get
		{
			if(_camera == null) _camera = Camera.main;
			return _camera;
		}
		set { _camera = value; }
	}

	/// <summary>Gets and Sets detectableControllers property.</summary>
	public DetectableControllers detectableControllers
	{
		get { return _detectableControllers; }
		set { _detectableControllers = value; }
	}

#region UnityMethods:
	/// <summary>InputController's' instance initialization.</summary>
	void Awake()
	{
		if(Instance != this) Destroy(gameObject);
		else DontDestroyOnLoad(gameObject);
	}
	
	/// <summary>InputController's tick at each frame.</summary>
	void Update ()
	{
		TrackInput();
	}
#endregion

	/// <summary>Tracks the Input setups depending on the current platform.</summary>
	private void TrackInput()
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		if(XBoxController != null && detectableControllers.HasFlag(DetectableControllers.XBox)) CheckXBoxControllerInputs();	
		if(PCController != null && detectableControllers.HasFlag(DetectableControllers.Pc)) CheckPCControllerInputs();
#elif (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		CheckTouchInputs();
#elif UNITY_N3DS
		if(N3DSController != null) CheckNintendo3DSControllerInputs();
#endif
	}

	public bool InputBegin(int _inputID)
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		if(XBoxController != null && detectableControllers.HasFlag(DetectableControllers.XBox)) return Input.GetKeyDown(XBoxController.keyMapping[_inputID].ToKeyCode());
		if(PCController != null && detectableControllers.HasFlag(DetectableControllers.Pc)) return Input.GetKeyDown(PCController.keyMapping[_inputID]);
//#elif (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		//CheckTouchInputs();
#elif UNITY_N3DS
		if(N3DSController != null) return GamePad.GetButtonTrigger(N3DSController.keyMapping[_inputID]);
#endif
		return false;
	}

	public bool InputStay(int _inputID)
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		if(XBoxController != null && detectableControllers.HasFlag(DetectableControllers.XBox)) return Input.GetKey(XBoxController.keyMapping[_inputID].ToKeyCode());
		if(PCController != null && detectableControllers.HasFlag(DetectableControllers.Pc)) return Input.GetKey(PCController.keyMapping[_inputID]);
//#elif (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		//CheckTouchInputs();
#elif UNITY_N3DS
		if(N3DSController != null) return GamePad.GetButtonHold(N3DSController.keyMapping[_inputID]);
#endif
		return false;
	}

	public bool InputEnds(int _inputID)
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		if(XBoxController != null && detectableControllers.HasFlag(DetectableControllers.XBox)) return Input.GetKeyUp(XBoxController.keyMapping[_inputID].ToKeyCode());
		if(PCController != null && detectableControllers.HasFlag(DetectableControllers.Pc)) return Input.GetKeyUp(PCController.keyMapping[_inputID]);
//#elif (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		//CheckTouchInputs();
#elif UNITY_N3DS
		if(N3DSController != null) return GamePad.GetButtonRelease(N3DSController.keyMapping[_inputID]);
#endif
		return false;
	}

	/// <summary>Checks XBox controller's mapped inputs.</summary>
	private void CheckXBoxControllerInputs()
	{
		if(onRightAxesChange != null) onRightAxesChange(XBoxController.rightAxisX, XBoxController.rightAxisY);
		if(onLeftAxesChange != null) onLeftAxesChange(XBoxController.leftAxisX, XBoxController.leftAxisY);
		if(onDPadAxesChanges != null) onDPadAxesChanges(XBoxController.dPadAxisX, XBoxController.dPadAxisY);
		if(onRightTriggerAxisChange != null) onRightTriggerAxisChange(XBoxController.rightTrigger);
		if(onLeftTriggerAxisChange != null) onLeftTriggerAxisChange(XBoxController.leftTrigger);

		if(XBoxController.keyMapping.Length > 0 && onInputReceived != null)
		{
			for(int i = 0; i < XBoxController.keyMapping.Length; i++)
			{
				if(Input.GetKeyDown(XBoxController.keyMapping[i].ToKeyCode()))
					onInputReceived(i, InputState.Begins);
				else if(Input.GetKey(XBoxController.keyMapping[i].ToKeyCode()))
					onInputReceived(i, InputState.Stays);
				else if(Input.GetKeyUp(XBoxController.keyMapping[i].ToKeyCode()))
					onInputReceived(i, InputState.Ended);
			}
		}
	}

	/// <summary>Checks PC controller's mapped inputs.</summary>
	private void CheckPCControllerInputs()
	{
		if(onMouseAxesChange != null) onMouseAxesChange(PCController.mouseAxisX, PCController.mouseAxisY);
		if(onRightAxesChange != null) onRightAxesChange(PCController.rightAxisX, PCController.rightAxisY);
		if(onLeftAxesChange != null) onLeftAxesChange(PCController.leftAxisX, PCController.leftAxisY);
		if(onRightTriggerAxisChange != null) onRightTriggerAxisChange(PCController.rightTrigger);
		if(onLeftTriggerAxisChange != null) onLeftTriggerAxisChange(PCController.leftTrigger);

		if(onMouseInput != null)
		{
			for(int i = 0; i < PCControllerSetup.MOUSE_INPUTS_LENGTH; i++)
			{
				if(Input.GetMouseButtonDown(i))
					onMouseInput(i, InputState.Begins);
				else if(Input.GetMouseButton(i))
					onMouseInput(i, InputState.Stays);
				else if(Input.GetMouseButtonUp(i))
					onMouseInput(i, InputState.Ended);
			}
		}	

		if(PCController.keyMapping.Length > 0 && onInputReceived != null)
		{
			for(int i = 0; i < PCController.keyMapping.Length; i++)
			{
				if(Input.GetKeyDown(PCController.keyMapping[i]))
					onInputReceived(i, InputState.Begins);
				else if(Input.GetKey(PCController.keyMapping[i]))
					onInputReceived(i, InputState.Stays);
				else if(Input.GetKeyUp(PCController.keyMapping[i]))
					onInputReceived(i, InputState.Ended);
			}
		}
	}

	/// <summary>Checks Tactil Platform's touches.</summary>
	private void CheckTouchInputs()
	{
		if(Input.touchCount > 0)
		{
			for(int i = 0; i < Input.touchCount; i++)
			{
				if(onTouch != null) onTouch(Input.touches[i], i);	
			}
		}
	}

	/// <summary>Checks if a point hits the screen's PointHitOnViewport [for 3D Mode].</summary>
	/// <param name="_hit">Hit reference to modify.</param>
	/// <param name="_distance">Ray's distance [infinity by default].</param>
	/// <param name="_debug">Debug the ray? false as default.</param>
	public static bool ClickHitOnViewport(out RaycastHit _hit, float _distance = Mathf.Infinity, int _layerMask = Physics.AllLayers, bool _debug = false)
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		Ray ray = Instance.camera.ScreenPointToRay(Input.mousePosition);
		if(_debug) Debug.DrawRay(ray.origin, ray.direction * _distance, Color.blue);
		return Physics.Raycast(ray, out _hit, _distance, _layerMask);
#else
		_hit = default(RaycastHit);
		Debug.LogWarning("[InputController] Cannot detect click on this platform.");
		return false;
#endif  
	}


	/// <summary>Checks if a point hits the screen's PointHitOnViewport [for 2D Mode].</summary>
	/// <param name="_hit">Hit reference to modify.</param>
	/// <param name="_distance">Ray's distance [infinity by default].</param>
	/// <param name="_debug">Debug the ray? false as default.</param>
	public static bool ClickHitOnViewport(out RaycastHit2D _hit, float _distance = Mathf.Infinity, int _layerMask = Physics.AllLayers, bool _debug = false)
	{
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX)
		if(Instance.camera.orthographic)
		{
			Vector3 vector = Instance.camera.ScreenToWorldPoint(Input.mousePosition.WithZ(Instance.camera.nearClipPlane));
			if(_debug) { Debug.DrawLine(vector, Vector3.zero * _distance, Color.blue); }
			_hit = Physics2D.Raycast(vector, Vector2.zero, _distance, _layerMask);
		}
		else
		{
			Ray ray = Instance.camera.ScreenPointToRay(Input.mousePosition);
			_hit = Physics2D.GetRayIntersection(ray, _distance, _layerMask);
		}

		return (_hit.collider != null);
#else
		_hit = default(RaycastHit2D);
		Debug.LogWarning("[InputController] Cannot detect click on this platform.");
		return false;
#endif  
	} 


	/// <summary>Checks if a touch hits the screen's viewport [for 3D Mode].</summary>
	/// <param name="_touchIndex">Touch's Index.</param>
	/// <param name="_hit">Hit reference to modify.</param>
	/// <param name="_distance">Ray's distance [infinity by default].</param>
	public static bool TouchOnViewport(int _touchIndex, out RaycastHit _hit, float _distance = Mathf.Infinity, int _layerMask = Physics.AllLayers)
	{
#if (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		Ray ray = Instance.camera.ScreenPointToRay(Input.GetTouch(_touchIndex).position);
		return Physics.Raycast(ray, out _hit, _distance, _layerMask);
#else
		_hit = default(RaycastHit);
		Debug.LogWarning("[InputController] Cannot detect touch on this platform.");
		return false;
#endif
	}

	/// <summary>Checks if a touch hits the screen's viewport [for 2D Mode].</summary>
	/// <param name="_touchIndex">Touch's Index.</param>
	/// <param name="_hit">Hit reference to modify.</param>
	/// <param name="_distance">Ray's distance [infinity by default].</param>
	public static bool TouchOnViewport(int _touchIndex, out RaycastHit2D _hit, float _distance = Mathf.Infinity, int _layerMask = Physics.AllLayers)
	{
#if (UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE)
		if(Instance.camera.orthographic)
		{
			Vector3 vector = Instance.camera.ScreenToWorldPoint(Input.GetTouch(_touchIndex).position);
			vector.z = Instance.camera.nearClipPlane;
			_hit = Physics2D.Raycast(vector, Vector2.zero, _distance, _layerMask);
		}
		else
		{
			Ray ray = Instance.camera.ScreenPointToRay(Input.GetTouch(_touchIndex).position);
			_hit = Physics2D.GetRayIntersection(ray, _distance, _layerMask);
		}

		return (_hit.collider != null);
#else
		_hit = default(RaycastHit2D);
		Debug.LogWarning("[InputController] Cannot detect touch on this platform.");
		return false;
#endif
	}

	/// <summary>Checks Nintendo 3DS controller's mapped inputs.</summary>
	private void CheckNintendo3DSControllerInputs()
	{
#if UNITY_N3DS
		if(onRightAxesChange != null) onRightAxesChange(N3DSController.rightAxisX, N3DSController.rightAxisY);
		if(onLeftAxesChange != null) onLeftAxesChange(N3DSController.leftAxisX, N3DSController.leftAxisY);
		if(onDPadAxesChanges != null) onDPadAxesChanges(N3DSController.dPadAxisX, N3DSController.dPadAxisY);
		if(onRightTriggerAxisChange != null) onRightTriggerAxisChange(N3DSController.rightTrigger);
		if(onLeftTriggerAxisChange != null) onLeftTriggerAxisChange(N3DSController.leftTrigger);

		if(N3DSController.keyMapping.Length > 0 && onInputReceived != null)
		{
			for(int i = 0; i < N3DSController.keyMapping.Length; i++)
			{
				if(GamePad.GetButtonTrigger(N3DSController.keyMapping[i]))
					onInputReceived(i, InputState.Begins);
				else if(GamePad.GetButtonHold(N3DSController.keyMapping[i]))
					onInputReceived(i, InputState.Stays);
				else if(GamePad.GetButtonRelease(N3DSController.keyMapping[i]))
					onInputReceived(i, InputState.Ended);
			}
		}
#endif
	}
}
}