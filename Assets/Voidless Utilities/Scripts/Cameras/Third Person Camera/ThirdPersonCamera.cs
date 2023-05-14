using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public enum LastDistanceOperation
{
	None,
	Addition,
	Subtraction
}

public enum InterpolateOrigin
{
	PathWaypointGenerator,
	FirstWaypoint
}

public class ThirdPersonCamera : BaseCamera
{
	private static readonly float LOOK_AT_EACH_OTHER_DOT_PRODUCT = 180;
	private static ThirdPersonCamera _Instance; 							/// <summary>Third person camera's instance.</summary>

	[Space(5f)]
	[Header("Yet to modify Attributes:")]
	[SerializeField] [Range(0.0f, 1.0f)] private float _followVelocity; 	/// <summary>CameraFollow's Velocity.</summary>
	[SerializeField] [Range(0.0f, 1.0f)] private float _rotationVelocity; 	/// <summary>Camera's  rotation velocity.</summary>
	[SerializeField] private float _maxFollowSpeed; 						/// <summary>Camera's Maximum follow speed.</summary>
	[SerializeField] private Transform _ThirdPersonCharacter; 				/// <summary>3rd Person Character this camera follows.</summary>
	[SerializeField] private float interpolationDuration; 					/// <summary>Interpolation duration between Camera and waypoints.</summary>
	[SerializeField] private Transform _directionReference; 				/// <summary>Direction Reference.</summary>
	[SerializeField] private NormalizedVector3 _normalizedOffset; 			/// <summary>Normalized offset.</summary>
	[SerializeField] private Vector3 _offset; 								/// <summary>Normalized offset between target point and camera.</summary>
	[SerializeField] private Vector3 _velocity; 							/// <summary>Camera's Velocity.</summary>
	[SerializeField] private float defaultInterpolationDuration; 			/// <summary>Default Interpolation's duration.</summary>
	[SerializeField] private int tolerance; 								/// <summary>Tolerance MOFO.</summary>
	[Space(5f)]
	[Header("Distance Attributes:")]
	[SerializeField] private Vector3 distanceOffset; 						/// <summary>Distance Offset.</summary>
	[SerializeField] private float minDistance; 							/// <summary>Minimum Distance.</summary>
	[SerializeField] private float _maxDistance; 							/// <summary>Maximum Distance.</summary>
	[SerializeField] private float distanceAddition; 						/// <summary>Distance's Addition each frame.</summary>
	[SerializeField] private float distanceSubtraction; 					/// <summary>Distance's Subtraction each frame.</summary>
	[SerializeField] private bool _relativeToTarget; 						/// <summary>Follow the Third Person Character relatie to its normal?.</summary>
	[Space(5f)]
	[Header("The Newest Stuff ma' boys:")]
	[SerializeField] [Range(0f, 90f)] private float playerTurnOnCameraTolerance; 	/// <summary>Tolerance of degrees if the Third Person Character is heading towards the camera.</summary>
	private bool follow; 													/// <summary>Should this camera follow the Third PersonCharacter?.</summary>
	private LastDistanceOperation lastDistanceOperation;
	private float angularVelocity;
	private float defaultDistance;
	private Vector3 _defaultOffset; 										/// <summary>Default offset between Camera and 3rd-Person Character.</summary>
	private Quaternion _defaultRotation;
	private Behavior _interpolate;
	private Behavior _defaultInterpolate;
	//private CameraWaypoint _lastCameraWaypoint; 							/// <summary>Last Camera Waypoint this Camera has interacted with.</summary>
	//private Vector3 _offset; 												/// <summary>Offset between Camera and 3rd person character.</summary>
	private Behavior _cameraInterpolation; 									/// <summary>CameraInterpolation coroutine controller.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets Instance property.</summary>
	public static ThirdPersonCamera Instance
	{
		get
		{
			if (_Instance == null) {
				_Instance = (ThirdPersonCamera)FindObjectOfType(typeof(ThirdPersonCamera));

				if (_Instance == null) {
					Debug.LogError("An Instance of " + typeof(ThirdPersonCamera) + " is needed in the scene, but there is none.");
				}
			}
			return _Instance;
		}
		private set { _Instance = value; }
	}

	/// <summary>Gets and Sets followVelocity property.</summary>
	public float followVelocity
	{
		get { return _followVelocity; }
		set { _followVelocity = value; }
	}

	/// <summary>Gets and Sets rotationVelocity property.</summary>
	public float rotationVelocity
	{
		get { return _rotationVelocity; }
		set { _rotationVelocity = value; }
	}

	/// <summary>Gets and Sets maxFollowSpeed property.</summary>
	public float maxFollowSpeed
	{
		get { return _maxFollowSpeed; }
		set { _maxFollowSpeed = value; }
	}

	/// <summary>Gets and Sets maxDistance property.</summary>
	public float maxDistance
	{
		get { return _maxDistance; }
		set { _maxDistance = value; }
	}

	/// <summary>Gets and Sets relativeToTarget property.</summary>
	public bool relativeToTarget
	{
		get { return _relativeToTarget; }
		set { _relativeToTarget = value; }
	}

	/// <summary>Gets and Sets ThirdPersonCharacter property.</summary>
	public Transform ThirdPersonCharacter
	{
		get { return _ThirdPersonCharacter; }
		set { _ThirdPersonCharacter = value; }
	}

	/// <summary>Gets and Sets directionReference property.</summary>
    public Transform directionReference
    {
        get { return _directionReference; }
        set { _directionReference = value; }
    }

    /// <summary>Gets and Sets velocity property.</summary>
    public Vector3 velocity
    {
    	get { return _velocity; }
    	set { _velocity = value; }
    }

	/*/// <summary>Gets and Sets lastCameraWaypoint property.</summary>
	public CameraWaypoint lastCameraWaypoint
	{
		get { return _lastCameraWaypoint; }
		set { _lastCameraWaypoint = value; }
	}*/

	/// <summary>Gets and Sets interpolate property.</summary>
	public Behavior interpolate
	{
		get { return _interpolate; }
		set { _interpolate = value; }
	}

	/// <summary>Gets and Sets defaultInterpolate property.</summary>
	public Behavior defaultInterpolate
	{
		get { return _defaultInterpolate; }
		set { _defaultInterpolate = value; }
	}

	/// <summary>Gets and Sets defaultOffset property.</summary>
	public Vector3 defaultOffset
	{
		get { return _defaultOffset; }
		set { _defaultOffset = value; }
	}

	/// <summary>Gets and Sets normalizedOffset property.</summary>
	public NormalizedVector3 normalizedOffset
	{
		get { return _normalizedOffset; }
		set { _normalizedOffset = value; }
	}

	/// <summary>Gets and Sets offset property.</summary>
	public Vector3 offset
	{
		get { return _offset; }
		set { _offset = value; }
	}

	/// <summary>Gets and Sets defaultRotation property.</summary>
	public Quaternion defaultRotation
	{
		get { return _defaultRotation; }
		set { _defaultRotation = value; }
	}

	/// <summary>Gets and Sets cameraInterpolation property.</summary>
	public Behavior cameraInterpolation
	{
		get { return _cameraInterpolation; }
		set { _cameraInterpolation = value; }
	}
#endregion

#region UnityMethods:
	void OnEnable()
	{
		/*CameraAgent.onWaypointCameraTrigger += OnWaypointCameraTrigger;
		CameraAgent.onGoToDefault += OnGoToDefault;*/

		//Shake(UnityEngine.Random.Range(0f, 1f));
	}

	void OnDisable()
	{
		/*CameraAgent.onWaypointCameraTrigger -= OnWaypointCameraTrigger;
		CameraAgent.onGoToDefault -= OnGoToDefault;*/
	}

	/// <summary>ThirdPersonCamera's' instance initialization.</summary>
	void Awake()
	{
		/*if(Instance != this) Destroy(gameObject);
		else Instance = this;*/

		//CalculateOffset();
		//defaultOffset = offset;
		defaultOffset = normalizedOffset;
		defaultRotation = transform.rotation;
		defaultDistance = distance;
		distance = maxDistance;

		//Debug.Log("[ThirdPersonCamera] Offset is: " + offset);
	}

	/*void Update()
	{
		CalculateRelativeNormals();
		if(ThirdPersonCharacter != null) follow = !FacingEachOther();
	}*/
	
	/*/// <summary>ThirdPersonCamera's tick at each frame.</summary>
	void Update ()
	{
		directionReference.localRotation = Quaternion.identity;
        directionReference.eulerAngles = directionReference.eulerAngles.AddToX(-transform.eulerAngles.x);
		//if(ThirdPersonCharacter != null && cameraInterpolation == null) PositionCameraOnOffset();
		//if(interpolate != null) transform.position = PositionCameraOnOffset();
	}*/
#endregion

#region EventCallbacks:
	private void OnWaypointCameraTrigger(Transform _focusPoint, PathWaypointGenerator _pathWaypoints)
	{
		//interpolate = new Behavior(this, Interpolate(_focusPoint, _pathWaypoints));
	}

	private void OnGoToDefault()
	{
		normalizedOffset = defaultOffset;
		distance = defaultDistance;
	}
#endregion

	/// <summary>Updates Camera.</summary>
	protected override void UpdateCamera()
	{
		if(ThirdPersonCharacter != null)
		{
			bool inside = CheckIfWithinGridFocusArea();
			Vector3 followVector = new Vector3
			(
				ignoreAxes == Axes2D.X ? transform.position.x : PositionCameraOnOffset().x,
				ignoreAxes == Axes2D.Y ? transform.position.y : PositionCameraOnOffset().y,
				PositionCameraOnOffset().z
			);

			Vector3 destiny = Vector3.SmoothDamp(transform.position, inside ? followVector : PositionCameraOnOffset(), ref _velocity, followVelocity, maxFollowSpeed);

			CheckForFocusPoint();
			transform.position = destiny;
			RotateSmoothlyTowardsTarget();			
		}

		directionReference.localRotation = Quaternion.identity;
        directionReference.eulerAngles = directionReference.eulerAngles.WithAddedX(-transform.eulerAngles.x);	
	}

	/// <summary>Rotates smoothly towards target point.</summary>
	private void RotateSmoothlyTowardsTarget()
	{
		Quaternion rotation = Quaternion.LookRotation(ThirdPersonCharacter.position - transform.position);
		float delta = Quaternion.Angle(transform.rotation, ThirdPersonCharacter.rotation);

		if(delta > 0.0f)
		{
			float t = Mathf.SmoothDampAngle(delta, 0.0f, ref angularVelocity, rotationVelocity);
			t = (1.0f - (t / delta));
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, t);
		}
	}

	/// <summary>Positions camera given its data. Used for Editor mode.</summary>
	public void RepositionCamera()
	{
		bool inside = CheckIfWithinGridFocusArea();
		Vector3 followVector = new Vector3
		(
			ignoreAxes == Axes2D.X ? transform.position.x : PositionCameraOnOffset().x,
			ignoreAxes == Axes2D.Y ? transform.position.y : PositionCameraOnOffset().y,
			PositionCameraOnOffset().z
		);

		LookAt(ThirdPersonCharacter.position);

		transform.position = inside ? followVector : PositionCameraOnOffset();
	}

	/// <summary>Rotates camera towards target, considering the Axes' Constrains.</summary>
	/// <param name="_point">Point to rotate at.</param>
	public void LookAt(Vector3 _point)
	{
		_point = new Vector3
		(
			ignoreAxes == Axes2D.X ? 0f : _point.x,
			ignoreAxes == Axes2D.Y ? 0f : _point.y,
			_point.z
		);

		transform.LookAt(_point);
	}

	/// <summary>Calculates offset between Camera's actual position and 3rd-Person Character's position.</summary>
	public void CalculateOffset()
	{
		offset = transform.position - ThirdPersonCharacter.position;
	}

	/// <summary>Positions Camera on offset between itself and 3rs-Person Character.</summary>
	public Vector3 PositionCameraOnOffset()
	{
		return ThirdPersonCharacter.position + (relativeToTarget ? ThirdPersonCharacter.TransformDirection(normalizedOffset * distance) : (normalizedOffset * distance));
	}

	public bool FacingEachOther()
	{
		return (Vector3.Angle(transform.forward, ThirdPersonCharacter.forward) > LOOK_AT_EACH_OTHER_DOT_PRODUCT - playerTurnOnCameraTolerance);
	}

	/// <summary>Evaluates meddling objects between camera and player to see if a distance adjustment is needed.</summary>
	public void CheckForFocusPoint()
	{
		Ray ray = default(Ray);

		switch(lastDistanceOperation)
		{
			case LastDistanceOperation.None:
			case LastDistanceOperation.Subtraction:
			ray = new Ray(transform.position, (ThirdPersonCharacter.position - transform.position));
			break;

			case LastDistanceOperation.Addition:
			ray = new Ray(transform.TransformPoint(distanceOffset), (ThirdPersonCharacter.position - transform.position));
			break;
		}
		
		RaycastHit[] hits = Physics.RaycastAll(ray, distance);

		distance = Mathf.Clamp(hits.Length > tolerance ? distance - (distanceSubtraction * Time.deltaTime) : distance + (distanceAddition * Time.deltaTime), minDistance, maxDistance);
		lastDistanceOperation = hits.Length > tolerance ? LastDistanceOperation.Subtraction : LastDistanceOperation.Addition;
	}

	/// <summary>Invokes Camera interpolation through given PathWaipoint.</summary>
	/// <param name="_pathWaypoints">Path Waypoints to interpolate with.</param>
	/// <param name="onInterpolationEnds">Callback invoked when the interpolation ends.</param>
	public void InvokePathInterpolation(PathWaypointGenerator _pathWaypoints, Action onInterpolationEnds = null)
	{
		interpolate = new Behavior(this, InterpolateCamera(_pathWaypoints, onInterpolationEnds));
	}

#region Coroutines:
	/// <summary>Shakes Camera.</summary>
	/// <param name="_shakePercentage">Shakes intensity measured in a normalized percentage.</param>
	protected override IEnumerator ShakeCamera(float _shakePercentage)
	{
		Vector3 originalPosition = transform.position;
		float xBias = 5f;
		float yBias = 3f;
		float n = 0.0f;
		float noise = 0.0f;

		while(n < 1.0f + Mathf.Epsilon)
		{
			noise = Mathf.PerlinNoise(n, n);
			Vector3 newPosition = (new Vector3
			(
				(noise - 0.5f),
				(noise - 0.5f),
				0f
			) * shakeStrength) * _shakePercentage;
			transform.position = originalPosition + newPosition;
			n += (Time.deltaTime / shakeDuration);

			yield return null;
		}

		transform.position = originalPosition;
	}

	/// <summary>Centers camera towards the Player. Activated when the Player leaves the grid area.</summary>
	private IEnumerator WhileOutOfGridArea()
	{
		while(!CheckIfWithinGridFocusArea())
		{
			transform.position = Vector3.SmoothDamp(transform.position, PositionCameraOnOffset(), ref _velocity/*, followVelocity*/, maxFollowSpeed);
			
			yield return null;
		}

		//interpolate.EndBehavior();
		interpolate = null;
	}

	/// <param name="onInterpolationEnds">Callback invoked when the interpolation ends.</param>
	public IEnumerator InterpolateCamera(PathWaypointGenerator _pathWaypoints, Action onInterpolationEnds = null)
	{
		IEnumerator currentWaypointInterpolation = null;
		
		if(defaultInterpolate != null) defaultInterpolate.EndBehavior();
		if(transform.position != _pathWaypoints.first.position) currentWaypointInterpolation = InterpolateTowardsWaypoint(_pathWaypoints.first);
		
		yield return currentWaypointInterpolation;

		for(int i = 0; i < _pathWaypoints.waypoints.Count - 1; i++)
		{
			currentWaypointInterpolation = InterpolateBetweenWaypoints(_pathWaypoints.waypoints[i], _pathWaypoints.waypoints[i + 1]);
			yield return currentWaypointInterpolation;
		}

		if(onInterpolationEnds != null) onInterpolationEnds();

		interpolate.EndBehavior();
		interpolate = null;
	}

	/// <summary>Interpolates camera towards given waypoint.</summary>
	/// <param name="_waypoint">Waypoint to interpolate towards to.</param>
	/// <param name="_interpolateProperties">Which Transform's properties to intterpolate.</param>
	private IEnumerator InterpolateTowardsWaypoint(PathWaypoint _waypoint, TransformProperties _interpolateProperties = TransformProperties.PositionAndRotation)
	{
		Vector3 originalPosition = transform.position;
		Quaternion originalRotation = transform.rotation;
		float nPosition = 0.0f;
		float nRotation = 0.0f;

		while((nPosition < (1.0f + Mathf.Epsilon)) && (nRotation < (1.0f + Mathf.Epsilon)))
		{
			if(_interpolateProperties.HasFlag(TransformProperties.Position) && (nPosition < (1.0f + Mathf.Epsilon)))
			switch(_waypoint.curveType)
			{
				case CurveType.Linear:
				transform.position = Vector3.Lerp(originalPosition, _waypoint.position, nPosition);
				break;

				case CurveType.Cuadratic:
				transform.position = VoidlessMath.CuadraticBeizer(originalPosition, _waypoint.position, _waypoint.startTangent, nPosition);
				break;

				case CurveType.Cubic:
				transform.position = VoidlessMath.CubicBeizer(originalPosition, _waypoint.position, _waypoint.startTangent, _waypoint.endTangent, nPosition);
				break;
			}

			if(_interpolateProperties.HasFlag(TransformProperties.Rotation) && (nRotation < (1.0f + Mathf.Epsilon)))
			transform.rotation = Quaternion.Lerp(originalRotation, _waypoint.rotation, nRotation);

			nPosition += (Time.deltaTime / _waypoint.movementSpeed);
			nRotation += (Time.deltaTime / _waypoint.rotationSpeed);

			yield return null;
		}
	}

	/// <summary>Interpolates camera between given waypoints.</summary>
	/// <param name="_initialWaypoint">Waypoint that will state the initial point of the interpolation.</param>
	/// <param name="_finalWaypoint">Waypoint to interpolate between to.</param>
	/// <param name="_interpolateProperties">Which Transform's properties to intterpolate.</param>
	private IEnumerator InterpolateBetweenWaypoints(PathWaypoint _initialWaypoint, PathWaypoint _finalWaypoint, TransformProperties _interpolateProperties = TransformProperties.PositionAndRotation)
	{
		float nPosition = 0.0f;
		float nRotation = 0.0f;

		while((nPosition < (1.0f + Mathf.Epsilon)) && (nRotation < (1.0f + Mathf.Epsilon)))
		{
			if(_interpolateProperties.HasFlag(TransformProperties.Position) && (nPosition < (1.0f + Mathf.Epsilon)))
			switch(_finalWaypoint.curveType)
			{
				case CurveType.Linear:
				transform.position = Vector3.Lerp(_initialWaypoint.position, _finalWaypoint.position, nPosition);
				break;

				case CurveType.Cuadratic:
				transform.position = VoidlessMath.CuadraticBeizer(_initialWaypoint.position, _finalWaypoint.position, _finalWaypoint.startTangent, nPosition);
				break;

				case CurveType.Cubic:
				transform.position = VoidlessMath.CubicBeizer(_initialWaypoint.position, _finalWaypoint.position, _initialWaypoint.startTangent, _finalWaypoint.endTangent, nPosition);
				break;
			}

			if(_interpolateProperties.HasFlag(TransformProperties.Rotation) && (nRotation < (1.0f + Mathf.Epsilon)))
			transform.rotation = Quaternion.Lerp(_initialWaypoint.rotation, _finalWaypoint.rotation, nRotation);

			nPosition += (Time.deltaTime / _finalWaypoint.movementSpeed);
			nRotation += (Time.deltaTime / _finalWaypoint.rotationSpeed);

			yield return null;
		}
	}
#endregion

#region DEPRECATED:
	/*/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerEnter(Collider col)
	{
		GameObject obj = col.gameObject;
	
		switch(obj.tag)
		{
			case "Camera_Waypoint":
			{
				if(lastCameraWaypoint != obj.GetComponent<CameraWaypoint>())
				{
					if(cameraInterpolation != null) cameraInterpolation.EndBehavior();
					cameraInterpolation = new Behavior(this, CameraInterpolation(obj.GetComponent<CameraWaypoint>()));
				}
				lastCameraWaypoint = obj.GetComponent<CameraWaypoint>();
			}	
			break;
	
			default:
			break;
		}
	}*/

	/*private IEnumerator CameraInterpolation(CameraWaypoint _cameraWaypoint)
	{
		float normalizedTime = 0.0f;
		float dividedDuration = (interpolationDuration / (_cameraWaypoint.waypoints.Length * 1f));
		Vector3 originalPosition = transform.position;
		Quaternion originalRotation = transform.rotation;

		if(_cameraWaypoint.waypoints.Length > 1)
		for(int i = 0; i < _cameraWaypoint.waypoints.Length; i++)
		{
			

			while(normalizedTime < 1.0f)
			{
				transform.position = Vector3.Lerp(originalPosition, _cameraWaypoint.waypoints[i].position, normalizedTime);
				transform.rotation = Quaternion.Lerp(originalRotation, _cameraWaypoint.waypoints[i].rotation, normalizedTime);
				CalculateOffset();
				PositionCameraOnOffset();

				normalizedTime += (Time.deltaTime / dividedDuration);
				yield return null;
			}

			normalizedTime = 0.0f;
			originalPosition = transform.position;
			originalRotation = transform.rotation;
		}
		else
		{
			offset = defaultOffset;

			while(normalizedTime < 1.0f)
			{
				transform.position = Vector3.Lerp(originalPosition, ThirdPersonCharacter.position + offset, normalizedTime);
				transform.rotation = Quaternion.Lerp(originalRotation, defaultRotation, normalizedTime);
				CalculateOffset();
				normalizedTime += (Time.deltaTime / interpolationDuration);
				yield return null;
			}
		}

		cameraInterpolation.EndBehavior();
		cameraInterpolation = null;
	}*/
#endregion
}
}