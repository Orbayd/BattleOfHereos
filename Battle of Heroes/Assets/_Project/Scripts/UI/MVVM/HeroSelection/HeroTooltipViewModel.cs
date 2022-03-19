using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroTooltipViewModel : ViewModelBase
    {
        public HeroDbo Data {get; private set;}

        public HeroTooltipViewModel(HeroDbo data)
        {
            Data = data;
        }
    }
}
