using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpawnConfig", menuName = "ScriptableObjects/SpawnConfig", order = 1)]
public class SpawnConfig : ScriptableObject
{
    [SerializeField]
    public Vector2[] HereosPositon;

    [SerializeField]
    public Vector2 MonsterPosition;

    [SerializeField]
    public GameObject HeroTemplate;

    [SerializeField]
    public GameObject MonsterTemplate;
}
