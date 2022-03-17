using BattleOfHeroes.Showcase.Enums;
using UnityEngine;

namespace BattleOfHeroes.Showcase.Core
{
    public interface ICreature
    {
        void Attack(ICreature target, Vector2 pos);
        void TakeDamage(float dmg);
        void Die();
        CreatureData GetCreatureData();
    }
}