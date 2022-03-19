using System.Collections.Generic;

namespace BattleOfHeroes.Showcase.Managers
{
    public static class PersistentStorage
    {
        public static List<HeroDbo> SelectedHeroes { get; set; } = new List<HeroDbo>();
    }
}