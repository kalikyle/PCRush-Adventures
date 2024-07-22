using Assets.PixelHeroes.Scripts.ExampleScripts;
using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class HorderManager : MonoBehaviour
{
    public List<HordeConfig> hordeConfigs; // List of all horde configurations
    private HordeConfig currentHordeConfig;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isTimerRunning = false;

    public PickUpSystem PickUpSystem;
    public LeanTweenAnimate LTA;
    public GameObject OnHordeUI;
    public GameObject ExploreUI;
    public GameObject QuestUI;
    public GameObject TopPanelUI;
    public GameObject ButtonsPanelUI;
    public TMP_Text timerText;
    public TMP_Text CoinsCollected;
    public TMP_Text MaterialsCollected;
    public TMP_Text EnemyKilled;
    public Button generalStopButton;

    // Scene-specific references for CPU cpu1
    public Canvas CPUWorldCanvas;
    public GameObject Wall;
    public GameObject CoinsAndMaterialsDropped;
    public Button cpuStartButton;
    public PolygonCollider2D CPU1spawnAreaCollider; // Add this line

    // Scene-specific references for RAM ram1
    public Canvas RAMWorldCanvas;
    public GameObject ramWall;
    public GameObject RamCoinsAndMaterialsDropped;
    public Button ramStartButton;
    public PolygonCollider2D RAM1spawnAreaCollider;

    // Scene-specific references for CPUF cpuf1
    public Canvas CPUFWorldCanvas;
    public GameObject cpufWall;
    public GameObject CPUFCoinsAndMaterialsDropped;
    public Button cpufStartButton;
    public PolygonCollider2D CPUF1spawnAreaCollider;

    // Scene-specific references for GPU gpu1
    public Canvas GPUWorldCanvas;
    public GameObject gpuWall;
    public GameObject GPUCoinsAndMaterialsDropped;
    public Button gpuStartButton;
    public PolygonCollider2D GPU1spawnAreaCollider;

    // General stop button (e.g., in the Horde UI)

    private Dictionary<string, PolygonCollider2D> SpawnArea = new Dictionary<string, PolygonCollider2D>();
    private Dictionary<string, GameObject> MaterialsandCoinsDrop = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> walls = new Dictionary<string, GameObject>();
    private Dictionary<string, Canvas> worldCanvases = new Dictionary<string, Canvas>();

    private Dictionary<string, int> EnemyExperienceMultiplier = new Dictionary<string, int>();

    private void Start()
    {

        MaterialsandCoinsDrop["cpu1"] = CoinsAndMaterialsDropped;
        MaterialsandCoinsDrop["ram1"] = RamCoinsAndMaterialsDropped;
        MaterialsandCoinsDrop["cpuf1"] = CPUFCoinsAndMaterialsDropped;
        MaterialsandCoinsDrop["gpu1"] = GPUCoinsAndMaterialsDropped;

        walls["cpu1"] = Wall;
        walls["ram1"] = ramWall;
        walls["cpuf1"] = cpufWall;
        walls["gpu1"] = gpuWall;

        worldCanvases["cpu1"] = CPUWorldCanvas;
        worldCanvases["ram1"] = RAMWorldCanvas;
        worldCanvases["cpuf1"] = CPUFWorldCanvas;
        worldCanvases["gpu1"] = GPUWorldCanvas;

        SpawnArea["cpu1"] = CPU1spawnAreaCollider;
        SpawnArea["ram1"] = RAM1spawnAreaCollider;
        SpawnArea["cpuf1"] = CPUF1spawnAreaCollider;
        SpawnArea["gpu1"] = GPU1spawnAreaCollider;

        cpuStartButton.onClick.AddListener(() => StartHorde(hordeConfigs.Find(config => config.HordeName == "cpu1")));

        ramStartButton.onClick.AddListener(() => StartHorde(hordeConfigs.Find(config => config.HordeName == "ram1")));

        cpufStartButton.onClick.AddListener(() => StartHorde(hordeConfigs.Find(config => config.HordeName == "cpuf1")));

        gpuStartButton.onClick.AddListener(() => StartHorde(hordeConfigs.Find(config => config.HordeName == "gpu1")));

        generalStopButton.onClick.AddListener(StopCurrentHorde);

        foreach (var config in hordeConfigs)
        {
            UpdateTimerText(config, config.countdownTime); // Initialize the timer text
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentHordeConfig.countdownTime -= Time.deltaTime;

            if (currentHordeConfig.countdownTime <= 0f)
            {
                currentHordeConfig.countdownTime = 0f;
                isTimerRunning = false;
                EndHorde(currentHordeConfig);
            }

            UpdateTimerText(currentHordeConfig, currentHordeConfig.countdownTime);
            UpdateUI(currentHordeConfig);
        }
    }

    private void StartHorde(HordeConfig config)
    {
        currentHordeConfig = config;
        config.countdownTime = 120f; // Reset the countdown time to 2 minutes
        isTimerRunning = true; // Start the timer

        if (worldCanvases.TryGetValue(config.HordeName, out var canvas))
        {
            canvas.gameObject.SetActive(false);
        }
        OnHordeUI.SetActive(true);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(false);
        TopPanelUI.SetActive(false);
        ButtonsPanelUI.SetActive(false);

        if (walls.TryGetValue(config.HordeName, out var wall))
        {
            wall.gameObject.SetActive(true);
        }

        StartCoroutine(SpawnEnemies(config));
    }

    private async void StopHorde(HordeConfig config)
    {
        if (config == null) return; // Avoid null reference errors

        if (worldCanvases.TryGetValue(config.HordeName, out var canvas))
        {
            canvas.gameObject.SetActive(true);
        }
        OnHordeUI.SetActive(false);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(true);
        TopPanelUI.SetActive(true);
        ButtonsPanelUI.SetActive(true);

        if (walls.TryGetValue(config.HordeName, out var wall))
        {
            wall.gameObject.SetActive(false);
        }


        config.countdownTime = 0f;
        isTimerRunning = false;
        StopAllCoroutines();
        DestroyAllEnemies(config);

        PickUpSystem.coins = 0;
        PickUpSystem.materials = 0;

        await Task.Delay(1500);
        GameManager.instance.TempEnemyKilled = 0;
        EnemyExperienceMultiplier.Clear();
    }

    public void StopCurrentHorde()
    {
        StopHorde(currentHordeConfig);
    }

    private async void EndHorde(HordeConfig config)
    {
        if (worldCanvases.TryGetValue(config.HordeName, out var canvas))
        {
            canvas.gameObject.SetActive(true);
        }
        OnHordeUI.SetActive(false);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(true);
        TopPanelUI.SetActive(true);
        ButtonsPanelUI.SetActive(true);

        if (walls.TryGetValue(config.HordeName, out var wall))
        {
            wall.gameObject.SetActive(false);
        }

        LTA.HordeFinish();

        getMaterials();
        GetMoney();
        getExperience();
        StopAllCoroutines();
        DestroyAllEnemies(config);
        
        PickUpSystem.coins = 0;
        PickUpSystem.materials = 0;

        await Task.Delay(1500);
        GameManager.instance.TempEnemyKilled = 0;
        EnemyExperienceMultiplier.Clear();
    }

    private IEnumerator SpawnEnemies(HordeConfig config)
    {
        while (isTimerRunning)
        {
            SpawnEnemy(config);
            yield return new WaitForSeconds(config.spawnInterval);
        }
    }

    //private void SpawnEnemy(HordeConfig config)
    //{
    //    if (!spawnAreas.TryGetValue(config.HordeName, out var spawnArea) ||
    //        !enemiesObjects.TryGetValue(config.HordeName, out var enemiesObject))
    //    {
    //        Debug.LogError("Spawn area or enemies object not found for horde: " + config.HordeName);
    //        return;
    //    }

    //    Vector3 randomPosition = new Vector3(
    //        Random.Range(spawnArea.position.x - config.areaSize.x / 2, spawnArea.position.x + config.areaSize.x / 2),
    //        Random.Range(spawnArea.position.y - config.areaSize.y / 2, spawnArea.position.y + config.areaSize.y / 2),
    //        spawnArea.position.z
    //    );

    //    GameObject enemy = Instantiate(config.enemyPrefab, randomPosition, Quaternion.identity, enemiesObject);

    //    EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

    //    if (enemyAI != null)
    //    {
    //        enemyAI.numberOfHeartsToDrop = Random.Range(0, 2);
    //        enemyAI.numberOfCoinsToDrop = Random.Range(0, 6);
    //        enemyAI.HeartValueToDrop = Random.Range(1, 6);
    //        enemyAI.CoinValueToDrop = Random.Range(1, 6);
    //        enemyAI.numberOfMaterialToDrop = Random.Range(0, 2);
    //        enemyAI.MaterialValueToDrop = 1;
    //    }

    //    EnemyExperienceMultiplier[enemyAI.name] = enemyAI.ExpMultiplier;
    //    spawnedEnemies.Add(enemy);
    //}

    private void SpawnEnemy(HordeConfig config)
    {
        PolygonCollider2D spawnAreaCollider = null;

        if (SpawnArea.TryGetValue(config.HordeName, out var spawnarea))
        {
            spawnAreaCollider = spawnarea;
        }

        //if (config.HordeName == "cpu1")
        //{
        //    spawnAreaCollider = CPU1spawnAreaCollider;
        //}
        //else if (config.HordeName == "ram1")
        //{
        //    spawnAreaCollider = RAM1spawnAreaCollider;
        //}
        //else if (config.HordeName == "cpuf1")
        //{
        //    spawnAreaCollider = CPUF1spawnAreaCollider;
        //}

        if (spawnAreaCollider == null || !spawnAreaCollider.enabled)
        {
            Debug.LogError("Spawn area collider not found or disabled for horde: " + config.HordeName);
            return;
        }

        Vector3 randomPosition = GetRandomPointInPolygon(spawnAreaCollider);
        GameObject enemy = Instantiate(config.enemyPrefab, randomPosition, Quaternion.identity);

        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.numberOfHeartsToDrop = Random.Range(0, 2);
            enemyAI.numberOfCoinsToDrop = Random.Range(0, 6);
            enemyAI.HeartValueToDrop = Random.Range(1, 6);
            enemyAI.CoinValueToDrop = Random.Range(1, 6);
            enemyAI.numberOfMaterialToDrop = Random.Range(0, 2);
            enemyAI.MaterialValueToDrop = 1;
        }

        EnemyExperienceMultiplier[enemyAI.name] = enemyAI.ExpMultiplier;
        spawnedEnemies.Add(enemy);
    }

    private Vector3 GetRandomPointInPolygon(PolygonCollider2D collider)
    {
        Bounds bounds = collider.bounds;
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        while (true)
        {
            Vector2 randomPoint = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)
            );

            if (PointInPolygon(collider, randomPoint))
            {
                return randomPoint;
            }
        }
    }

    private bool PointInPolygon(PolygonCollider2D collider, Vector2 point)
    {
        bool inside = false;
        Vector2[] points = collider.points;
        int j = points.Length - 1;

        for (int i = 0; i < points.Length; j = i++)
        {
            Vector2 pi = collider.transform.TransformPoint(points[i]);
            Vector2 pj = collider.transform.TransformPoint(points[j]);

            if ((pi.y > point.y) != (pj.y > point.y) &&
                (point.x < (pj.x - pi.x) * (point.y - pi.y) / (pj.y - pi.y) + pi.x))
            {
                inside = !inside;
            }
        }

        return inside;
    }


    private void DestroyAllEnemies(HordeConfig config)
    {

        //foreach (Transform child in CoinsAndMaterialsDropped.transform)
        //{
        //    Destroy(child.gameObject);
        //}

        if (MaterialsandCoinsDrop.TryGetValue(config.HordeName, out var coinsandmat))
        {
            foreach (Transform child in coinsandmat.transform)
        {
             Destroy(child.gameObject);
        }
        }

        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Health health = enemy.GetComponent<Health>();
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

                if (enemyAI != null)
                {
                    enemyAI.numberOfHeartsToDrop = 0;
                    enemyAI.numberOfCoinsToDrop = 0;
                    enemyAI.HeartValueToDrop = 0;
                    enemyAI.CoinValueToDrop = 0;
                    enemyAI.numberOfMaterialToDrop = 0;
                    enemyAI.MaterialValueToDrop = 0;
                }

                if (health != null)
                {
                    health.currentHealth = 0;
                }
            }
        }
        spawnedEnemies.Clear();
    }

    private void UpdateTimerText(HordeConfig config, float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateUI(HordeConfig config)
    {
        CoinsCollected.gameObject.SetActive(PickUpSystem.coins > 0);
        CoinsCollected.text = PickUpSystem.coins.ToString();

        MaterialsCollected.gameObject.SetActive(PickUpSystem.materials > 0);
        MaterialsCollected.text = PickUpSystem.materials.ToString();

        if (MaterialsCollected.transform.childCount > 0)
        {
            Image materialImage = MaterialsCollected.transform.GetChild(0).GetComponent<Image>();
            if (materialImage != null)
            {
                materialImage.sprite = PickUpSystem.materialImage;
            }
        }

        EnemyKilled.gameObject.SetActive(GameManager.instance.TempEnemyKilled > 0);
        EnemyKilled.text = GameManager.instance.TempEnemyKilled.ToString();

        if (EnemyKilled.transform.childCount > 0)
        {
            Image materialImage = EnemyKilled.transform.GetChild(0).GetComponent<Image>();
            if (materialImage != null)
            {
                EnemyAI enemyAI = config.enemyPrefab.GetComponent<EnemyAI>();
                materialImage.sprite = enemyAI.EnemyImage;
            }
        }
    }

    private void getExperience()
    {
        int totalExperience = 0;
        foreach (var pair in EnemyExperienceMultiplier)
        {
            totalExperience += pair.Value * GameManager.instance.TempEnemyKilled;
        }

        LTA.expcollected.text = totalExperience.ToString();
        LTA.showkills.text = GameManager.instance.TempEnemyKilled.ToString();
        GameManager.instance.AddPlayerExp(totalExperience);
    }

    private void getMaterials()
    {
        OtherWorldItem inventoryItem = ConvertMaterialsToInventoryItem();

        if (inventoryItem.isEmpty)
        {
            GameManager.instance.ShowPopUpEquipments(inventoryItem);
            Debug.LogError("Null inventory item returned from conversion.");
        }
        else
        {
            GameManager.instance.AddItemToTransfer(inventoryItem);
            Debug.LogError("Item added to inventory ");
        }
    }

    private void GetMoney()
    {
        LTA.coinscollected.text = PickUpSystem.coins.ToString();
        GameManager.instance.PlayerMoney += PickUpSystem.coins;
    }

    private OtherWorldItem ConvertMaterialsToInventoryItem()
    {
        LTA.materialscollect.text = PickUpSystem.materials.ToString();
        OtherWorldItem inventoryItem = new OtherWorldItem();

        inventoryItem.quantity = PickUpSystem.materials;
        inventoryItem.item = ConvertMaterial();

        return inventoryItem;
    }

    private OtherWorldItemSO ConvertMaterial()
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();

        inventoryItems.name = PickUpSystem.materialname;

        var spriteArray = GameManager.instance.SpriteCollections.Layers;

        inventoryItems.Name = PickUpSystem.materialname;
        inventoryItems.ItemImage = PickUpSystem.materialImage;
        inventoryItems.Category = "Materials";

        inventoryItems.IsStackable = true;
        inventoryItems.MaxStackableSize = 9999;
        inventoryItems.SpriteIndex = -1;

        return inventoryItems;
    }
}
