using Assets.PixelHeroes.Scripts.ExampleScripts;
using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject ExploreUI;
    public GameObject QuestUI;
    public GameObject TopPanelUI;
    public GameObject ButtonsPanelUI;
    public TMP_Text timerText;
    public TMP_Text CoinsCollected;
    public TMP_Text MaterialsCollected;
    public TMP_Text EnemyKilled;

    private Dictionary<string, int> EnemyExperienceMultiplier = new Dictionary<string, int>();

    private void Start()
    {
        foreach (var config in hordeConfigs)
        {
            config.HordeCanvas.transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() => StartHorde(config));
            config.HordeCanvas.transform.Find("StopButton").GetComponent<Button>().onClick.AddListener(() => StopHorde(config));
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

        config.HordeCanvas.gameObject.SetActive(false);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(false);
        TopPanelUI.SetActive(false);
        ButtonsPanelUI.SetActive(false);
        config.Wall.gameObject.SetActive(true);

        StartCoroutine(SpawnEnemies(config));
    }

    private void StopHorde(HordeConfig config)
    {
        config.countdownTime = 0f;
        isTimerRunning = false;
        EndHorde(config);
    }

    private void EndHorde(HordeConfig config)
    {
        config.HordeCanvas.gameObject.SetActive(true);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(true);
        TopPanelUI.SetActive(true);
        ButtonsPanelUI.SetActive(true);
        config.Wall.gameObject.SetActive(false);

        LTA.HordeFinish();

        getMaterials();
        GetMoney();
        StopAllCoroutines();
        DestroyAllEnemies(config);
        getExperience();

        PickUpSystem.coins = 0;
        PickUpSystem.materials = 0;

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

    private void SpawnEnemy(HordeConfig config)
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(config.spawnArea.position.x - config.areaSize.x / 2, config.spawnArea.position.x + config.areaSize.x / 2),
            Random.Range(config.spawnArea.position.y - config.areaSize.y / 2, config.spawnArea.position.y + config.areaSize.y / 2),
            config.spawnArea.position.z
        );

        GameObject enemy = Instantiate(config.enemyPrefab, randomPosition, Quaternion.identity, config.EnemiesObject);

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

    private void DestroyAllEnemies(HordeConfig config)
    {
        foreach (Transform child in config.CoinsAndMaterialsDropped.transform)
        {
            Destroy(child.gameObject);
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
        if (PickUpSystem.coins == 0)
        {
            CoinsCollected.gameObject.SetActive(false);
        }
        else
        {
            CoinsCollected.gameObject.SetActive(true);
            CoinsCollected.text = PickUpSystem.coins.ToString();
        }

        if (PickUpSystem.materials == 0)
        {
            MaterialsCollected.gameObject.SetActive(false);
        }
        else
        {
            MaterialsCollected.gameObject.SetActive(true);
            MaterialsCollected.text = PickUpSystem.materials.ToString();
        }

        if (GameManager.instance.TempEnemyKilled == 0)
        {
            EnemyKilled.gameObject.SetActive(false);
        }
        else
        {
            EnemyKilled.gameObject.SetActive(true);
            EnemyKilled.text = GameManager.instance.TempEnemyKilled.ToString();
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
