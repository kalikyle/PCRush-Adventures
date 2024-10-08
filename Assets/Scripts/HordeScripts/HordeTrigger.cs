using Assets.PixelHeroes.Scripts.ExampleScripts;
using OtherWorld.Model;
using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class HordeTrigger : MonoBehaviour
{

    public PickUpSystem PickUpSystem;

    public TMP_Text timerText; 

    public Button startButton;
    public Button stopButton;

    public Canvas CPUWorldCanvas;
    public GameObject ONHordeUI;
    public GameObject ExploreUI;
    public GameObject QuestUI;
    public GameObject TopPanelUI;
    public GameObject ButtonsPanelUI;

    //for CPU World
    public GameObject Wall;
    public GameObject CoinsAndMaterialsDropped;
    public Transform EnemiesObject;
    public Transform spawnArea; // The Transform defining the center of the spawning area
    public Vector2 areaSize = new Vector2(10f, 10f); // The size of the spawning area
    public GameObject enemyPrefab; // The enemy AI prefab to spawn
    private List<GameObject> spawnedEnemies = new List<GameObject>();




    public TMP_Text CoinsCollected;
    public TMP_Text MaterialsCollected;
    public TMP_Text Enemykilled;


  
    private Dictionary<string, int> EnemyExperienceMultiplier = new Dictionary<string, int>();


    public float spawnInterval = 2f; // Time interval between spawns
    public float countdownTime = 60f; // Countdown time in seconds (2 minutes)
    private bool isTimerRunning = false;

    public LeanTweenAnimate LTA;

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned.");
        }
        if (startButton == null)
        {
            Debug.LogError("Start Button is not assigned.");
        }
        else
        {
            startButton.onClick.AddListener(StartTimer);
        }


        if (stopButton == null)
        {
            Debug.LogError("Stop Button is not assigned.");
        }
        else
        {
            stopButton.onClick.AddListener(StopTimer);
        }
       



        UpdateTimerText(countdownTime); // Initialize the timer text
    }

    private async void Update()
    {
        if (isTimerRunning)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                isTimerRunning = false;
                CPUWorldCanvas.gameObject.SetActive(true);
                ExploreUI.SetActive(true);
                QuestUI.SetActive(true);
                TopPanelUI.SetActive(true);
                ButtonsPanelUI.SetActive(true);
                ONHordeUI.SetActive(false);
                Wall.gameObject.SetActive(false);

                LTA.HordeFinish();

                getMaterials();
                GetMoney();
                StopAllCoroutines();
                DestroyAllEnemies();
                getExperience();

                PickUpSystem.coins = 0;
                PickUpSystem.materials = 0;

                await Task.Delay(1500);
                GameManager.instance.TempEnemyKilled = 0;
                EnemyExperienceMultiplier.Clear();
            }

            UpdateTimerText(countdownTime);



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


            if (GameManager.instance.TempEnemyKilled == 0) {

                Enemykilled.gameObject.SetActive(false);

            }
            else
            {
                Enemykilled.gameObject.SetActive(true);
                Enemykilled.text = GameManager.instance.TempEnemyKilled.ToString();
            }


        }
    }

    public void getExperience()
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
    public void getMaterials()
    {
        OtherWorldItem inventoryItem = ConvertMaterialsToInventoryItem();
        
        if (inventoryItem.isEmpty)
        {
            GameManager.instance.ShowPopUpEquipments(inventoryItem);
            Debug.LogError("Null inventory item returned from conversion.");
        }
        else
        {

            //data.AddItem(inventoryItem);
            //data.AddItemList(inventoryItem.item);
            GameManager.instance.AddItemToTransfer(inventoryItem);
            Debug.LogError("Item added to inventory ");
        }
    }

    public void GetMoney()
    {
        LTA.coinscollected.text = PickUpSystem.coins.ToString();
        GameManager.instance.PlayerMoney += PickUpSystem.coins;
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
    }

    private OtherWorldItem ConvertMaterialsToInventoryItem()
    {
        LTA.materialscollect.text = PickUpSystem.materials.ToString();
        OtherWorldItem inventoryItem = new OtherWorldItem();
        
        inventoryItem.quantity = PickUpSystem.materials;
        inventoryItem.item = ConvertMaterial();

        return inventoryItem;
    }

    public OtherWorldItemSO ConvertMaterial()
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();

        inventoryItems.name = PickUpSystem.materialname;

        var spriteArray = GameManager.instance.SpriteCollections.Layers;

        inventoryItems.Name = PickUpSystem.materialname;
        inventoryItems.ItemImage = PickUpSystem.materialImage;
        inventoryItems.Category = "Materials";

        //if want not stackable
        inventoryItems.IsStackable = true;
        inventoryItems.MaxStackableSize = 9999;
        inventoryItems.SpriteIndex = -1;
        // if stackable
        //inventoryItems.IsStackable = true;
        //inventoryItems.MaxStackableSize = 99;

        return inventoryItems;
    }

    private IEnumerator SpawnEnemies()
    {
        while (isTimerRunning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Calculate random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnArea.position.x - areaSize.x / 2, spawnArea.position.x + areaSize.x / 2),
            Random.Range(spawnArea.position.y - areaSize.y / 2, spawnArea.position.y + areaSize.y / 2),
            spawnArea.position.z
        );

        // Instantiate the enemy at the random position
        //Instantiate(enemyPrefab, randomPosition, Quaternion.identity, EnemiesObject);

        GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity, EnemiesObject);

        // Get the EnemyAI component from the instantiated enemy
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            // Assign random values to the enemyAI properties
            enemyAI.numberOfHeartsToDrop = Random.Range(0, 2); // 0 or 1
            enemyAI.numberOfCoinsToDrop = Random.Range(0, 6); // 1 to 5
            enemyAI.HeartValueToDrop = Random.Range(1, 6); // 1 to 5
            enemyAI.CoinValueToDrop = Random.Range(1, 6); // 1 to 5
            enemyAI.numberOfMaterialToDrop = Random.Range(0, 2);
            enemyAI.MaterialValueToDrop = 1;
        }

        EnemyExperienceMultiplier[enemyAI.name] = enemyAI.ExpMultiplier;
        spawnedEnemies.Add(enemy);

        
    }

    private void DestroyAllEnemies()
    {

        // Destroy all children of CoinsAndMaterialsDropped
        foreach (Transform child in CoinsAndMaterialsDropped.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                //Destroy(enemy);

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
        spawnedEnemies.Clear(); // Clear the list of enemies
    }

    private void StartTimer()
    {
        countdownTime = 120f; // Reset the countdown time to 2 minutes
        isTimerRunning = true; // Start the timer

        CPUWorldCanvas.gameObject.SetActive(false);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(false);
        TopPanelUI.SetActive(false);
        ButtonsPanelUI.SetActive(false);
        ONHordeUI.SetActive(true);
        Wall.gameObject.SetActive(true);

        StartCoroutine(SpawnEnemies());
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private async void StopTimer()
    {



        countdownTime = 0f;
        isTimerRunning = false;
        CPUWorldCanvas.gameObject.SetActive(true);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(true);
        TopPanelUI.SetActive(true);
        ButtonsPanelUI.SetActive(true);
        ONHordeUI.SetActive(false);
        Wall.gameObject.SetActive(false);

        //getMaterials();
        StopAllCoroutines();
        DestroyAllEnemies();

        
        PickUpSystem.coins = 0;
        PickUpSystem.materials = 0;



        await Task.Delay(1500);
        GameManager.instance.TempEnemyKilled = 0;
        EnemyExperienceMultiplier.Clear();


    }
}
