using Netologia.Behaviours;
using Netologia.TowerDefence;
using Netologia.TowerDefence.Behaviors;
using System.Diagnostics;
using UnityEngine;
using Zenject;

namespace Netologia.Systems
{
    public class TowerSystem : GameObjectPoolContainer<Tower>, Director.IManualUpdate
    {
        private UnitSystem _units;              //injected
        private ProjectileSystem _projectiles;  //injected
        private Unit _target;

        [Inject]
        private void Construct(UnitSystem units, ProjectileSystem projectiles)
        {
            _units = units;
            _projectiles = projectiles;
        }

        public void ManualUpdate()
        {
            foreach (var pair in this)
                foreach (var tower in pair)
                {
                    var poolProjectile = _projectiles[tower.Projectile];

                    if (!tower.DecrementAttackReload(tower.AttackDelay))
                    {
                        UnityEngine.Debug.Log("In Reload");
                        continue;
                    }
                    else if (!tower.HasTarget)
                    {
                        _target = _units.FindTarget(tower.transform.position, tower.Range);
                        if (_target != null)
                        {
                            tower.Target = _target;
                            print("Target is find!!!");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    Projectile projectile = poolProjectile.Get;
                    projectile.PrepareData(tower.transform.position, tower.Target, tower.Damage, tower.AttackElemental);
                    tower.Attack();
                    print("Attack!!!");

                }
        }


        //todo Netologia homework 


        public void OnDespawnUnit(int unitID)
        {
            foreach (var pair in this)
                foreach (var tower in pair)
                    if (tower.TargetID == unitID)
                        tower.Target = null;
        }
    }
}