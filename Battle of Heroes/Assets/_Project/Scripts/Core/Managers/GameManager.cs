using System;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Helpers;
using BattleOfHeroes.Showcase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleOfHeroes.Showcase.Managers
{
    public static class PersistentStorage
    {
        public static List<HeroData> SelectedHeroes { get; set; } = new List<HeroData>();
    }
    public class GameManager : MonoBehaviour
    {
        [Header("Repository Dependencies")]
        [SerializeField]
        private HeroConfig _heroConfig;

        [SerializeField]
        private UIManager _uIManager;

        private RepositoryManager _repository;

        private void Start()
        {
           Init();
        }

        private void OnDestroy()
        {
            Terminate();
        }

        private void Init()
        {
            InitRepository();
            InitUI();
            AddEvents();
        }

        private void Terminate()
        {
            RemoveEvents();
            _uIManager.Terminate();
        }

        private void InitRepository()
        {
            _repository = new RepositoryManager(_heroConfig);
            _repository.Load();
        }

        private void InitUI()
        {
            _uIManager.Init();
        }

        private void AddEvents()
        {
         
        }
        
        private void RemoveEvents()
        {
            
        }
    }
}