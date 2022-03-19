using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Core;
using BattleOfHeroes.Showcase.Helpers;
using UnityEngine;

namespace BattleOfHeroes.Showcase.Managers
{
    public class TurnManager : IService
    {
        private List<CreatureBase> _heroes = new List<CreatureBase>();
        private List<CreatureBase> _monsters = new List<CreatureBase>();
        private bool IsPlayersTurn = true;
        private UIManager _uIManager;
        private RepositoryService _repostioryService;

        private WorldFactory _worldFactory;

        public TurnManager(WorldFactory factory, UIManager uIManager)
        {
            _worldFactory = factory;
            _uIManager = uIManager;
        }

        public void Init()
        {
            CreateWorld();
            AddEvents();
        }
        public void Terminate()
        {
            RemoveEvents();
            DestroyWorld();
        }
        private void DestroyWorld()
        {
            foreach (var hero in _heroes.Where(x=> x != null))
            {
                MonoBehaviour.Destroy(hero.gameObject);
            }
            _heroes.Clear();

            foreach (var monster in _monsters.Where(x=> x != null))
            {
                MonoBehaviour.Destroy(monster.gameObject);
            }
            _monsters.Clear();
        }
        private void CreateWorld()
        {
            _heroes = _worldFactory.CreateHeroes();
            _monsters =_worldFactory.CreateMonsters();
            
            IsPlayersTurn = true;
        }

        IEnumerator MonsterAttackRoutine()
        {
            yield return new WaitForSeconds(1f);
            MonsterAttack();
        }

        private void MonsterAttack()
        {
            IsPlayersTurn = true;
            var target = _heroes.ElementAt(UnityEngine.Random.Range(0,_heroes.Count));
            _monsters.First().Attack(target,target.transform.position);

        }
        private void OnHeroAttack(HeroAttackEvent e)
        {
            if (e.Type == Enums.CreatureType.Hero && IsPlayersTurn)
            {
                var monster = _monsters.First();
                e.Creature.Attack(monster, monster.transform.position);
                IsPlayersTurn = false;
            }
        }
        private void OnCreatureDamageTaken(DamageTaken e)
        {
            if (e.Type == Enums.CreatureType.Monster)
            {
                if (e.IsAlive)
                {
                    CoroutineHelper.Singleton.StartRoutine(MonsterAttackRoutine());
                }
                else
                {
                    _monsters.Remove(e.Creature);
                    if (!_monsters.Any())
                    {
                        BattleFinished(false);
                    }
                }
            }
            else
            {
                if (!e.IsAlive)
                {
                    _heroes.Remove(e.Creature);
                    if (!_heroes.Any())
                    {
                        BattleFinished(true);
                    }
                }
            }
            
        }

        private void BattleFinished(bool isLost)
        {
            MessageBus.Publish(new BattleFinishedEvent(isLost,_heroes.ToArray()));
            _uIManager.Navigate(Enums.ViewName.BattleResult);
        }

        private void AddEvents()
        {
            MessageBus.Subscribe<HeroAttackEvent>(e => OnHeroAttack(e));
            MessageBus.Subscribe<DamageTaken>(e=> OnCreatureDamageTaken(e));
        }

        private void RemoveEvents()
        {
            MessageBus.UnSubscribe<HeroAttackEvent>();
            MessageBus.UnSubscribe<DamageTaken>();
        }

    }
}
