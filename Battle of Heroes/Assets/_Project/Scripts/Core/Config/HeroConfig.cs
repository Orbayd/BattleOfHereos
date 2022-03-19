using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObjects/HeroConfig", order = 1)]
public class HeroConfig : ScriptableObject
{
    [SerializeReference]
    public List<HeroData> Heroes  = new List<HeroData>()
    {
        new HeroData()
        {
            Id = 1,
            Name = "Jack",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 2,
            Name = "Joe",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 3,
            Name = "Gill",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 4,
            Name = "Virk",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 5,
            Name = "Orb",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 6,
            Name = "Astra",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 7,
            Name = "Kasandra",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true
        },
        new HeroData()
        {
            Id = 8,
            Name = "Medu",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true,
        },
        new HeroData()
        {
            Id = 9,
            Name = "Keta",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true,
        },
        new HeroData()
        {
            Id = 10,
            Name = "Samur",
            Health = 40f,
            AttackPower = 10f,
            StartingLevel = 1,
            IsAvailableAtStart = true,
        },
    };
    
}  

