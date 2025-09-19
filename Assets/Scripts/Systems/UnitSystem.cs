using Behaviours;
using JetBrains.Annotations;
using Netologia.Behaviours;
using Netologia.TowerDefence;
using Netologia.TowerDefence.Behaviors;
using Netologia.TowerDefence.Settings;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using Zenject;
using static UnityEngine.GraphicsBuffer;

namespace Netologia.Systems
{
	public class UnitSystem : GameObjectPoolContainer<Unit>, Director.IManualUpdate
	{
		private Director _director;					//injected
		private EffectSystem _effects;				//injected
		private Constants _constants;				//injected
		private Vector3[] _path;					//injected

		[SerializeField, Min(0.01f)]
		private float _arrivalDistance = 0.1f;

		public event Action<int> OnDespawnUnitHandler;

		[CanBeNull]
		public Unit FindTarget(in Vector3 position, float range)
		{
			range *= range;
			var target = default(Unit);
			foreach (var pair in this)
			{
				foreach (var unit in pair)
				{
					var distance = Vector3.SqrMagnitude(unit.transform.position - position);
					if (distance < range)
						(range, target) = (distance, unit);
				}
			}

			return target;
		}

		public void ManualUpdate()
		{
            foreach (var pair in this)
            {
                foreach (var unit in pair)
                {
                  if(unit.CurrentHealth <= 0)
					{ 
						DespawnUnit(unit, unit.transform.position);
						_director.AddMoney(unit.Stats.Cost);
					}
					else
					{
                        MoveUnit(unit);
					}
                }
            }
            //todo Netologia homework 
        }

           private void MoveUnit(Unit unit)
        {            
            if (_path == null || _path.Length == 0) return;
           
            var targetIdx = unit.PathIndex;
			print($"path index {targetIdx}");
            if (targetIdx >= _path.Length-1)
            {
                DespawnUnit(unit, unit.transform.position);
                _director.AddPlayerDamage(unit.Stats.Cost);
                return;
            }

            Vector3 targetPos = _path[targetIdx];
            Vector3 direction = (targetPos - unit.transform.position).normalized;
			            
            float step = unit.MoveSpeed * Time.deltaTime;
            unit.transform.Translate(direction * step, Space.World);
            
            if (Vector3.Distance(unit.transform.position, targetPos) <= _arrivalDistance)
            {                
                unit.PathIndex = Math.Min(targetIdx + 1, _path.Length - 1);
            }
        }
		
				
		private void DespawnUnit(Unit unit, in Vector3 position)
		{
			//Create HitEffect
			if (unit.HasEffect)
			{
				var effect = _effects[unit.DieEffect].Get;
				effect.transform.position = position;
				effect.Play();
			}
			//Play sound
			if (unit.HasSound)
			{
				AudioManager.PlayHit(unit.DieSound);
			}

			//_director.AddMoney(unit.Stats.Cost);
			this[unit.Ref].ReturnElement(unit.ID);
		}
		
		[Inject]
		private void Construct(EffectSystem effects, Director director, Constants constants, WaveController path)
		{
			(_effects, _director, _constants, _path) = (effects, director, constants, path.GetPath());
			_arrivalDistance *= _arrivalDistance;
			AwakeMethod = t => t.Constants = _constants;
		}
	}
}