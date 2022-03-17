using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIDependency
{

}

[CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObjects/HeroConfig", order = 1)]
public class HeroConfig : ScriptableObject, IUIDependency
{
    [SerializeReference]
    public List<HeroData> Heroes  = new List<HeroData>()
    {
        new HeroData()
        {
            Name = "Jack",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Joe",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Gill",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Virk",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Orb",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Astra",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Kasandra",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Medu",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Keta",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
        new HeroData()
        {
            Name = "Samur",
            Health = 40f,
            AttackPower = 10f,
            Experience  = 0 ,
            Level = 1
        },
    };
}
