using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;

namespace VoidlessUtilities
{
public delegate void RecycleRequestCallback<T>(T _poolObject);

public class ObjectPool : Singleton<ObjectPool>, ISlotCollection
{
	[SerializeField] private bool _destroyOnLoad; 					/// <summary>Destroy this GameObject when a scene loads?.</summary>
	[SerializeField] private bool _ignoreIfPeekedElementIsActive; 	/// <summary>Ignore this Slot's retrieval request if the peeked element is active?.</summary>
	[SerializeField] private int _slotsSize; 						/// <summary>Slots Limit [if it must have some].</summary>
	private Dictionary<int, Queue<GameObject>> _objectsPool; 		/// <summary>Pool of IPoolObjects.</summary>
	private Dictionary<int, IPoolObjectPoolData> _poolObjectsData; 	/// <summary>Pool of IPoolObjectPoolData.</summary>
	private Queue<IEnumerator> _poolObjectsRemovalRoutines; 		/// <summary>Remove Requests handled for each end of frame.</summary>
	private Queue<IEnumerator> _poolObjectsAdditionRoutines; 		/// <summary>Addition Requests handled for each end of frame.</summary>
	private List<IPoolObject> _activePoolObjectsSlots; 				/// <summary>List that stores references to all active Pool Objects registered on all Queues.</summary>
	private Coroutine _queuesRequestAttendance; 					/// <summary>AttendRequestsAtEndOfFrame coroutine's reference.</summary>
	private int _occupiedSlotsCount; 								/// <summary>Slots currently occupied on the Pool.</summary>
	private int _vacantSlotsCount; 									/// <summary>Slots currently vacant on this Pool.</summary>

#if UNITY_EDITOR
	[Space(5f)]
	[Header("Debug Attributes:")]
	[SerializeField] private bool debug; 							/// <summary>Debug this Pool?.</summary>
	private float verticalScrollBarValue; 							/// <summary>Current Vertical Scroll Bar's Value.</summary>
	private Vector2 scrollPosition; 								/// <summary>Scroll's Position.</summary>
#endif

#region Getters/Setters:
	/// <summary>Gets and Sets destroyOnLoad property.</summary>
	public bool destroyOnLoad
	{
		get { return _destroyOnLoad; }
		set { _destroyOnLoad = value; }
	}

	/// <summary>Gets and Sets ignoreIfPeekedElementIsActive property.</summary>
	public bool ignoreIfPeekedElementIsActive
	{
		get { return _ignoreIfPeekedElementIsActive; }
		set { _ignoreIfPeekedElementIsActive = value; }
	}

	/// <summary>Gets and Sets slotsSize property.</summary>
	public int slotsSize
	{
		get { return _slotsSize; }
		set
		{
			_slotsSize = value;
			if(_slotsSize > activePoolObjectsSlots.Count)
			{
				for(int i = 0; i < (_slotsSize - activePoolObjectsSlots.Count); i++)
				{
					activePoolObjectsSlots.Add(default(IPoolObject));
				}
			}
		}
	}

	/// <summary>Gets and Sets poolObjectsData property.</summary>
	public Dictionary<int, IPoolObjectPoolData> poolObjectsData
	{
		get { return _poolObjectsData; }
		set { _poolObjectsData = value; }
	}

	/// <summary>Gets and Sets objectsPool property.</summary>
	public Dictionary<int, Queue<GameObject>> objectsPool
	{
		get { return _objectsPool; }
		private set { _objectsPool = value; }
	}

	/// <summary>Gets and Sets poolObjectsRemovalRoutines property.</summary>
	public Queue<IEnumerator> poolObjectsRemovalRoutines
	{
		get { return _poolObjectsRemovalRoutines; }
		private set { _poolObjectsRemovalRoutines = value; }
	}

	/// <summary>Gets and Sets poolObjectsAdditionRoutines property.</summary>
	public Queue<IEnumerator> poolObjectsAdditionRoutines
	{
		get { return _poolObjectsAdditionRoutines; }
		private set { _poolObjectsAdditionRoutines = value; }
	}

	/// <summary>Gets and Sets activePoolObjectsSlots property.</summary>
	public List<IPoolObject> activePoolObjectsSlots
	{
		get { return _activePoolObjectsSlots; }
		private set { _activePoolObjectsSlots = value; }
	}

	/// <summary>Gets and Sets queuesRequestAttendance property.</summary>
	public Coroutine queuesRequestAttendance
	{
		get { return _queuesRequestAttendance; }
		private set { _queuesRequestAttendance = value; }
	}

	/// <summary>Gets and Sets occupiedSlotsCount property.</summary>
	public int occupiedSlotsCount
	{
		get { return _occupiedSlotsCount; }
		set
		{
			_occupiedSlotsCount = value;
			vacantSlotsCount = (slotsSize - _occupiedSlotsCount);
		}
	}

	/// <summary>Gets and Sets vacantSlotsCount property.</summary>
	public int vacantSlotsCount
	{
		get { return _vacantSlotsCount; }
		set { _vacantSlotsCount = value; }
	}
#endregion

#region UnityMethods:
	private void OnGUI()
	{
#if UNITY_EDITOR
		if(debug)
		{
			StringBuilder text = new StringBuilder();
			float width = 250f;
			float height = 300f;
			float offset = 10f;

			//verticalScrollBarValue = GUI.VerticalScrollbar(new Rect(width + offset, offset, offset, height));

			for(int i = 0; i < poolObjectsData.Count; i++)
			{
				IPoolObjectPoolData poolData = poolObjectsData.ElementAt(i).Value;
				
				text.Append("Pool Object with ID ");
				text.Append(poolObjectsData.ElementAt(i).Key);
				text.Append(":");
				text.Append("\n");
				text.Append("Size: ");
				text.Append(poolData.slotsSize);
				text.Append("\n");
				text.Append("Occupied Count: ");
				text.Append(poolData.occupiedSlotsCount);
				text.Append("\n");
				text.Append("Vacant Count: ");
				text.Append(poolData.vacantSlotsCount);
				text.Append("\n");
				text.Append("\n");

				foreach(IPoolObject poolObject in poolData.poolObjectsQueue)
				{
					text.Append("Pool Object Dictionary ID: ");
					text.Append(poolObject.poolDictionaryID);
					text.Append("\n");
				}
			}

			GUI.Box(new Rect(offset, offset, width, height), text.ToString());
		}
#endif
	}

	private void OnEnable()
	{
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.activeSceneChanged += OnActiveSceneChanged;
	}

	private void OnDisable()
	{
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.activeSceneChanged -= OnActiveSceneChanged;
	}

	private void Awake()
	{
		if(Instance != this) Destroy(gameObject);
		else
		{
			if(!destroyOnLoad) DontDestroyOnLoad(gameObject);
			if(ignoreIfPeekedElementIsActive) activePoolObjectsSlots = new List<IPoolObject>(slotsSize);
			occupiedSlotsCount = 0;
			poolObjectsData = new Dictionary<int, IPoolObjectPoolData>();
			objectsPool = new Dictionary<int, Queue<GameObject>>();
			poolObjectsAdditionRoutines = new Queue<IEnumerator>();
			poolObjectsRemovalRoutines = new Queue<IEnumerator>();
			queuesRequestAttendance = StartCoroutine(AttendRequestsAtEndOfFrame());
		}
	}
#endregion

#region ObjectPoolOperations:
	/// <summary>Creates Pool of Objects [if it doesn't already exist].</summary>
	/// <param name="_poolObject">Prefab implementing IPoolObject interface.</param>
	/// <param name="_poolSize">Size of the Pool.</param>
	/// <param name="_ignoreIfPeekedElementIsActive">Ignore this Slot's retrieval request if the peeked element is active?.</param>
	public void CreateObjectsPool<T>(T _poolObject, int _poolSize = 1, bool _ignoreIfPeekedElementIsActive = false) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();

		if(!poolObjectsData.ContainsKey(instanceKey))
		{
			Transform newGroup = new GameObject("Group_" + _poolObject.gameObject.name + "_WithID_" + instanceKey.ToString()).transform;
			Queue<IPoolObject> newPoolQueue = new Queue<IPoolObject>();

			for(int i = 0; i < _poolSize; i++)
			{
				T newPoolObject = Instantiate<T>(_poolObject, Vector3.zero, Quaternion.identity);
				newPoolObject.OnObjectCreation();
				newPoolObject.poolDictionaryID = instanceKey;
				newPoolObject.gameObject.transform.parent = newGroup;
				newPoolQueue.Enqueue(newPoolObject);
			}

			IPoolObjectPoolData newPoolData  = new IPoolObjectPoolData
			(
				newPoolQueue,
				newGroup,
				_ignoreIfPeekedElementIsActive
			);
			newPoolData.slotsSize += _poolSize;
			poolObjectsData.Add(instanceKey, newPoolData);
		}
		else AddToObjectsPool(_poolObject, _poolSize);
	}

	/// <summary>Adds Object to Pool and recycles it. It is private since it is handled internally.</summary>
	/// <param name="_poolObject">Pool Pbject to recycle.</param>
	/// <param name="_position">Position to giv to Poo, Object.</param>
	/// <param name="_rotation">Rotation to give to Pool Object.</param>
	/// <returns>Pool Object recycled.</returns>
	private T AddToObjectsPoolAndRecycle<T>(T _poolObject, Vector3 _position, Quaternion _rotation) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();
		T newObject = Instantiate(_poolObject, Vector3.zero, Quaternion.identity) as T;
		IPoolObjectPoolData destintyIPoolObjectData = null;

		/// Add Pool Object.
		newObject.OnObjectCreation();
		newObject.poolDictionaryID = instanceKey;		
		
		if(poolObjectsData.ContainsKey(instanceKey))
		{ /// Add the Queue to the Pool Object's Data
			destintyIPoolObjectData = poolObjectsData[instanceKey];
		}
		else
		{ /// Add Element to existing Pool Object's Data.
			Transform newGroup = new GameObject("Group_" + _poolObject.gameObject.name + "_WithID_" + instanceKey.ToString()).transform;
			Queue<IPoolObject> newPoolQueue = new Queue<IPoolObject>();
			
			destintyIPoolObjectData = new IPoolObjectPoolData
			(
				newPoolQueue,
				newGroup,
				true
			);
			poolObjectsData.Add(instanceKey, destintyIPoolObjectData);
		}

		/// "Recycle" Pool Object.
		newObject.OnObjectReset();
		newObject.onPoolObjectDeactivation += OnPoolObjectDeactivated;
		newObject.gameObject.transform.parent = destintyIPoolObjectData.group;
		
		destintyIPoolObjectData.poolObjectsQueue.Enqueue(newObject);
		destintyIPoolObjectData.slotsSize++;
		destintyIPoolObjectData.occupiedSlotsCount++;

		return newObject;
	}

	/// <summary>Adds Pool Object to Pool, only if such exists.</summary>
	/// <param name="_poolObject">Object to add to existing Pool.</param>
	/// <param name="_copies">Number of times the Object will be added.</param>
	public void AddToObjectsPool<T>(T _poolObject, int _copies = 1, bool _ignoreIfPeekedElementIsActive = false) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();

		if(poolObjectsData.ContainsKey(instanceKey))
		{
			IPoolObjectPoolData destintyIPoolObjectData = poolObjectsData[instanceKey];

			for(int i = 0; i < _copies; i++)
			{
				T newObject = Instantiate(_poolObject, Vector3.zero, Quaternion.identity) as T;	
				newObject.OnObjectCreation();
				newObject.poolDictionaryID = instanceKey;
				newObject.gameObject.transform.parent = destintyIPoolObjectData.group;
				destintyIPoolObjectData.poolObjectsQueue.Enqueue(newObject);
			}

			destintyIPoolObjectData.slotsSize += _copies;
		}
		else CreateObjectsPool(_poolObject, _copies, _ignoreIfPeekedElementIsActive);
	}

	/// <summary>Recycles first IPoolObject fetched from queue, then it adds it to the end of the queue.</summary>
	/// <param name="_poolObject">Type of Pool Object to recycle.</param>
	/// <param name="_position">New Pool Object's position.</param>
	/// <param name="_rotation">New Pool Object's rotation.</param>
	public T RecyclePoolObject<T>(T _poolObject, Vector3 _position, Quaternion _rotation) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();
		
		if(poolObjectsData.ContainsKey(instanceKey))
		{
			IPoolObjectPoolData poolObjectData = poolObjectsData[instanceKey];
			Queue<IPoolObject> destinyQueue = poolObjectData.poolObjectsQueue;
			//T objectToRecycle = destinyQueue.Queue() as T;
			T objectToRecycle = destinyQueue.Peek() as T;

			/*if(!poolObjectData.RequestApproved())
			{
				return AddToObjectsPoolAndRecycle(_poolObject, _position, _rotation);
			}*/

			destinyQueue.Dequeue();
			objectToRecycle.transform.position = _position;
			objectToRecycle.transform.rotation = _rotation;
			destinyQueue.Enqueue(objectToRecycle);
			objectToRecycle.active = true;
			objectToRecycle.OnObjectReset();
			objectToRecycle.onPoolObjectDeactivation += OnPoolObjectDeactivated;
			poolObjectData.occupiedSlotsCount++;

			return objectToRecycle;
		}
		else
		{
			Debug.LogError("[ObjectPool] There is no GameObject of type " + _poolObject + " registered on Pool's Dictionary.");
			return null;
		}
	}

	/// <summary>Dispatches Pool Objects from Pool, if such exists.</summary>
	/// <param name="_poolObject">Type of Pool Object to dispatch.</param>
	/// <param name="_quantity">Quantity of Objects to dispatch from existing Pool.</param>
	public void DispatchPoolObjects<T>(T _poolObject, int _quantity = 1) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();
		
		if(poolObjectsData.ContainsKey(instanceKey))
		{
			IPoolObjectPoolData destintyIPoolObjectData = poolObjectsData[instanceKey];
			int clampedCopies = Mathf.Clamp(_quantity, 0, destintyIPoolObjectData.poolObjectsQueue.Count);

			for(int i = 0; i < clampedCopies; i++)
			{
				T objectToDispatch = destintyIPoolObjectData.poolObjectsQueue.Dequeue() as T;	
				objectToDispatch.OnObjectDestruction();
				destintyIPoolObjectData.occupiedSlotsCount--;
			}

			destintyIPoolObjectData.slotsSize -= clampedCopies;
		}
		else
		{
			Debug.LogError("[ObjectPool] There is not this type of GameObject registered on Pool. Be sure to first create a Pool for this GameObject");
			return;
		}
	}
#endregion

#region QueueRequestsMethods:
	/// <summary>Enqueues Pool Object Addition Request to attendance's queue.</summary>
	/// <param name="_poolObject">Pool  Object requested for addition.</param>
	/// <param name="_position">Position given to Pool Object when attended and recycled.</param>
	/// <param name="_rotation">Rotation given to Pool Object when attended and recycled.</param>
	/// <param name="recycleCallback">Callback invoked when the request gets attended, passing the recycled Pool Object.</param>
	public void OnPoolObjectAddToSlotRequest<T>(T _poolObject, Vector3 _position, Quaternion _rotation, RecycleRequestCallback<T> recycleCallback) where T : MonoBehaviour, IPoolObject
	{
		poolObjectsAdditionRoutines.Enqueue(AddAtEndOfFrame(_poolObject, _position, _rotation, recycleCallback));
	}

	/// <summary>Enqueues Pool Object Removal Request to attandance's queue.</summary>
	/// <param name="_poolObject">Pool Object requested for removal.</param>
	public void OnPoolObjectRemoveFromSlotRequest<T>(T _poolObject) where T : MonoBehaviour, IPoolObject
	{
		poolObjectsRemovalRoutines.Enqueue(RemoveAtEndOfFrame(_poolObject));
	}
#endregion

#region SlotsCounting:
	/// <summary>Gets Occupied Slots' count on the pool belonging to the given ID.</summary>
	/// <param name="_instanceID">ID of the Pool requested.</param>
	/// <returns>Occupied count if there is an existing pool, -1 if there is no pool.</returns>
	public int GetOccupiedSlotsCount(int _instanceID)
	{
		int count = -1;

		try { if(poolObjectsData.ContainsKey(_instanceID)) count = poolObjectsData[_instanceID].occupiedSlotsCount; }
		catch(NoGameObjectRegisteredOnPoolException exception) { Debug.LogError("[ObjectPool] Error: " + exception.Message + " From ID: " + _instanceID.ToString()); }
		
		return count;
	}

	/// <summary>Gets Occupied Slots' count on the pool belonging to the given Pool Object.</summary>
	/// <param name="_poolObject">Pool Object of the Pool requested.</param>
	/// <returns>Occupied count if there is an existing pool, -1 if there is no pool.</returns>
	public int GetOccupiedSlotsCount<T>(T _poolObject) where T : MonoBehaviour, IPoolObject
	{
		int instanceKey = _poolObject.GetInstanceID();
		int count = -1;

		try { if(poolObjectsData.ContainsKey(instanceKey)) count = poolObjectsData[instanceKey].occupiedSlotsCount; }
		catch(NoGameObjectRegisteredOnPoolException exception) { Debug.LogError("[ObjectPool] Error: " + exception.Message + " From GameObject: " + _poolObject.ToString()); }
		
		return count;
	}

	/// <summary>Gets Vacant Slots' count on the pool belonging to the given ID.</summary>
	/// <param name="_instanceID">ID of the Pool requested.</param>
	/// <returns>Vacant count if there is an existing pool, -1 if there is no pool.</returns>
	public int GetVacantSlotsCount(int _instanceID)
	{
		int count = -1;

		try { if(poolObjectsData.ContainsKey(_instanceID)) return poolObjectsData[_instanceID].vacantSlotsCount; }
		catch(NoGameObjectRegisteredOnPoolException exception) { Debug.LogError("[ObjectPool] Error: " + exception.Message + " From ID: " + _instanceID.ToString()); }

		return count;
	}

	/// <summary>Gets Occupied Slots' count on the pool belonging to the given Pool Object.</summary>
	/// <param name="_poolObject">Pool Object of the Pool requested.</param>
	/// <returns>Occupied count if there is an existing pool, -1 if there is no pool.</returns>
	public int GetVacantSlotsCount<T>(T _poolObject) where T : MonoBehaviour, IPoolObject
	{
		int count = -1;
		int instanceKey = _poolObject.GetInstanceID();

		try{ if(poolObjectsData.ContainsKey(instanceKey)) count = poolObjectsData[instanceKey].vacantSlotsCount; }
		catch(NoGameObjectRegisteredOnPoolException exception) { Debug.LogError("[ObjectPool] Error: " + exception.Message + " From GameObject: " + _poolObject.ToString()); }
	
		return count;
	}
#endregion

#region Callbacks:
	private void OnSceneUnloaded(Scene _currentScene)
	{
		if(Application.isPlaying)
		{
			Debug.Log("[ObjectPool - OnSceneUnloaded] Current Scene: " + _currentScene);
		}	
	}

	private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
    	if(Application.isPlaying)
    	{
    		Debug.Log("[ObjectPool - OnSceneLoaded] Loaded: " + _scene.name + " in mode: " + _mode.ToString());
    	}
    }

    private void OnActiveSceneChanged(Scene _currentScene, Scene _nextScene)
    {
    	if(Application.isPlaying)
    	{
    		string currentName = _currentScene.name;

	        if (currentName == null)
	        {
	            // Scene1 has been removed
	            currentName = "Replaced";
	        }

	        Debug.Log("[ObjectPool - OnActiveSceneChanged]: Current Scene: " + currentName + ", Next Scene: " + _nextScene.name);
    	}   
    }

	/// <summary>Action invoked when a subscribed's Pool Object's onPoolObjectDeactivation's event is invoked. Deactivates the Object and decreases the pool's count.</summary>
	/// <param name="_poolObject">Pool's Object that invoked the event.</param>
	private void OnPoolObjectDeactivated(IPoolObject _poolObject)
	{
		_poolObject.onPoolObjectDeactivation -= OnPoolObjectDeactivated;
		_poolObject.active = false;

		if(poolObjectsData.ContainsKey(_poolObject.poolDictionaryID))
		{
			poolObjectsData[_poolObject.poolDictionaryID].occupiedSlotsCount--;
		}
	}
#endregion

#region Coroutines:
	private IEnumerator AddAtEndOfFrame<T>(T _poolObject, Vector3 _position, Quaternion _rotation, RecycleRequestCallback<T> recycleCallback) where T : MonoBehaviour, IPoolObject
	{
		T retrievedPoolObject = null;
		int instanceKey = _poolObject.GetInstanceID();
		
		if(poolObjectsData.ContainsKey(instanceKey))
		{
			IPoolObjectPoolData poolData = poolObjectsData[_poolObject.GetInstanceID()];

			if(!poolData.RequestApproved())
			{
				retrievedPoolObject = AddToObjectsPoolAndRecycle(_poolObject, _position, _rotation) as T;
			}
			else
			{
				retrievedPoolObject = RecyclePoolObject(_poolObject, _position, _rotation) as T;
			}
		}
		else
		{
			retrievedPoolObject = AddToObjectsPoolAndRecycle(_poolObject, _position, _rotation) as T;
		}

		recycleCallback(retrievedPoolObject);
		
		yield return null;
	}

	/// <summary>Removes given Pool Object from active slots at the end of the frame.</summary>
	/// <param name="_poolObject">Pool Object to remove from occupied slots.</param>
	private IEnumerator RemoveAtEndOfFrame<T>(T _poolObject) where T : MonoBehaviour, IPoolObject
	{
		_poolObject.active = false;
		occupiedSlotsCount--;
		yield return null;
	}

	/// <summary>Attends Addition and Removal rquests queues at the end of the frame.</summary>
	private IEnumerator AttendRequestsAtEndOfFrame()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			
			if(poolObjectsRemovalRoutines.Count > 0)
			while(poolObjectsRemovalRoutines.Count > 0)
			{
				poolObjectsRemovalRoutines.Dequeue().MoveNext();	
			}

			if(poolObjectsAdditionRoutines.Count > 0)
			while(poolObjectsAdditionRoutines.Count > 0)
			{
				poolObjectsAdditionRoutines.Dequeue().MoveNext();
			}
		}
	}
#endregion

}
}