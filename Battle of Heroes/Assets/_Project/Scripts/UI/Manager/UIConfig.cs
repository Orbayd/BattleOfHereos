using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig", order = 1)]
public class UIConfig : ScriptableObject
{
    // [SerializeField]
    // private ViewDependencies[] _viewDependencies;
    [SerializeReference]
    public ViewModelBase battleResult;

    [SerializeField]
    private ViewModelDependencies[] ViewModelDependencies;

    public  ViewModelBase CreateViewModel(ViewName name, params object[] dependencies)
    {
        var d = ViewModelDependencies.First(x=>x.Name == name);
        switch (name)
        {
            case ViewName.HeroSelection :
                return new HeroSelectionViewModel(d.dependency.First() as HeroConfig);
            case ViewName.BattleResult :
                return new BattleResultViewModel();
            default:break;
        }

        return null;
    }
}
[Serializable]
public class ViewDependencies
{
    public ViewName Name; 
    
    [SerializeReference]
    public IView View;
}
[Serializable]
public class ViewModelDependencies
{
    public ViewName Name;
    
    public ScriptableObject[] dependency;
}
