using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Core;
using UnityEngine;

namespace BattleOfHeroes.Showcase.Managers
{
    public class WorldFactory
    {
        private SpawnConfig _spawnConfig;
        private CreatureFactory _factory;
        private UserDbo _dbo;
        private MonsterConfig _monsterConfig;
        public WorldFactory(SpawnConfig config, MonsterConfig monsterConfig, UserDbo dbo)
        {
            _spawnConfig = config;
            _dbo = dbo;
            _factory = new CreatureFactory();
            _monsterConfig = monsterConfig;
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
                hero.InitBillboard(CreateBillboard(_spawnConfig.BillboardTemplate));
                heros.Add(hero);
            }

            return heros;
        }

        public List<CreatureBase> CreateMonsters()
        {
            var monsters = new List<CreatureBase>();
            var index = UnityEngine.Random.Range(0,_monsterConfig.MonsterSprites.Count);
            _monsterConfig.Monster.Sprite = _monsterConfig.MonsterSprites[index];
            var monster = _factory.CreateMonster(_spawnConfig.MonsterTemplate,_monsterConfig.Monster, _spawnConfig.MonsterPosition, _dbo.Level);
            monster.InitBillboard(CreateBillboard(_spawnConfig.BillboardTemplate));
            monsters.Add(monster);
            return monsters;
        }

        public HealthBarUI CreateBillboard(GameObject billboard)
        {
            return GameObject.Instantiate(billboard,MonoBehaviour.FindObjectOfType<Canvas>().transform).GetComponent<HealthBarUI>();
        }
    }
}
