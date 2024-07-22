using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHordeConfig", menuName = "Horde Config", order = 51)]
public class HordeConfig : ScriptableObject
{
    public string HordeName;
    public float spawnInterval = 2f;
    public float countdownTime = 60f;
    public GameObject enemyPrefab;
    
}
