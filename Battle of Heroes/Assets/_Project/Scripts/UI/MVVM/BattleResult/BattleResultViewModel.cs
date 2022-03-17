using System;
using System.Collections;
using System.Collections.Generic;
using BattleOfHeroes.Showcase.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleOfHeroes.Showcase.UI
{
    public class BattleResultViewModel : ViewModelBase
    {
        public bool _isBattleLost;
        public bool IsBattleLost { get { return _isBattleLost; } set { _isBattleLost = value; NotifyPropertyChanged(); } }

        public void OnReturnButtonClicked()
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
        private void OnBattleFinished(BattleFinishedEvent e)
        {
            IsBattleLost = e.IsLost;
        }
        public override void OnBind()
        {
            MessageBus.Subscribe<BattleFinishedEvent>(e => OnBattleFinished(e));
        }

        public override void OnUnBind()
        {
            MessageBus.UnSubscribe<BattleFinishedEvent>();
        }

    }
}
