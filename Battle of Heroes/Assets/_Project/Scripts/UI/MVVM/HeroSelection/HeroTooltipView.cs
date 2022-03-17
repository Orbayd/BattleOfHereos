using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroTooltipView :ViewBase<HeroTooltipViewModel>
    {
        [SerializeField]
        private TMP_Text _txtName; 
        [SerializeField]
        private TMP_Text _txtHealth; 
        
        [SerializeField]
        private TMP_Text _txtAttackPower; 
        
        [SerializeField]
        private TMP_Text _textExperience;

        protected override void OnBind(HeroTooltipViewModel model)
        {
            _txtName.text = model.Data.Name;
            _txtHealth.text = model.Data.Health.ToString();
            _txtAttackPower.text = model.Data.AttackPower.ToString();
            _textExperience.text = model.Data.Experience.ToString();
        }

        private float cooldown = 5.0f;
        void Update()
        {
            cooldown -= Time.deltaTime;

            if(cooldown < 0f)
            {
                Show(false);
                cooldown = 5.0f;
            }
        }
    }
}
