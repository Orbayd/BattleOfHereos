using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Core;

namespace BattleOfHeroes.Showcase.Managers
{
    public class WorldFactory
    {
        private SpawnConfig _spawnConfig;
        private CreatureFactory _factory;

        public WorldFactory(SpawnConfig config)
        {
            _spawnConfig = config;
    
            _factory = new CreatureFactory();
        }

        public List<CreatureBase> CreateHeroes()
        {
            var heroesdata = new List<HeroDbo>();
            var heros = new List<CreatureBase>();

            if (PersistentStorage.SelectedHeroes.Any())
            {
                heroesdata = PersistentStorage.SelectedHeroes;
            }
            else
            {
                heroesdata = ServiceLocator.GetService<RepositoryService>().Dbo.Heroes.Take(3).ToList();// This is fore easy lunch of game scnene not used
            }

            for (int i = 0; i < heroesdata.Count; i++)
            {
                var hero = _factory.CreateHero(_spawnConfig.HeroTemplate, heroesdata[i], _spawnConfig.HereosPositon[i]);
                heros.Add(hero);
            }

            return heros;
        }

        public List<CreatureBase> CreateMonsters()
        {
            var monsters = new List<CreatureBase>();
            var monster = _factory.CreateMonster(_spawnConfig.MonsterTemplate, _spawnConfig.MonsterPosition, 1);
            monsters.Add(monster);
            return monsters;
        }
    }
}
