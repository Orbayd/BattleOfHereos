using UnityEngine;

namespace BattleOfHeroes.Showcase.Core
{
    public class CreatureFactory
    {
        public Hero CreateHero(GameObject prefab, HeroData data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var hero = go.AddComponent<Hero>();
            var creature = new Creature(data, Enums.CreatureType.Hero, hero);
            hero.Init(creature);
            return hero;
        }

        public Monster CreateMonster(GameObject prefab, MonsterData data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var monster = go.AddComponent<Monster>();
            var creature = new Creature(data, Enums.CreatureType.Monster, monster);
            monster.Init(creature);
            return monster;
        }

        public Monster CreateMonster(GameObject prefab, Vector2 position, int level)
        {
            var MonsterData = new MonsterData()
            {
                Name = "Monster",
                Health = 100,
                AttackPower = 10,
                Level = level
            };

            return CreateMonster(prefab,MonsterData,position);
        }
    }
}