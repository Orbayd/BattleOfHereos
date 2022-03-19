using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterConfig", menuName = "ScriptableObjects/MonsterConfig", order = 1)]
public class MonsterConfig : ScriptableObject
{
    public HeroData Monster;
    public List<Sprite> MonsterSprites = new List<Sprite>();
}


