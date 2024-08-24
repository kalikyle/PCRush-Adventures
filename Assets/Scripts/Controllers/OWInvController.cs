using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.EditorScripts;
using Firebase.Firestore;
using Inventory.Model;
using Mono.Cecil.Cil;
using OtherWorld.Model;
using OtherWorld.UI;
using PartsInventory.Model;
using PartsInventory.UI;
using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Inventory.Model.PartsInventorySO;
using static OtherWorld.Model.OWInvSO;

namespace OtherWorld
{
    public class OWInvController : MonoBehaviour
    {
        [SerializeField]
        private OWInvPage inventoryUI;

        [SerializeField]
        public OWInvSO inventoryData;

        public List<OtherWorldItem> initialItems = new List<OtherWorldItem>();
        public event Action<int> OnDescriptionRequested;

        public List<LayerEditor> Layers;
        public CharacterBuilder CharacterBuilder;

        public Image SwordImage;
        public Image ArmorImage;
        public Image HelmetImage;
        public Image ShieldImage;

        public Button EquipBTN;
        public Button SellBTN;
        public TMP_Dropdown DropDcategory;
        public GameObject Notif;


        public Button SwordXBTN;
        public Button ArmorXBTN;
        public Button HelmetXBTN;
        public Button ShieldXBTN;

        //public OWInvItem items;




        public void Start()
        {
            //inventoryData.Initialize();
            GameManager.instance.OnOWItemsToTransferUpdated += UpdateInventory;
            
            //ToggleALLButton();

            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;
            EquipBTN.onClick.AddListener(HandleUseButton);
            SellBTN.onClick.AddListener(HandleSellButton);

            SwordXBTN.onClick.AddListener(() => BackOWEquipment("Sword"));
            ArmorXBTN.onClick.AddListener(() => BackOWEquipment("Armor"));
            HelmetXBTN.onClick.AddListener(() => BackOWEquipment("Helmet"));
            ShieldXBTN.onClick.AddListener(() => BackOWEquipment("Shield"));

        }
        public string openedCategory;
        public void HandleCategory(int val)
        {

            switch (val)
            {
                case 0://all
                    ToggleALLButton();
                    openedCategory = null;
                    break;
                case 1:
                    OpenFiltered("Helmet");
                    openedCategory = "Helmet";
                    break;
                case 2:
                    
                    OpenFiltered("Sword");
                    openedCategory = "Sword";
                    break;
                case 3:
                    
                    OpenFiltered("Armor");
                    openedCategory = "Armor";
                    break;
                case 4:
                    OpenFiltered("Shield");
                    openedCategory = "Shield";
                    break;
                case 5:
                    OpenFiltered("Materials");
                    openedCategory = "Materials";
                    break;
            }
        }

        private void HandleUseButton()
        {
           
            

            int tempIndex = GameManager.instance.OWstempindex;
            Debug.Log("Using item with temporary index: " + tempIndex);

          


            if (ToogleFiltered)
            {
                Debug.Log(ToogleFiltered);
                HandleItemRightActionRequest(tempIndex);
                StartCoroutine(OpenOtherWorldInventoryCategory());

            }
            else
            {
                Debug.Log(ToogleFiltered);
                HandleItemActionRequest(tempIndex);
                StartCoroutine(OpenOtherWorldInventory());

            }


            //if(ToogleFiltered == false) {
               
            //}
            //else
            //{
               
            //}
           
        }

        private void HandleSellButton()
        {
            int tempIndex = GameManager.instance.OWstempindex;
            Debug.Log("Using item with temporary index: " + tempIndex);

            if (ToogleFiltered)
            {
                Debug.Log(ToogleFiltered);
                //HandleItemRightActionRequest(tempIndex);
                HandleFilteredDelete(tempIndex);

                
            
            }
            else
            {
                Debug.Log(ToogleFiltered);
                HandleAllDelete(tempIndex);
                
            }
        }

        public IEnumerator OpenOtherWorldInventory()
        {

            inventoryData.OWinventoryItems.Clear();
            yield return new WaitForSeconds(.2f);
            LoadItemsList();
            yield return new WaitForSeconds(.2f);
            ToggleALLButton();
            

        }
        public IEnumerator OpenOtherWorldInventoryCategory()
        {

            inventoryData.OWinventoryItems.Clear();
            yield return new WaitForSeconds(.2f);
            LoadItemsList();
            yield return new WaitForSeconds(.2f);
            OpenFiltered(openedCategory);


        }

        public void Awake()
        {
            //StartCoroutine(OtherWorldInventory());
        }

        public IEnumerator OtherWorldInventory()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(.5f);

            // Now load initial items
            //LoadComputerItems();
            LoadItemsList();
        }

        public async void LoadItemsList()
        {
            var spriteArray = GameManager.instance.SpriteCollections.Layers;
            int spriteIndex;

            GameManager.instance.SwordDocumentIds.Clear();
            GameManager.instance.ArmorDocumentIds.Clear();
            GameManager.instance.HelmetDocumentIds.Clear();
            GameManager.instance.ShieldDocumentIds.Clear();
            GameManager.instance.MaterialsDocumentIds.Clear();
            GameManager.instance.AllDocumentIds.Clear();

            if (GameManager.instance.UserID != "")
            {
                inventoryData.Initialize();
                //GameManager.instance.pcsoDocumentIds.Clear();
                // Get a reference to the Firestore collection containing the PCSO documents
                CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory");

                // Fetch all documents from the PCSO collection asynchronously
                QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

                // Iterate through the retrieved documents
                foreach (DocumentSnapshot docSnapshot in querySnapshot.Documents)
                {

                    string documentId = docSnapshot.Id;
                    

                    // Deserialize the PCSO data from the Firestore document
                    string itemJson = docSnapshot.GetValue<string>("Items");
                    int intJson = docSnapshot.GetValue<int>("Quantity");

                    if (!string.IsNullOrEmpty(itemJson))
                    {
                        // Create a new PCSO instance
                        OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();

                        // Deserialize the JSON data into the PCSO object
                        JsonUtility.FromJsonOverwrite(itemJson, loadedItem);
                        OtherWorldItem inventoryItem = new OtherWorldItem();


                        inventoryItem.item = loadedItem;
                        inventoryItem.quantity = intJson;

                        if (inventoryItem.item.Category == "Sword")
                        {
                            spriteIndex = loadedItem.SpriteIndex;
                            if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                            {

                                Texture2D texture = spriteArray[8].Textures[spriteIndex];
                                Texture2D text2 = spriteArray[8].GetIcon(texture);
                                // Create a sprite from the texture
                                Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);
                                inventoryItem.item.ItemImage = sprite;

                            }
                                inventoryData.AddItem(inventoryItem);
                                GameManager.instance.SwordDocumentIds.Add(documentId);
                                if (inventoryItem.item.inUse)
                                {
                                    //UseloadComputer(loadedPCSO);
                                    UseItem(inventoryItem);
                                    GameManager.instance.SwordinUse = documentId;
                                }

                           
                        }
                        if (inventoryItem.item.Category == "Armor")
                        {
                            spriteIndex = loadedItem.SpriteIndex;
                            if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                            {

                                Texture2D texture = spriteArray[3].Textures[spriteIndex];
                                Texture2D text2 = spriteArray[3].GetIcon(texture);
                                // Create a sprite from the texture
                                Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);
                                inventoryItem.item.ItemImage = sprite;

                            }
                            inventoryData.AddItem(inventoryItem);
                            GameManager.instance.ArmorDocumentIds.Add(documentId);
                            if (inventoryItem.item.inUse)
                            {
                                //UseloadComputer(loadedPCSO);
                                UseItem(inventoryItem);
                                GameManager.instance.ArmorinUse = documentId;
                            }
                        }
                        if (inventoryItem.item.Category == "Helmet")
                        {
                            spriteIndex = loadedItem.SpriteIndex;
                            if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                            {

                                Texture2D texture = spriteArray[7].Textures[spriteIndex];
                                Texture2D text2 = spriteArray[7].GetIcon(texture);
                                // Create a sprite from the texture
                                Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);
                                inventoryItem.item.ItemImage = sprite;

                            }
                            inventoryData.AddItem(inventoryItem);
                            GameManager.instance.HelmetDocumentIds.Add(documentId);
                            if (inventoryItem.item.inUse)
                            {
                                //UseloadComputer(loadedPCSO);
                                UseItem(inventoryItem);
                                GameManager.instance.HelmetinUse = documentId;
                            }
                        }
                        if (inventoryItem.item.Category == "Shield")
                        {
                            spriteIndex = loadedItem.SpriteIndex;
                            if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                            {

                                Texture2D texture = spriteArray[1].Textures[spriteIndex];
                                Texture2D text2 = spriteArray[1].GetIcon(texture);
                                // Create a sprite from the texture
                                Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);
                                inventoryItem.item.ItemImage = sprite;

                            }
                            inventoryData.AddItem(inventoryItem);
                            GameManager.instance.ShieldDocumentIds.Add(documentId);
                            if (inventoryItem.item.inUse)
                            {
                                //UseloadComputer(loadedPCSO);
                                UseItem(inventoryItem);
                                GameManager.instance.ShieldinUse = documentId;
                            }
                        }

                        if (inventoryItem.item.Category == "Materials")
                        {
                            inventoryItem.item.ItemImage = loadedItem.ItemImage;
                            inventoryData.AddItem(inventoryItem);
                            GameManager.instance.MaterialsDocumentIds.Add(documentId);
                        }

                        GameManager.instance.AllDocumentIds.Add(documentId);
                    }
                }

                // Log a message indicating the successful loading of PCSO items
                Debug.Log("Items items loaded from Firestore.");
            }
        }

       
        public void UpdateInventory(OtherWorldItem updatedItems)
        {
            //inventoryData.inventoryItems.Clear();
            //initialItems.Clear();
            //initialItems.Add(updatedItems);
            
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            inventoryData.AddItem(updatedItems);
            inventoryData.OWSaveItems(updatedItems.item, updatedItems.quantity);
            //initialItems.Clear();

            inventoryData.OWinventoryItems.Clear();
            StartCoroutine(OtherWorldInventory());
        }

        

        public List<OtherWorldItem> InventoryfilteredItems;
        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();


        private void UpdateInventoryUI(Dictionary<int, OtherWorldItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();
            Notif.SetActive(true);
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.inUse);
            }

            
        }

        private int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in inventoryData.OWinventoryItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
        public void ToggleALLButton()
        {
            //infoButton.gameObject.SetActive(false);
            //PartsButton.gameObject.SetActive(true);
            //ComputerButton.gameObject.SetActive(true);
            ToogleFiltered = false;
            Notif.SetActive(false);
            Debug.Log("Toggle: false");
            inventoryUI.ResetSelection();
            inventoryUI.ClearItems();
            inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
            //HandleCategory(0);
            DropDcategory.value = 0;
            //inventoryData.PartsSaveItems();
            OpenInvBTN();
           


        }

        public void OpenFiltered(string category)
        {
            //infoButton.gameObject.SetActive(true);

            Debug.Log("Toggle: true");
            Notif.SetActive(false);

            ShowCategory(category);
            int i = 0;
           
                inventoryUI.Show();

                foreach (var item in InventoryfilteredItems)
                {
                    inventoryUI.UpdateData(i, item.item.ItemImage, item.quantity, item.item.Name, item.item.inUse);
                    i++;
                }
        }

        public void OpenInvBTN()
        {

            // if (inventoryUI.isActiveAndEnabled == false)
            // {
            inventoryUI.Show();

            //DisplayText.text = "Inventory".ToUpper();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.inUse);
            }
           

        }
        private string currentCategory = "";
        public List<OtherWorldItem> itemsToShow;
        public bool ToogleFiltered = true;
        public Dictionary<string, OtherWorldItem> lastUsedItems = new Dictionary<string, OtherWorldItem>();
        private void ShowCategory(string category)
        {
            ToogleFiltered = true;
            currentCategory = category;
            inventoryUI.ResetSelection();
            InventoryfilteredItems.Clear();
            tempToOriginalIndexMapping.Clear();
            inventoryUI.ClearItems();// Clear the existing items in the UI

            itemsToShow = inventoryData.GetItemsByCategory(currentCategory);//all by category

            int originalIndex = 0;
            int tempIndex = 0;

            foreach (var item in itemsToShow)//loop each categorized item
            {
                // Add items to the filtered list and store the mapping

                InventoryfilteredItems.Add(item);//then add to filteredItems

                tempToOriginalIndexMapping[tempIndex] = originalIndex;

                //create a new filteredItems
                inventoryUI.AddShopItem(item.item.ItemImage, item.quantity, item.item.Name);
                originalIndex++;
                tempIndex++;
            }

        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {

                inventoryUI.ResetSelection();
                return;

            }
            OtherWorldItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Category, ItemPerks(item));

            //if (item.Category == "Sword")
            //{
            //    GameManager.instance.clickedInventoryItemID = GameManager.instance.SwordDocumentIds[itemIndex];
            //}
            //else if (item.Category == "Armor")
            //{
            //    GameManager.instance.clickedInventoryItemID = GameManager.instance.ArmorDocumentIds[itemIndex];
            //}
            GameManager.instance.clickedInventoryItemID = GameManager.instance.AllDocumentIds[itemIndex];



        }


        private string ItemPerks(OtherWorldItemSO item)
        {
            string perks = "";

            // Check each perk property and accumulate non-zero values
            if (item.AttackDamage != 0)
            {
                perks += "Attack Damage +" + item.AttackDamage + "\n";
            }
            if (item.Mana != 0)
            {
                perks += "Mana +" + item.Mana + "\n";
            }
            if (item.Armor != 0)
            {
                perks += "Armor +" + item.Armor + "\n";
            }
            if (item.AttackSpeed != 0)
            {
                perks += "Attack Speed +" + item.AttackSpeed + "\n";
            }
            if (item.Health != 0)
            {
                perks += "Health +" + item.Health + "\n";
            }
            if (item.HealthRegen != 0)
            {
                perks += "Health Regen +" + item.HealthRegen + "\n";
            }
            if (item.CriticalHit != 0)
            {
                perks += "Critical Hit +" + item.CriticalHit + "\n";
            }

            if (item.CriticalChance != 0)
            {
                perks += "Critical Chance +" + item.CriticalChance + "\n";
            }

            return perks;
        }


        public void HandleDescriptionRequests(int obj)
        {
            if (obj >= 0 && obj < InventoryfilteredItems.Count)
            {
                OtherWorldItem shopItem = InventoryfilteredItems[obj];
                if (!shopItem.isEmpty)
                {
                    OtherWorldItemSO item = shopItem.item;




                    inventoryUI.UpdateDescription(obj, item.ItemImage, item.Name, item.Category, ItemPerks(item));//update description

                    if (item.Category == "Sword")
                    {
                        GameManager.instance.clickedInventoryItemID = GameManager.instance.SwordDocumentIds[obj];
                    }
                    else if (item.Category == "Armor")
                    {
                        GameManager.instance.clickedInventoryItemID = GameManager.instance.ArmorDocumentIds[obj];
                    }
                    else if (item.Category == "Helmet")
                    {
                        GameManager.instance.clickedInventoryItemID = GameManager.instance.HelmetDocumentIds[obj];
                    }
                    else if (item.Category == "Shield")
                    {
                        GameManager.instance.clickedInventoryItemID = GameManager.instance.ShieldDocumentIds[obj];
                    }
                    //GameManager.instance.clickedInventoryItemID = GameManager.instance.AllDocumentIds[obj];

                }
            }
        }

        public void HandleItemSelection(int tempIndex)
        {
            // Use the mapping to get the original index
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {
                OnDescriptionRequested?.Invoke(originalIndex);
            }
        }

        public int FindLayerIndex(string layerName, string textureName)
        {
            int layerIndex = -1;
            for (int i = 0; i < GameManager.instance.SpriteCollections.Layers.Count; i++)
            {
                if (GameManager.instance.SpriteCollections.Layers[i].Name == layerName)
                {
                    layerIndex = i;
                    break;
                }
            }

            // If the layer with the specified name is found
            if (layerIndex != -1)
            {
                // Iterate through the textures of the specified layer
                var textures = GameManager.instance.SpriteCollections.Layers[layerIndex].Textures;
                for (int j = 0; j < textures.Count; j++)
                {
                    // Check if the name of the current texture matches the input textureName
                    if (textures[j].name == textureName)
                    {
                        // Return the index of the texture within the layer
                        return j;
                    }
                }
            }

            // Return -1 if the layerName or textureName is not found in the SpriteCollection
            return -1;
        }
        public async void BackOWEquipment(string category)
        {

            if (category == "Sword")
            {

                SwordImage.gameObject.SetActive(false);
                SwordImage.sprite = null;
                GameManager.instance.StatsSwordImage.gameObject.SetActive(false);
                GameManager.instance.StatsSwordImage.sprite = null;
                GameManager.instance.EquipmentAttackDamage = 0;
                GameManager.instance.EquipmentAttackSpeed = 0;
                if (!string.IsNullOrEmpty(GameManager.instance.SwordinUse))
                {
                    // Update the PCSO that was previously in use to set inUse = false
                    await UpdateInventoryInUse(GameManager.instance.SwordinUse, false);
                }

                string[] Weaponparts = GameManager.instance.DefaultCharacter["Weapon"].Split('#');
                string WeaponTextureName = Weaponparts[0];

                int WeaponIndex = FindLayerIndex("Weapon", WeaponTextureName);

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Weapon")
                        {
                            SetIndex(layer, WeaponIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }

            }

            else if (category == "Armor")
            {

                ArmorImage.gameObject.SetActive(false);
                ArmorImage.sprite = null;
                GameManager.instance.StatsArmorImage.gameObject.SetActive(false);
                GameManager.instance.StatsArmorImage.sprite = null;
                GameManager.instance.EquipmentArmor = 0;
                GameManager.instance.EquipmentMana = 0;
                if (!string.IsNullOrEmpty(GameManager.instance.ArmorinUse))
                {

                    await UpdateInventoryInUse(GameManager.instance.ArmorinUse, false);
                }

                string[] Armorparts = GameManager.instance.DefaultCharacter["Armor"].Split('#');
                string ArmorTextureName = Armorparts[0];

                int ArmorIndex = FindLayerIndex("Armor", ArmorTextureName);

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Armor")
                        {
                            SetIndex(layer, ArmorIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }

            }

            else if (category == "Helmet")
            { 
                HelmetImage.gameObject.SetActive(false);
                HelmetImage.sprite = null;
                GameManager.instance.StatsHelmetImage.gameObject.SetActive(false);
                GameManager.instance.StatsHelmetImage.sprite = null;
                GameManager.instance.EquipmentHealth = 0;
                GameManager.instance.EquipmentHealthRegen = 0;
                if (!string.IsNullOrEmpty(GameManager.instance.HelmetinUse))
                {

                    await UpdateInventoryInUse(GameManager.instance.HelmetinUse, false);
                }

                string[] Helmetparts = GameManager.instance.DefaultCharacter["Helmet"].Split('#');
                string HelmetTextureName = Helmetparts[0];

                int HelmetIndex = FindLayerIndex("Helmet", HelmetTextureName);

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Helmet")
                        {
                            SetIndex(layer, HelmetIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }
            }

            else if (category == "Shield")
            {

                ShieldImage.gameObject.SetActive(false);
                ShieldImage.sprite = null;
                GameManager.instance.StatsShieldImage.gameObject.SetActive(false);
                GameManager.instance.StatsShieldImage.sprite = null;
                GameManager.instance.EquipmentCriticalHit = 0;
                GameManager.instance.EquipmentCriticalChance = 0;
                if (!string.IsNullOrEmpty(GameManager.instance.ShieldinUse))
                {

                    await UpdateInventoryInUse(GameManager.instance.ShieldinUse, false);
                }

                string[] Shieldparts = GameManager.instance.DefaultCharacter["Shield"].Split('#');
                string ShieldTextureName = Shieldparts[0];

                int ShieldIndex = FindLayerIndex("Shield", ShieldTextureName);

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Shield")
                        {
                            SetIndex(layer, ShieldIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }
            }

            if (ToogleFiltered == false)
            {
                StartCoroutine(OpenOtherWorldInventory());
            }
            else
            {
                StartCoroutine(OpenOtherWorldInventoryCategory());
            }
        }
        public async void HandleItemRightActionRequest(int tempIndex)//for filtered
        {

            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {

                OtherWorldItem inventoryItem = InventoryfilteredItems[originalIndex];
                OtherWorldItemSO inventItem = inventoryItem.item;
                string category = inventoryItem.item.Category;



                if (category == "Sword")
                {
                    EquipSword(inventoryItem);
                    inventoryItem.item.inUse = true;
                    await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
                }
                else if (category == "Armor")
                {
                    EquipArmor(inventoryItem);
                    inventoryItem.item.inUse = true;
                    await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
                }
                else if (category == "Helmet")
                {
                    EquipHelmet(inventoryItem);
                    inventoryItem.item.inUse = true;
                    await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
                }
                else if (category == "Shield")
                {
                    EquipShield(inventoryItem);
                    inventoryItem.item.inUse = true;
                    await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
                }

                else if (category == "Materials")
                {

                    Debug.LogError("Cant Use this Item");
                }




            }
        }

        private void EquipArmor(OtherWorldItem inventoryItem)
        {
            ArmorImage.gameObject.SetActive(true);
            ArmorImage.sprite = inventoryItem.item.ItemImage;
            GameManager.instance.StatsArmorImage.gameObject.SetActive(true);
            GameManager.instance.StatsArmorImage.sprite = inventoryItem.item.ItemImage;

            if (inventoryItem.item.Armor != 0)
            {

                GameManager.instance.EquipmentArmor = (int)inventoryItem.item.Armor;
            }

            if (inventoryItem.item.Mana != 0)
            {

                GameManager.instance.EquipmentMana = (int)inventoryItem.item.Mana;

            }

            foreach (var layer in Layers)
            {

                if (layer.Controls)
                {
                    layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                    if (layer.Name == "Armor")
                    {
                        SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                        Rebuild(layer);
                    }

                }

            }

           
        }

        private void EquipSword(OtherWorldItem inventoryItem)
        {
            SwordImage.gameObject.SetActive(true);
            SwordImage.sprite = inventoryItem.item.ItemImage;
            GameManager.instance.StatsSwordImage.gameObject.SetActive(true);
            GameManager.instance.StatsSwordImage.sprite = inventoryItem.item.ItemImage;

            if (inventoryItem.item.AttackDamage != 0)
            {
               
                GameManager.instance.EquipmentAttackDamage = (int)inventoryItem.item.AttackDamage;
               
            }

            if (inventoryItem.item.AttackSpeed != 0)
            {

                GameManager.instance.EquipmentAttackSpeed = (int)inventoryItem.item.AttackSpeed;

            }


            foreach (var layer in Layers)
            {

                if (layer.Controls)
                {
                    layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                    if (layer.Name == "Weapon")
                    {
                        SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                        Rebuild(layer);
                    }

                }

            }


            
        }
       
        private void EquipHelmet(OtherWorldItem inventoryItem)
        {
           
            HelmetImage.gameObject.SetActive(true);
            HelmetImage.sprite = inventoryItem.item.ItemImage;
            GameManager.instance.StatsHelmetImage.gameObject.SetActive(true);
            GameManager.instance.StatsHelmetImage.sprite = inventoryItem.item.ItemImage;

            if (inventoryItem.item.Health != 0)
            {

                GameManager.instance.EquipmentHealth = (int)inventoryItem.item.Health;

            }

            if (inventoryItem.item.HealthRegen != 0)
            {

                GameManager.instance.EquipmentHealth = (int)inventoryItem.item.HealthRegen;

            }


            foreach (var layer in Layers)
            {

                if (layer.Controls)
                {
                    layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                    if (layer.Name == "Helmet")
                    {
                       
                        SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                        Rebuild(layer);
                    }

                }

            }
        }
        
        private void EquipShield(OtherWorldItem inventoryItem)
        {
            ShieldImage.gameObject.SetActive(true);
            ShieldImage.sprite = inventoryItem.item.ItemImage;
            GameManager.instance.StatsShieldImage.gameObject.SetActive(true);
            GameManager.instance.StatsShieldImage.sprite = inventoryItem.item.ItemImage;

            if (inventoryItem.item.CriticalHit != 0)
            {

                GameManager.instance.EquipmentCriticalHit = (int)inventoryItem.item.CriticalHit;

            }

            if (inventoryItem.item.CriticalChance != 0)
            {

                GameManager.instance.EquipmentCriticalChance = (int)inventoryItem.item.CriticalChance;

            }
            //foreach (var layer in Layers)
            //{
            //    if (layer.Controls)
            //    {
            //        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

            //        if (layer.Name == "Shield")
            //        {
            //            SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
            //            Rebuild(layer);
            //        }
            //    }

            //}

            foreach (var layer in Layers)
            {
                layer.Content = GameManager.instance.SpriteCollections.Layers.SingleOrDefault(i => i.Name == layer.Name);
                if (layer.Content == null)
                {
                    Debug.LogWarning($"Layer content not found for {layer.Name}");
                }
            }

            foreach (var layer in Layers)
            {
                if (layer.Controls)
                {
                    if (layer.Name == "Shield")
                    {
                        SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                        Rebuild(layer);
                    }
                }
            }





        }
        //for all 

        public void UseItem(OtherWorldItem inventoryItem)//for all
        {

            OtherWorldItemSO inventItem = inventoryItem.item;
            string category = inventoryItem.item.Category;


            if (category == "Sword")
            {

                EquipSword(inventoryItem);
               
            }
            else if (category == "Armor")
            {

                EquipArmor(inventoryItem);

            }

            else if (category == "Helmet")
            {

                EquipHelmet(inventoryItem);

            }
            else if (category == "Shield")
            {

                EquipShield(inventoryItem);

            }

        }

        public async void HandleItemActionRequest(int itemIndex)//for all
        {

            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            OtherWorldItemSO inventItem = inventoryItem.item;
            string category = inventoryItem.item.Category;


            if (category == "Sword")
            {

                EquipSword(inventoryItem);

                inventoryItem.item.inUse = true;
                await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
            }
            else if (category == "Armor")
            {


                EquipArmor(inventoryItem);

                inventoryItem.item.inUse = true;
                await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
            }
            else if (category == "Helmet")
            {

                EquipHelmet(inventoryItem);
                inventoryItem.item.inUse = true;
                await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
            }
            else if (category == "Shield")
            {
                EquipShield(inventoryItem);

                inventoryItem.item.inUse = true;
                await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
            }
            else if (category == "Materials")
            {

                Debug.LogError("Cant Use this Item");
            }

        }

        public async void HandleFilteredDelete(int itemIndex)
        {
            if (tempToOriginalIndexMapping.TryGetValue(itemIndex, out int originalIndex))
            {

                OtherWorldItem inventoryItem = InventoryfilteredItems[originalIndex];
                OtherWorldItemSO inventItem = inventoryItem.item;
                string category = inventoryItem.item.Category;

                if (category == "Sword")
                {
                   
                    await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
                }
                else if (category == "Armor")
                {
                    
                    await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);

                }
                else if (category == "Helmet")
                {
                   
                    await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
                }
                else if (category == "Shield")
                {
                    
                    await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
                }
                else if (category == "Materials")
                {
                   
                    Debug.LogError("Cant Delete this Item");
                }

            }
           
        }

        public async void HandleAllDelete(int itemIndex)//for all
        {

            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            OtherWorldItemSO inventItem = inventoryItem.item;
            string category = inventoryItem.item.Category;

            if (category == "Sword")
            {
                
                await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
            }
            else if (category == "Armor")
            {
               
                await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);

            }
            else if (category == "Helmet")
            {
               
                await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
            }
            else if (category == "Shield")
            {
                
                await DeleteInventoryItem(GameManager.instance.clickedInventoryItemID, category);
            }
            else if (category == "Materials")
            {

                Debug.LogError("Cant Delete this Item");
            }
           
        }

        public async Task DeleteInventoryItem(string documentId, string category)
        {
            try
            {
                // Check if the item is currently in use
                if (GameManager.instance.SwordinUse == documentId ||
                    GameManager.instance.ArmorinUse == documentId ||
                    GameManager.instance.HelmetinUse == documentId ||
                    GameManager.instance.ShieldinUse == documentId)
                {
                    // Revert the equipment back to its default state
                    BackOWEquipment(category);
                }
                else
                {
                   
                    if (ToogleFiltered) {
                        StartCoroutine(OpenOtherWorldInventoryCategory());
                    }
                    else
                    {
                        StartCoroutine(OpenOtherWorldInventory());
                    }
                   
                }

                // Get a reference to the Firestore document to be deleted
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory")
                    .Document(documentId);

                // Delete the Firestore document
                await docRef.DeleteAsync();

                Debug.Log("Inventory item deleted successfully.");

                // Clear the corresponding inUse field in GameManager
                if (GameManager.instance.SwordinUse == documentId)
                {
                    GameManager.instance.SwordinUse = "";
                    GameManager.instance.SwordDocumentIds.Remove(documentId); // Remove from Sword list
                }
                else if (GameManager.instance.ArmorinUse == documentId)
                {
                    GameManager.instance.ArmorinUse = "";
                    GameManager.instance.ArmorDocumentIds.Remove(documentId); // Remove from Armor list
                }
                else if (GameManager.instance.HelmetinUse == documentId)
                {
                    GameManager.instance.HelmetinUse = "";
                    GameManager.instance.HelmetDocumentIds.Remove(documentId); // Remove from Helmet list
                }
                else if (GameManager.instance.ShieldinUse == documentId)
                {
                    GameManager.instance.ShieldinUse = "";
                    GameManager.instance.ShieldDocumentIds.Remove(documentId); // Remove from Shield list
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error deleting inventory item: " + ex.Message);
            }
        }

        private void SetIndex(LayerEditor layer, int index)
        {
            if (layer.CanBeEmpty) index--;

            layer.SetIndex(index);

            if (layer.Name == "Body")
            {
                Layers.Single(i => i.Name == "Head").SetIndex(index);
            }

            //Rebuild(layer);
        }

        private void Rebuild(LayerEditor layer)
        {
            var layers = Layers.ToDictionary(i => i.Name, i => i.SpriteData);
           
            CharacterBuilder.Head = layers["Head"];
            CharacterBuilder.Body = layers["Body"];
            CharacterBuilder.Hair = layers["Hair"];
            CharacterBuilder.Armor = layers["Armor"];
            CharacterBuilder.Helmet = layers["Helmet"];
            CharacterBuilder.Weapon = layers["Weapon"];
            CharacterBuilder.Shield = layers["Shield"];
            CharacterBuilder.Cape = layers["Cape"];
            CharacterBuilder.Back = layers["Back"];
            CharacterBuilder.Rebuilds(layer?.Name);

        }
        public async Task UpdateInventoryItem(string documentId, OtherWorldItemSO UpdatedItem)
        {
            try
            {
                // Convert the updated PCSO object to JSON
                string updatedPCSOJson = JsonUtility.ToJson(UpdatedItem);
                //pcsothatinUse = documentId;
                if(UpdatedItem.Category == "Sword")
                {
                    if (!string.IsNullOrEmpty(GameManager.instance.SwordinUse) && GameManager.instance.SwordinUse != documentId)
                    {
                        // Update the PCSO that was previously in use to set inUse = false
                        await UpdateInventoryInUse(GameManager.instance.SwordinUse, false);
                    }

                }else if (UpdatedItem.Category == "Armor")
                {
                    if (!string.IsNullOrEmpty(GameManager.instance.ArmorinUse) && GameManager.instance.ArmorinUse != documentId)
                    {
                        // Update the PCSO that was previously in use to set inUse = false
                        await UpdateInventoryInUse(GameManager.instance.ArmorinUse, false);
                    }
                }
                else if (UpdatedItem.Category == "Helmet")
                {
                    if (!string.IsNullOrEmpty(GameManager.instance.HelmetinUse) && GameManager.instance.HelmetinUse != documentId)
                    {
                        // Update the PCSO that was previously in use to set inUse = false
                        await UpdateInventoryInUse(GameManager.instance.HelmetinUse, false);
                    }
                }
                else if (UpdatedItem.Category == "Shield")
                {
                    if (!string.IsNullOrEmpty(GameManager.instance.ShieldinUse) && GameManager.instance.ShieldinUse != documentId)
                    {
                        // Update the PCSO that was previously in use to set inUse = false
                        await UpdateInventoryInUse(GameManager.instance.ShieldinUse, false);
                    }
                }else if (UpdatedItem.Category == "Materials")
                {

                    return;
                }




                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory")
                    .Document(documentId);

                // Create a dictionary to store the updated PCSO data
                Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "Items", updatedPCSOJson }
        };

                // Update the Firestore document with the new data
                await docRef.UpdateAsync(updateData);

                if(UpdatedItem.Category == "Sword")
                {
                    GameManager.instance.SwordinUse = documentId;
                }else if (UpdatedItem.Category == "Armor")
                {
                    GameManager.instance.ArmorinUse = documentId;
                }
                else if (UpdatedItem.Category == "Helmet")
                {
                    GameManager.instance.HelmetinUse = documentId;
                }
                else if (UpdatedItem.Category == "Shield")
                {
                    GameManager.instance.ShieldinUse = documentId;
                }else if (UpdatedItem.Category == "Materials")
                {
                    return;
                }

                Debug.Log("PCSO document updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO document: " + ex.Message);
            }
        }

        private async Task UpdateInventoryInUse(string ItemThatinUse, bool inUseStatus)
        {
            try
            {
                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory")
                    .Document(ItemThatinUse);

                // Fetch the document snapshot
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Deserialize the PCSO data from the Firestore document
                    string pcsoJson = snapshot.GetValue<string>("Items");

                    if (!string.IsNullOrEmpty(pcsoJson))
                    {
                        // Deserialize the JSON data into a PCSO object
                        //PCSO loadedPCSO = JsonUtility.FromJson<PCSO>(pcsoJson);

                        OtherWorldItemSO loadedPCSO = ScriptableObject.CreateInstance<OtherWorldItemSO>();

                        // Deserialize the JSON data into the PCSO object
                        JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);

                        // Update the inUse status
                        loadedPCSO.inUse = inUseStatus;

                        // Convert the updated PCSO object back to JSON
                        string updatedPCSOJson = JsonUtility.ToJson(loadedPCSO);

                        // Create a dictionary to update the inUse status
                        Dictionary<string, object> updateData = new Dictionary<string, object>
                {
                    { "Items", updatedPCSOJson }
                };

                        // Update the Firestore document with the new inUse status
                        await docRef.UpdateAsync(updateData);

                        Debug.Log("PCSO inUse status updated successfully.");


                    }
                    else
                    {
                        Debug.LogWarning("PCSO JSON data is empty or invalid.");
                    }
                }
                else
                {
                    Debug.LogWarning("PCSO document does not exist.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO inUse status: " + ex.Message);
            }
        }



    }

   
}
