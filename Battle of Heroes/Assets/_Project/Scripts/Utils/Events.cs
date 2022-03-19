using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BattleOfHeroes.Showcase.Core;
using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.UI;
using UnityEngine;

public class HeroSelectedEvent
{
    public HeroDbo Data {get; private set;}

    public HeroPortraitViewModel HeroPortrait {get; private set;}

    public HeroSelectedEvent(HeroDbo data ,HeroPortraitViewModel viewModel)
    {
        this.Data = data;
        HeroPortrait = viewModel;
    }
}

public class ShowToolTipEvent
{
    public HeroDbo Data {get; private set;}

    public Vector2 Position {get; private set;}

    public ShowToolTipEvent(HeroDbo data, Vector2 position)
    {
        Data = data;
        Position = position;
    }
}

public class SceneChangeEvent
{
    public int SceneIndex {get; private set;}
    public ReadOnlyCollection<HeroData> SelectedHereos {get; private set;}
    public SceneChangeEvent(int sceneIndex ,IEnumerable<HeroData> heroData)
    {
        SceneIndex = sceneIndex;
        SelectedHereos = Array.AsReadOnly(heroData.ToArray());
    }
}
public class HeroAttackEvent
{
    public ICreature Creature {get; private set;}
    public CreatureType Type {get; private set;}

    public HeroAttackEvent(ICreature data, CreatureType type)
    {
       Creature = data;
       Type = type;
    }
}

public class DamageTaken
{
    public CreatureBase Creature {get; private set;}
    public CreatureType Type {get; private set;}
    public bool IsAlive { get; private set; }

    public DamageTaken(CreatureBase creature, CreatureType type, bool isAlive)
    {
       Creature = creature;
       Type = type;
       IsAlive = isAlive;
    }
}
public class BattleFinishedEvent
{
    public bool IsLost {get; private set;}
    public CreatureBase[] Data {get; private set;}
    public BattleFinishedEvent(bool status,CreatureBase[] data)
    {
        IsLost = status;
        Data = data;
    }
}

