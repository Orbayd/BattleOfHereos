
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleOfHeroes.Showcase.Managers
{
    public class DependencyManager : MonoBehaviour
    {
        [Header("Repository Dependencies")]
        [SerializeField]
        private HeroConfig _heroConfig;

        [Header("UI Dependencies")]
        [SerializeField]
        private UIConfig _uiConfig;

        [Header("World Dependencies")]
        [SerializeField]
        private SpawnConfig _spawnConfig;

        public void HandleDependencies()
        {
            if (!ServiceLocator.HasService<RepositoryService>())
            {
                ServiceLocator.AddService<RepositoryService>(new RepositoryService(_heroConfig));
                ServiceLocator.GetService<RepositoryService>().Load();
            }

            if (!ServiceLocator.HasService<UIManager>())
            {
                ServiceLocator.AddService<UIManager>(new UIManager(_uiConfig, ServiceLocator.GetService<RepositoryService>().Dbo));
                ServiceLocator.GetService<UIManager>().Init();
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.sceneUnloaded += OnSceneUnLoaded;
            }
            if (!ServiceLocator.HasService<TurnManager>())
            {
                var wordFactory = new WorldFactory(_spawnConfig);
                var turnManager = new TurnManager(wordFactory, ServiceLocator.GetService<UIManager>());
                ServiceLocator.AddService<TurnManager>(turnManager);
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"[INFO] Scene Loaded {scene}");
            ServiceLocator.GetService<UIManager>().InitPresentation(SceneManager.GetActiveScene().name);
            if (scene.name == "GameScene")
            {
                ServiceLocator.GetService<TurnManager>().Init();
            }
            else if (scene.name == "MainMenuScene")
            {
                ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.HeroSelection);
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
    }

}