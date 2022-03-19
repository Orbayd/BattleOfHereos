using UnityEngine;

namespace BattleOfHeroes.Showcase.Core
{
    public class CreatureFactory
    {
        public Hero CreateHero(GameObject prefab, HeroDbo data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var hero = go.AddComponent<Hero>();
            hero.Init(new CreatureAnimHandler(hero), data);
            return hero;
        }

        public Monster CreateMonster(GameObject prefab, HeroDbo data, Vector2 position)
        {
            var go = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity);
            var monster = go.AddComponent<Monster>();
            monster.Init(new CreatureAnimHandler(monster) ,data);
            return monster;
        }

        public Monster CreateMonster(GameObject prefab, Vector2 position, int level)
        {
            
            var MonsterData = new HeroData()
            {
                Name = "Monster",
                Health = 100,
                AttackPower = 10,
                StartingLevel = level,
                PowerUpPerLevel = 0.2f,
            };

            var heroDbo = new HeroDbo()
            {
                Level = level,
                HeroData = MonsterData,
                Id = 0
            };

            return CreateMonster(prefab, heroDbo, position);
        }
    }
}