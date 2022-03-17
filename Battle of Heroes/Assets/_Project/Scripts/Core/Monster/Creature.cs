using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.Helpers;
using DG.Tweening;
using UnityEngine;
namespace BattleOfHeroes.Showcase.Core
{
    public class Creature : ICreature
    {
        private Transform _transform;
        private CreatureData _data;
        private CreatureType _type;
        private CreatureBase _creature;

        public Creature(CreatureData data, CreatureType type, CreatureBase creature)
        {
            _data = data;
            _type = type;
            _transform = creature.transform;
            _creature = creature;

            _data.AttackPower += (_data.Level -1) * _data.PowerUpPerLevel;
            _data.Health += (_data.Level -1) * _data.PowerUpPerLevel;
        }

        public void Attack(ICreature target, Vector2 pos)
        {
            Debug.Log($"[Info] {_data.Name} attacked Hero");
            Vector2 inital = _transform.position;
            _transform.DOMove(pos, 1).OnComplete(() => { target.TakeDamage(_data.AttackPower); _transform.DOMove(inital, 1); });
        }

        public void TakeDamage(float dmg)
        {          
            Debug.Log($"[Info]{_data.Name} takes {dmg} damage. Health {_data.Health}");
            _transform.DOShakePosition(1.0f, strength: new Vector3(0, 0.1f, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true)
                      .OnComplete(() =>
                      {
                          _data.Health -= dmg;
                          bool isAlive = true;
                          if (_data.Health <= 0)
                          {
                              Debug.Log($"[Info]{_data.Name} Is Dead");
                              isAlive = false;
                              Die();
                          }
                          MessageBus.Publish<DamageTaken>(new DamageTaken(_creature, _type, isAlive));
                      });
        }
        public void Die()
        {
             _creature.gameObject.SetActive(false);
        }

        public CreatureData GetCreatureData()
        {
            return _data;
        }
    }
}