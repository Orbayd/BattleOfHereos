using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Core;
using BattleOfHeroes.Showcase.Helpers;
using UnityEngine;

namespace BattleOfHeroes.Showcase.Managers
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField]
        private Vector2[] _hereosPositon;

        [SerializeField]
        private Vector2 _monsterPosition;

        [SerializeField]
        private GameObject _heroTemplate;

        [SerializeField]
        private GameObject _monsterTemplate;

        private List<CreatureBase> _heroes = new List<CreatureBase>();

        private List<CreatureBase> _monsters = new List<CreatureBase>();

        private CreatureFactory _factory;

        private bool IsPlayersTurn = true;

        [SerializeField]
        private HeroConfig _config;

        [SerializeField]
        private UIManager _uIManager;

        void Start()
        {
            InitFactories();
            InitHeroes();
            InitMonsters();
            AddEvents();
        }

        void OnDisable()
        {
            RemoveEvents();
        }

        public void InitFactories()
        {
            _factory = new CreatureFactory();
        }

        public void InitHeroes()
        {
            var heroesdata = new List<HeroData>();
            if (PersistentStorage.SelectedHeroes.Any())
            {
                heroesdata = PersistentStorage.SelectedHeroes;
            }
            else
            {
                heroesdata = _config.Heroes.Take(3).ToList();
            }

            for (int i = 0; i < heroesdata.Count; i++)
            {
                var hero = _factory.CreateHero(_heroTemplate, heroesdata[i], _hereosPositon[i]);
                _heroes.Add(hero);

            }
        }

        public void InitMonsters()
        {
           
            var monster = _factory.CreateMonster(_monsterTemplate,_monsterPosition,1);
            _monsters.Add(monster);
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
                    StartCoroutine(MonsterAttackRoutine());
                }
                else
                {
                    _monsters.Remove(e.Creature);
                    if (!_monsters.Any())
                    {
                        MessageBus.Publish(new BattleFinishedEvent(false));
                        _uIManager.Navigate(Enums.ViewName.BattleResult);
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
                        MessageBus.Publish(new BattleFinishedEvent(true));
                        _uIManager.Navigate(Enums.ViewName.BattleResult);
                    }
                }
            }
            
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
