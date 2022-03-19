using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class BattleViewModel :ViewModelBase
    {
        public UserDbo Dbo { get; private set;}
        public BattleViewModel(UserDbo dbo)
        {
            Dbo = dbo;
        }
    }
}
