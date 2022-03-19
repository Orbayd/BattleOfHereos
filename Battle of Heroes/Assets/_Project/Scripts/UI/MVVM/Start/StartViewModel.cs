using System;
using System.Collections;
using System.Collections.Generic;
using BattleOfHeroes.Showcase.Managers;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class StartViewModel : ViewModelBase
    {
        public void OnStartButtonClicked()
        {
            ServiceLocator.GetService<UIManager>().Navigate(Enums.ViewName.HeroSelection);
        }
    }
}
