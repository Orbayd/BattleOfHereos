using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Core;
using BattleOfHeroes.Showcase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleOfHeroes.Showcase.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        DependencyManager _dependencyManager;
        void Awake()
        {
            Init();
        }

        void OnDestroy()
        {
            // SceneManager.sceneLoaded -= OnSceneLoaded;
            // SceneManager.sceneUnloaded -= OnSceneUnLoaded;
            RemoveEvents();
        }

        private void Init()
        {
            _dependencyManager.HandleDependencies();
            AddEvents();
        }

        private void OnBattleFinished(BattleFinishedEvent e)
        {
            var _repostioryService = ServiceLocator.GetService<RepositoryService>();
            var _userDbo = _repostioryService.Dbo;
            _userDbo.BattleCount++;
            if(!e.IsLost)
            {
                _userDbo.Level++;
                foreach (var hero in e.Data)
                {
                    var creaturedata = hero.GetCreatureData();
                    creaturedata.Experience++; 
                    if(creaturedata.Experience >=5)
                    {
                        creaturedata.Level++;
                        creaturedata.Experience = 0;
                    }
                }
            }
            if( _userDbo.BattleCount % 5 == 0)
            {
                var nextAvaible = _userDbo.Heroes.First(x=>x.IsAvailable == false);
                nextAvaible.IsAvailable = true;
            }
            
            _repostioryService.Save();
        }

        private void AddEvents()
        {
            Helpers.MessageBus.Subscribe<BattleFinishedEvent>((e)=>OnBattleFinished(e));
        }

        private void RemoveEvents()
        {
            Helpers.MessageBus.UnSubscribe<BattleFinishedEvent>();
        }

        // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     Debug.Log("[INFO] Scene Loaded");
        //     ServiceLocator.GetService<UIManager>().InitPresentation(SceneManager.GetActiveScene().name);
        //     if (scene.name == "GameScene")
        //     {
        //         ServiceLocator.GetService<TurnManager>().Init();
        //     }
        //     else if (scene.name == "MainMenuScene")
        //     {
        //         ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.HeroSelection);
        //     }
        // }

        // void OnSceneUnLoaded(Scene scene)
        // {
        //     Debug.Log("[INFO] Scene UnLoaded");
        //     ServiceLocator.GetService<UIManager>().TerminatePresentation();
        //     if (scene.name == "GameScene")
        //     {
        //         ServiceLocator.GetService<TurnManager>().Terminate();
        //     }
        // }
    }
}