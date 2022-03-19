using UnityEngine;

namespace BattleOfHeroes.Showcase.Core
{
    public class CreatureFactory
    {
        public Hero CreateHero(GameObject prefab, HeroDbo data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var hero = go.AddComponent<Hero>();
            var creature = new Creature(data, Enums.CreatureType.Hero, hero);
            hero.Init(creature);
            return hero;
        }

        public Monster CreateMonster(GameObject prefab, HeroDbo data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var monster = go.AddComponent<Monster>();
            var creature = new Creature(data, Enums.CreatureType.Monster, monster);
            monster.Init(creature);
            return monster;
        }

        public Monster CreateMonster(GameObject prefab, Vector2 position, int level)
        {
            
            var MonsterData = new HeroData()
            {
                Name = "Monster",
                Health = 100,
                AttackPower = 10,
                Level = level
            };

            var heroDbo = new HeroDbo()
            {
                Level = level,
                HeroData = MonsterData,
                Id = "Monster"
            };

            return CreateMonster(prefab, heroDbo, position);
        }
    }
}