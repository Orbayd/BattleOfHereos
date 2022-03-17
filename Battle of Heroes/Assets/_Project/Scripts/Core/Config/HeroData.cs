using System;

[Serializable]
public class HeroData : CreatureData
{
    //public string Name;
    //public float Health;
    //public float AttackPower;
    public float Experience;
    //public int Level;
}

[Serializable]
public class MonsterData : CreatureData
{
    // public string Name;
    // public float Health;
    // public float AttackPower;
    // public int Level;
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
