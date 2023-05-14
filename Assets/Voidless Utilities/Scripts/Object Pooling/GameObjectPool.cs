using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
public class GameObjectPool<T> : IEnumerable<T> where T : MonoBehaviour, IPoolObject
{
	private const string TAG_GROUP = "Group_"; 	/// <summary>Group's prefix.</summary>

	private Queue<T> _poolQueue; 				/// <summary>Under the hood's dictionary pool Queue.</summary>
	private T _referenceObject; 				/// <summary>Pool's Reference Prefab.</summary>
	private Transform _poolGroup; 				/// <summary>Pool's Group.</summary>
	private int _limit; 						/// <summary>Pool's Limit.</summary>
	private int _occupiedSlotsCount; 			/// <summary>Occupied Slots' Count.</summary>
	private int _vacantSlotsCount; 				/// <summary>Vacant Slots' Count.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets poolQueue property.</summary>
	public Queue<T> poolQueue
	{
		get { return _poolQueue; }
		private set { _poolQueue = value; }
	}

	/// <summary>Gets and Sets referenceObject property.</summary>
	public T referenceObject
	{
		get { return _referenceObject; }
		set { _referenceObject = value; }
	}

	/// <summary>Gets and Sets poolGroup property.</summary>
	public Transform poolGroup
	{
		get { return _poolGroup; }
		private set { _poolGroup = value; }
	}

	/// <summary>Gets and Sets limit property.</summary>
	public int limit
	{
		get { return _limit; }
		set { _limit = value == int.MinValue ? int.MaxValue : value; }
	}

	/// <summary>Gets and Sets occupiedSlotsCount property.</summary>
	public int occupiedSlotsCount
	{
		get { return _occupiedSlotsCount; }
		set
		{
			_occupiedSlotsCount = value;
			_vacantSlotsCount = Count - occupiedSlotsCount;
		}
	}

	/// <summary>Gets and Sets vacantSlotsCount property.</summary>
	public int vacantSlotsCount
	{
		get { return _vacantSlotsCount; }
		set { _vacantSlotsCount = value; }
	}

	/// <summary>Gets Count property.</summary>
	public int Count { get { return poolQueue.Count; } }

	/// <summary>Gets IsReadOnly property.</summary>
	public bool IsReadOnly { get { return false; } }
#endregion

	/// <summary>GameObjectPool's Constructor.</summary>
	/// <param name="_referenceObject">Pool's Reference Prefab.</param>
	/// <param name="_size">Pool's starting size.</param>
	/// <param name="_limit">Pool's Limit.</param>
	public GameObjectPool(T _referenceObject, int _size = 0, int _limit = int.MaxValue)
	{
		poolQueue = new Queue<T>();
		referenceObject = _referenceObject;
		limit = _limit;

		for(int i = 0; i < _size; i++)
		{
			AddGameObject();	
		}
	}

	/// <summary>Adds Pool Object.</summary>
	/// <param name="_position">Pool Object's default position.</param>
	/// <param name="_rotation">Pool Object's default rotation.</param>
	/// <returns>Added Pool Object.</returns>
	public T AddGameObject(Vector3 _position = default(Vector3), Quaternion _rotation = default(Quaternion))
	{
		if(_rotation == default(Quaternion)) _rotation = Quaternion.identity;

		T newObject = Object.Instantiate(referenceObject, _position, _rotation) as T;

		if(Count == 0) poolGroup = new GameObject(TAG_GROUP + referenceObject.name).transform;

		newObject.transform.SetParent(poolGroup);
		poolQueue.Enqueue(newObject);
		newObject.OnObjectCreation();

		return newObject;
	}

	/// <summary>Adds created Object into Pool.</summary>
	/// <param name="_object">Object to add to Pool.</param>
	public void AddGameObject(T _object)
	{
		poolQueue.Enqueue(_object);
		_object.OnObjectCreation();
	}

	/// <summary>Recycles Pool Object from queue [dequeues], then it enqueues is again.</summary>
	/// <param name="_position">Pool Object's position.</param>
	/// <param name="_rotation">Pool Object's rotation.</param>
	/// <returns>Recycled Pool Object.</returns>
	public T RecycleGameObject(Vector3 _position = default(Vector3), Quaternion _rotation = default(Quaternion))
	{
		if(_rotation == default(Quaternion)) _rotation = Quaternion.identity;
		
		T recycledObject = poolQueue.Count > 0 ? poolQueue.Peek() : null;

		if(recycledObject != null && !recycledObject.active)
		{
			poolQueue.Dequeue();
			poolQueue.Enqueue(recycledObject);
			recycledObject.transform.position = _position;
			recycledObject.transform.rotation = _rotation;
		}
		else
		{
			recycledObject = AddGameObject(_position, _rotation);
		}

		recycledObject.OnObjectReset();
		return recycledObject;
	}

	/// <summary>Dispatched Pool Object and returns it.</summary>
	/// <returns>Dispatched Pool Object.</returns>
	public T DispatchGameObject()
	{
		T dispatchedObject = poolQueue.Dequeue();

		if(dispatchedObject != null)
		dispatchedObject.OnObjectDestruction();

		return dispatchedObject;
	}

	/// <returns>Returns an enumerator T that iterates through the collection.</returns>
	public IEnumerator<T> GetEnumerator()
	{
		return poolQueue.GetEnumerator();
	}

	/// <summary>Returns an enumerator that iterates through the collection.</summary>
	IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <returns>String Representing this Object's Pool.</returns>
    public override string ToString()
    {
    	StringBuilder builder = new StringBuilder();

    	return builder.ToString();
    }
}
}