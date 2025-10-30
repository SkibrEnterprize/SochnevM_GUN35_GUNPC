using System;
using Netologia.Quest.Characters.Player;
using UnityEngine;

public sealed class TriggerObserver : MonoBehaviour
{
	public event Action OnEnter;
   
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out PlayerController _))
		{
			OnEnter?.Invoke();
		}
	}
}