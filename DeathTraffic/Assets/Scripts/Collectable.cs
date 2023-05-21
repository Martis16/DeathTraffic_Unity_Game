using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public UnityEvent OnTake;

    [field: SerializeField] public int Score { get; private set; } = 1;

	private void Start()
	{
		OnTake ??= new UnityEvent();
	}

	private void OnDestroy()
	{
		OnTake?.Invoke();
	}
}
