using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHordeConfig", menuName = "Horde Config", order = 51)]
public class HordeConfig : ScriptableObject
{
    public string HordeName;
    public Canvas HordeCanvas;
    public GameObject Wall;
    public GameObject CoinsAndMaterialsDropped;
    public Transform EnemiesObject;
    public Transform spawnArea; 
    public Vector2 areaSize = new Vector2(10f, 10f); 
    public GameObject enemyPrefab;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public float countdownTime = 60f;
    public float spawnInterval = 2f;

}
