using BattleOfHeroes.Showcase.Enums;
using UnityEngine;
namespace BattleOfHeroes.Showcase.Core
{
    public abstract class CreatureBase : MonoBehaviour, ICreature
    {
        public virtual CreatureType Type {get;protected set;}
        public ICreature Creature {get; private set;}

        public void Init(ICreature creature)
        {
            Creature = creature;
        }
        public void Attack(ICreature target, Vector2 pos)
        {
            Creature.Attack(target,pos);
        }

        public void Die()
        {
            Creature.Die();
        }

        public void TakeDamage(float dmg)
        {
            Creature.TakeDamage(dmg);
        }

        public CreatureData GetCreatureData()
        {
           return Creature.GetCreatureData();
        }
    }
}