using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class BattleView : ViewBase<BattleViewModel>
    {
        [SerializeField]
        private TMP_Text txtLevel;

        protected override void OnBind(BattleViewModel model)
        {
           txtLevel.text = "Level :" + model.Dbo.Level.ToString();
        }

        protected override void OnUnBind(BattleViewModel model)
        {
    
        }
    }
}