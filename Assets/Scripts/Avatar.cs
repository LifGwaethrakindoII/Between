using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoidlessUtilities;

public delegate void OnDestroyed(Avatar _avatar);

public class Avatar : MonoBehaviour
{
	public event OnDestroyed onDestroyed; 				/// <summary>OnDestroyed's delegate.</summary>

	[SerializeField] private LayerMask _obstacleMask; 	/// <summary>Obstacle's LayerMask.</summary>
	[SerializeField] private SineWave _sineWave; 		/// <summary>Sine Wave.</summary>
	[SerializeField] private bool _down; 				/// <summary>OC.</summary>
	[SerializeField] private int _segment; 				/// <summary>Segment.</summary>
	private SpriteRenderer _renderer; 					/// <summary>SpriteRenderer's Component.</summary>

	/// <summary>Gets and Sets obstacleMask property.</summary>
	public LayerMask obstacleMask
	{
		get { return _obstacleMask; }
		set { _obstacleMask = value; }
	}

	/// <summary>Gets and Sets sineWave property.</summary>
	public SineWave sineWave
	{
		get { return _sineWave; }
		set { _sineWave = value; }
	}

	/// <summary>Gets and Sets down property.</summary>
	public bool down
	{
		get { return _down; }
		set { _down = value; }
	}

	/// <summary>Gets and Sets segment property.</summary>
	public int segment
	{
		get { return _segment; }
		set { _segment = value; }
	}

	/// <summary>Gets and Sets renderer Component.</summary>
	public SpriteRenderer renderer
	{ 
		get
		{
			if(_renderer == null)
			{
				_renderer = GetComponent<SpriteRenderer>();
			}
			return _renderer;
		}
	}

	private void Update()
	{
		Vector3 segmentPosition = sineWave.lineRenderer.GetPosition(segment);
		float extentsY = renderer.bounds.extents.y;
		float y = down ? Mathf.Min(segmentPosition.y - extentsY, -extentsY) : Mathf.Max(segmentPosition.y + extentsY, extentsY);

		transform.position = new Vector3(segmentPosition.x, y, 0.0f);
	}

	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject obj = col.gameObject;
	
		if(obj.IsOnLayerMask(obstacleMask) && onDestroyed != null) onDestroyed(this);	
	}
}
