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
        private static GameManager _instance;
        private static bool _isFirstPlay = true;

        [SerializeField]
        private DependencyManager _dependencyManager;
        void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                Init();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void OnDestroy()
        {
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
            if (!e.IsLost)
            {
                _userDbo.Level++;
                foreach (var hero in e.Data)
                {
                    var creaturedata = hero.GetCreatureData();
                    creaturedata.Experience++;
                    if (creaturedata.Experience >= 5)
                    {
                        creaturedata.Level++;
                        creaturedata.Experience = 0;
                    }
                }
            }
            if (_userDbo.BattleCount % 5 == 0)
            {
                var nextAvaible = _userDbo.Heroes.OrderBy(x => x.Id).FirstOrDefault(x => x.IsAvailable == false);
                if (nextAvaible != null)
                {
                    nextAvaible.IsAvailable = true;
                }
            }
            ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.BattleResult);
            _repostioryService.Save();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"[INFO] Scene Loaded {scene}");
            ServiceLocator.GetService<UIManager>().InitPresentation(SceneManager.GetActiveScene().name);
            if (scene.name == "GameScene")
            {
                ServiceLocator.GetService<TurnManager>().Init();
                ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.Battle);
            }
            else if (scene.name == "MainMenuScene")
            {
                if(_isFirstPlay)
                {
                    ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.Start);
                    _isFirstPlay = false;
                }
                else
                {
                    ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.HeroSelection);
                }
            }
        }

        void OnSceneUnLoaded(Scene scene)
        {
            Debug.Log($"[INFO] Scene UnLoaded {scene}");
            ServiceLocator.GetService<UIManager>().TerminatePresentation();
            if (scene.name == "GameScene")
            {
                ServiceLocator.GetService<TurnManager>().Terminate();
            }
        }

        private void AddEvents()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
            Helpers.MessageBus.Subscribe<BattleFinishedEvent>((e) => OnBattleFinished(e));
        }

        private void RemoveEvents()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnLoaded;
            Helpers.MessageBus.UnSubscribe<BattleFinishedEvent>();
        }
    }
}