using System;
using System.Collections.Generic;
using Netologia.Quest.Characters.Player;
using UnityEngine;

public sealed class InteractSystem : MonoBehaviour
{
	public event Action<Transform> OnInteract;
	
	[SerializeField]
	private List<LaserMirror> _mirrors;

	private void Awake()
	{
		foreach (LaserMirror mirror in _mirrors)
		{
			mirror.OnEnter += MirrorOnEnterHandler;
		}
	}

	private void OnDestroy()
	{
		foreach (LaserMirror mirror in _mirrors)
		{
			mirror.OnEnter -= MirrorOnEnterHandler;
		}
	}

	private void MirrorOnEnterHandler(ITrigger obj)
	{
		obj.Interact();
		OnInteract?.Invoke(obj.Transform);
	}
}