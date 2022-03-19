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
    [SerializeField]
    public List<SceneDependencies>  SceneViews;

    [SerializeField]
    public List<ViewDependencies> Commons;
}

[Serializable]
public class SceneDependencies
{
    [SerializeField]
    public string SceneName; 
    
    [SerializeField]
    public List<ViewDependencies> Views;
}

[Serializable]
public class ViewDependencies
{
    public ViewName Name; 
    public GameObject Prefab;
}

