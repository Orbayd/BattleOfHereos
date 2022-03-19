using System;

[Serializable]
public class HeroData : CreatureData
{
    public float Experience;
}

[Serializable]
public class MonsterData : CreatureData
{

}

[Serializable]
public class CreatureData
{
    public string Name;
    public float Health;
    public float AttackPower;
    public int Level;
    public float PowerUpPerLevel = 0.1f;
}
