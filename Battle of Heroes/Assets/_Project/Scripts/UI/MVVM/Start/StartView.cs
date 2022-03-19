using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleOfHeroes.Showcase.UI
{
    public class StartView : ViewBase<StartViewModel>
    {
        [SerializeField]
        private Button _btnStart;

        protected override void OnBind(StartViewModel model)
        {
            _btnStart.onClick.AddListener(model.OnStartButtonClicked);
        }

        protected override void OnUnBind(StartViewModel model)
        {
            _btnStart.onClick.RemoveAllListeners();
        }


    }
}
