using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoidlessUtilities;

public class GameplayController : MonoBehaviour
{
	[Header("Player's Data:")]
	[SerializeField] private Avatar _avatar; 				/// <summary>Avatar's Prefab.</summary>
	[SerializeField] private int _avatarSegment; 			/// <summary>Avatar's Segment.</summary>
	[Space(5f)]
	[Header("Sine Wave's Data:")]
	[SerializeField] private SineWave _sineWave; 			/// <summary>Sine's Wave.</summary>
	[SerializeField] private FloatRange _amplitudeLimits; 	/// <summary>Amplitude's Limits.</summary>
	[SerializeField] private FloatRange _frequencyLimits; 	/// <summary>Frequency's Limits.</summary>
	[SerializeField] private Vector2 _speed; 				/// <summary>Speed's Vector.</summary>
	[SerializeField] private Vector2 _minMagnitude; 		/// <summary>Minimum Magnitude to displace on its respective axis.</summary>
	[Space(5f)]
	[Header("FXs' Data:")]
	[SerializeField] private ParticleSystem _explosion; 	/// <summary>Explosion's Effect Prefab.</summary>
	[SerializeField] private float _explosionDuration; 		/// <summary>Explosion's Duration.</summary>
	[Space(5f)]
	[Header("Input's Data:")]
	[SerializeField] private LayerMask _layerMask; 			/// <summary>Touchable's Layer Mask.</summary>
	[SerializeField] private float _distance; 				/// <summary>Viewport's Distance.</summary>
	[Space(5f)]
	[Header("Layer's Data:")]
	[SerializeField] private LayerMask _obstacleMask; 		/// <summary>Obstacle's LayerMask.</summary>
	[SerializeField] private LayerMask _boundaryMask; 		/// <summary>Boundaries' Mask.</summary>
	[Space(5f)]
	[Header("Spawn's Data:")]
	[SerializeField] private PoolGameObject _obstacle; 		/// <summary>Obstacle's Pool GameObject.</summary>
	[SerializeField] private TransformData _spawnWaypoint; 	/// <summary>Spawn Waypoint's Reference.</summary>
	[SerializeField] private FloatRange _heightRange; 		/// <summary>Screen Height's Range.</summary>
	[SerializeField] private FloatRange _minimumWaitRange; 	/// <summary>Minimum's Wait Range.</summary>
	private Avatar avatarOne;
	private Avatar avatarTwo;
	private GameObjectPool<PoolGameObject> obstaclesPool; 	/// <summary>Obstacles' Pool.</summary>
	private bool onTouch;
	private bool onGameOver;
	private Vector2 lastPosition;
	private RaycastHit2D hit;

	/// <summary>Gets avatar property.</summary>
	public Avatar avatar { get { return _avatar; } }

	/// <summary>Gets explosion property.</summary>
	public ParticleSystem explosion { get { return _explosion; } }

	/// <summary>Gets avatarSegment property.</summary>
	public int avatarSegment { get { return _avatarSegment; } }

	/// <summary>Gets sineWave property.</summary>
	public SineWave sineWave { get { return _sineWave; } }

	/// <summary>Gets amplitudeLimits property.</summary>
	public FloatRange amplitudeLimits { get { return _amplitudeLimits; } }

	/// <summary>Gets frequencyLimits property.</summary>
	public FloatRange frequencyLimits { get { return _frequencyLimits; } }

	/// <summary>Gets speed property.</summary>
	public Vector2 speed { get { return _speed; } }

	/// <summary>Gets minMagnitude property.</summary>
	public Vector2 minMagnitude { get { return _minMagnitude; } }

	/// <summary>Gets layerMask property.</summary>
	public LayerMask layerMask { get { return _layerMask; } }

	/// <summary>Gets obstacleMask property.</summary>
	public LayerMask obstacleMask { get { return _obstacleMask; } }

	/// <summary>Gets boundaryMask property.</summary>
	public LayerMask boundaryMask { get { return _boundaryMask; } }

	/// <summary>Gets explosionDuration property.</summary>
	public float explosionDuration { get { return _explosionDuration; } }

	/// <summary>Gets distance property.</summary>
	public float distance { get { return _distance; } }

	/// <summary>Gets obstacle property.</summary>
	public PoolGameObject obstacle { get { return _obstacle; } }

	/// <summary>Gets spawnWaypoint property.</summary>
	public TransformData spawnWaypoint { get { return _spawnWaypoint; } }

	/// <summary>Gets heightRange property.</summary>
	public FloatRange heightRange { get { return _heightRange; } }

	/// <summary>Gets minimumWaitRange property.</summary>
	public FloatRange minimumWaitRange { get { return _minimumWaitRange; } }

	private void Awake()
	{
		avatarOne = Instantiate(avatar) as Avatar;
		avatarTwo = Instantiate(avatar) as Avatar;

		avatarOne.sineWave = sineWave;
		avatarTwo.sineWave = sineWave;
		avatarOne.segment = avatarSegment;
		avatarTwo.segment = avatarSegment;
		avatarOne.down = false;
		avatarTwo.down = true;
		avatarOne.obstacleMask = obstacleMask;
		avatarTwo.obstacleMask = obstacleMask;
		avatarOne.onDestroyed += OnAvatarDestroyed;
		avatarTwo.onDestroyed += OnAvatarDestroyed;

		obstaclesPool = new GameObjectPool<PoolGameObject>(obstacle);

		onTouch = false;
		onGameOver = false;
		lastPosition = Vector2.zero;

		StartCoroutine(SpawnCycle());
	}

	private void Update()
	{
		TrackInput();
	}

	private void TrackInput()
	{
		if(Input.GetMouseButton(0))
		{
#if UNITY_EDITOR
			if(InputController.ClickHitOnViewport(out hit, distance, layerMask))
#else
			if(InputController.TouchOnViewport(out hit, distance, layerMask))
#endif
			{
				if(!onTouch) onTouch = true;
				else
				{
					Vector2 direction = (hit.point - lastPosition);
					Vector2 displacement = Vector2.Scale(direction, speed) * Time.deltaTime;
					Debug.DrawRay(lastPosition, displacement / Time.deltaTime, Color.red, 15.0f);
					if(Mathf.Abs(direction.x) >= minMagnitude.x * Time.deltaTime) sineWave.frequency = Mathf.Clamp(sineWave.frequency += displacement.x, frequencyLimits.Min(), frequencyLimits.Max());
					if(Mathf.Abs(direction.y) >= minMagnitude.y * Time.deltaTime) sineWave.amplitude = Mathf.Clamp(sineWave.amplitude += displacement.y, amplitudeLimits.Min(), amplitudeLimits.Max());
				}

				lastPosition = hit.point;
			}
		} else onTouch = false;
	}

	private void OnAvatarDestroyed(Avatar _avatar)
	{
		enabled = false;
		_avatar.onDestroyed -= OnAvatarDestroyed;
		_avatar.enabled = false;
		ParticleSystem avatarExplosion = Instantiate(explosion, _avatar.transform.position, explosion.transform.rotation);
		avatarExplosion.Stop();
		ParticleSystem.MainModule module = avatarExplosion.main;
		module.duration = explosionDuration;
		avatarExplosion.Play();

		Color avatarColor = _avatar.renderer.color;
		Color destinyColor = avatarColor;
		destinyColor.a = 0.0f;

		if(!onGameOver)
		{
			onGameOver = true;
			StartCoroutine(this.DoOnNormalizedTime(explosionDuration,
			(t)=>
			{
				_avatar.renderer.color = Color.Lerp(avatarColor, destinyColor, t);
			},
			()=>
			{
				_avatar.renderer.color = destinyColor;
				avatarExplosion.Stop();
				SceneManager.LoadScene("Scene_Gameplay");
			}));
		}
	}

	private void SpawnObstacle()
	{
		float obstacleWidth = obstacle.GetComponent<Renderer>().bounds.extents.y;
		float signMultiplier = (Random.Range(0, 2) == 1 ? 1.0f : -1.0f);
		float yPosition = signMultiplier == 1.0f ? Random.Range(obstacleWidth, heightRange.Max() - obstacleWidth) : Random.Range(heightRange.Min() + obstacleWidth, -obstacleWidth);
		Vector3 newPosition = spawnWaypoint.position.WithY(yPosition);
		
		obstaclesPool.RecycleGameObject(newPosition, spawnWaypoint.rotation);
	}

	private IEnumerator SpawnCycle()
	{
		while(true)
		{
			SecondsDelayWait wait = new SecondsDelayWait(Random.Range(minimumWaitRange.Min(), minimumWaitRange.Max()));
			yield return wait;
			SpawnObstacle();
		}
	}
}
