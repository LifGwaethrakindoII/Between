using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities
{
	public struct NodeRecord
	{
		public BaseNode<bool, float> current;
		public BaseNode<bool, float> connection;
		public float totalCost;
	}

public class DijkstraPathfinding : MonoBehaviour
{
	public GameObject[] dijkstraWaypoints;

	/// <summary>DijkstraPathfinding default constructor.</summary>
	public DijkstraPathfinding()
	{
		
	}

	public HashSet<BaseNode<bool, float>> CalculateRoute(BaseNode<bool, float> start, BaseNode<bool, float> end)
	{
		GameObject[] open = new GameObject[dijkstraWaypoints.Length];
		Array.Copy(dijkstraWaypoints, open, dijkstraWaypoints.Length);
		NodeRecord record;
		record.current = start;
		record.connection = null;
		record.totalCost = 0f;

		while(open.Length > 0)
		{

		}

		return null;
	}
}
}