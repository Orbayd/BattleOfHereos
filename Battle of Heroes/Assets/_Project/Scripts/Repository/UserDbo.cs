using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public interface IModel { }

[Serializable]
public class UserDbo : IModel
{
    public int Level { get; set; } = 1;
    public int BattleCount { get; set; } = 0;
    public List<HeroDbo> Heroes { get; set; }

}

public class HeroDbo : IModel
{
    public int Experience { get; set; }
    public int Level { get; set; }
    public int Id { get; set; }
    public bool IsAvailable { get; set; }

    [JsonIgnore]
    public HeroData HeroData { get; set; }
}
