using System;
using System.Collections;
using System.Collections.Generic;
using BattleOfHeroes.Showcase.Helpers;
using BattleOfHeroes.Showcase.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroSelectionViewModel : ViewModelBase
    {
        public List<HeroData> Itemsource {get; private set;} = new List<HeroData>();
        
        private List<HeroData> _selectedHereos  = new List<HeroData>();

        public HeroSelectionViewModel(HeroConfig config)
        {
            Itemsource = config.Heroes;
        }

        public void OnPlayButtonClicked()
        {
            //MessageBus.Publish<SceneChangeEvent>(new SceneChangeEvent(1,_selectedHereos));
            PersistentStorage.SelectedHeroes = _selectedHereos;
            var scene = SceneManager.LoadSceneAsync(1, new LoadSceneParameters(LoadSceneMode.Single) { });
        }

        public override void OnBind()
        {
            AddEvents();
        }

        public override void OnUnBind()
        {
           RemoveEvents();
        }
        private void OnHeroSelected(HeroSelectedEvent e)
        {
            if (e.HeroPortrait.IsSelected)
            {
                e.HeroPortrait.IsSelected = false;
                _selectedHereos.Remove(e.Data);
            }
            else
            {
                if (_selectedHereos.Count < 3)
                {
                    _selectedHereos.Add(e.Data);
                    //Debug.Log($"INFO] Hero Selected {e.Data.Name}");
                    e.HeroPortrait.IsSelected = true;
                }
            }
        }

        private void AddEvents()
        {
            MessageBus.Subscribe<HeroSelectedEvent>((e)=> OnHeroSelected(e));
        }

        private void RemoveEvents()
        {
            MessageBus.UnSubscribe<HeroSelectedEvent>();
        }

    }
}
