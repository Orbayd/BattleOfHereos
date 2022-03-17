using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroTooltipViewModel : ViewModelBase
    {
        public HeroData Data {get; private set;}

        public HeroTooltipViewModel(HeroData data)
        {
            Data = data;
        }
    }
}
