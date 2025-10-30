using System;
using UnityEngine;

namespace Netologia.Quest.Characters.Player
{
	public interface ITrigger
	{
		public event Action<ITrigger> OnEnter;
		public Transform Transform { get; }
		public void Interact();
	}
}