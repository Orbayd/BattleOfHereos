using System;

[Serializable]
public class HeroData : CreatureData
{
    public int Id;
    public bool IsAvailableAtStart;
}

[Serializable]
public class CreatureData
{
    public string Name;
    public float Health;
    public float AttackPower;
    public int StartingLevel;
    public float PowerUpPerLevel = 0.1f;
}
