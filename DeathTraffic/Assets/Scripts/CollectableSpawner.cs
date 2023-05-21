using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class CollectableSpawner : MonoBehaviour
{
	[SerializeField] private WaypointsGroup _waypoints;
	[SerializeField, Min(1)] private int _spawnCount = 5;
	[SerializeField, Min(1)] private int _maxSpawnSkip = 5;
	[SerializeField] private Collectable[] _collectables; 

	private IEnumerator<Waypoint> _enumerator;

	private void Start()
	{
		_enumerator = _waypoints.GetWaypointChildren().GetEnumerator();
		for (int i = 0; i < _spawnCount; i++)
			Spawn();
	}

	public void Spawn()
	{
		int skip = Random.Range(1, _maxSpawnSkip);
		for (int i = 0; i < skip; i++)
		{
			if (!_enumerator.MoveNext())
			{ 
				_enumerator.Reset();
				_enumerator.MoveNext();
			}
		}

		Collectable collectable = _collectables[Random.Range(0, _collectables.Length)];
		Waypoint wPoint = _enumerator.Current;
		Collectable newCollectable = Instantiate(collectable, wPoint.position + _waypoints.transform.position, Quaternion.identity);
		newCollectable.OnTake.AddListener(Spawn);
	}
}
