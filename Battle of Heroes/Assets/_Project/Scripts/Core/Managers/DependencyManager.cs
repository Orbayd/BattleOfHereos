
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
            }
            if (!ServiceLocator.HasService<TurnManager>())
            {
                var turnManager = new TurnManager(new WorldFactory(_spawnConfig,ServiceLocator.GetService<RepositoryService>().Dbo));
                ServiceLocator.AddService<TurnManager>(turnManager);
            }
        }

    }

}