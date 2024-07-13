using Exchanger.Model.CPUWorld;
using Firebase.Firestore;
using OtherWorld.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;
using static UnityEditor.Progress;

public class CPUWorldExchangerBuy : MonoBehaviour
{
    private List<CPUWorldExchangerItem> toBuy = new List<CPUWorldExchangerItem>();
    public CPUWorldItemSO so;
    public OWInvSO data;
    public Button buyButton;
    private int value = 1;

    public TMP_Text displayText;
    public TMP_Text priceText;
    public Button incrementButton;
    public Button decrementButton;


    public double ItemPrice = 0;
    private int minValue = 1; // Minimum value (decrement limit)
    private int maxValue = 99;// Maximum value (increment limit)
    double total;


   


    public void Start()
    {
        incrementButton.onClick.AddListener(IncrementValue);
        decrementButton.onClick.AddListener(DecrementValue);
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    private void IncrementValue()
    {
        if (value < maxValue)
        {
            value++;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }

    private void DecrementValue()
    {
        if (value > minValue)
        {
            value--;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }

    private void UpdatePriceDisplay()
    {
        total = ItemPrice * value;
        if (priceText != null)
        {
            priceText.text = total.ToString();

        }

    }
    public void ResetQuantity()
    {
        value = 1;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = value.ToString();

        }

    }

    public void SelectItem(CPUWorldExchangerItem item)
    {

        if (!toBuy.Contains(item))// selectedItem = 0 item + 1
        {

            SelectNewOrDeselectPrevious(item);


        }
        else
        {

            toBuy.Clear();
            SelectNewOrDeselectPrevious(item);


        }

    }

    private void SelectNewOrDeselectPrevious(CPUWorldExchangerItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);



        int index = item.temporaryIndex;
        CPUs sp = GetItemAt(index);
        ItemPrice = sp.item.MaterialsAmountNeed;
        total = sp.item.MaterialsAmountNeed;






        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public CPUs GetItemAt(int obj)
    {
        return so.Procies[obj];
    }

    private async Task<int> GetMaterialQuantity(string materialName)
    {
        CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
            .Collection(GameManager.instance.UserCollection)
            .Document(GameManager.instance.UserID)
            .Collection("OtherWorldInventory");

        QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

        foreach (var docSnapshot in querySnapshot.Documents)
        {
            string existingItemJson = docSnapshot.GetValue<string>("Items");
            OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();
            JsonUtility.FromJsonOverwrite(existingItemJson, loadedItem);
            OtherWorldItem existingItem = new OtherWorldItem
            {
                item = loadedItem
            };

            if (existingItem.item.Category == "Materials" && existingItem.item.Name == materialName)
            {
                return docSnapshot.GetValue<int>("Quantity");
            }
        }

        return 0; // If material is not found, return 0
    }

    private async void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            CPUWorldExchangerItem CPUItem = toBuy[0];
            if (CPUItem != null)
            {
                //you can place the condition for currency here
                int index = CPUItem.temporaryIndex;
                CPUs sp = GetItemAt(index);

                string MaterialName = sp.item.MaterialsNeed.name;
                double requiredamount = total;

                int currentQuantity = await GetMaterialQuantity(MaterialName);

                if (currentQuantity >= requiredamount)
                {
                    CPUBuys(CPUItem);

                    // Deduct the used quantity
                    OtherWorldItemSO newItemSO = ScriptableObject.CreateInstance<OtherWorldItemSO>();
                    newItemSO.Name = MaterialName;
                    newItemSO.Category = "Materials";
                    int quantityToDeduct = -(int)requiredamount;


                    await GameManager.instance.SaveOWItems(newItemSO, quantityToDeduct);

                    Debug.Log("The item has been purchase");

                    GameManager.instance.LoadOtherWorldInventory();


                    CPUItem.DeSelect();

                    value = 1;
                    displayText.text = value.ToString();
                    priceText.text = sp.item.MaterialsAmountNeed.ToString();

                }
                else
                {
                    Debug.LogError("You dont have enough of this Item!");
                }


            }
            else
            {
                Debug.LogWarning("Null shop item in toBuy list.");
            }
        }
        else
        {
            Debug.Log("No item to purchase. The toBuy list is empty.");
        }
        toBuy.Clear();

    }
    private Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
    //public void HandlePurchase(Armors.UI.CPUWorldExchangerItem shopItem)//need fix
    //{
    //    List<Armors.Model.Armors> shopItems = so.Sword;
    //    int tempIndexs;
    //    int originalIndex;
    //    int tempIndex;

    //    tempToOriginalIndexMapping.Clear();
    //    tempIndexs = 0;
    //    originalIndex = 0;
    //    Debug.Log("Toggle: False");
    //    //Debug.Log(shopItems.Count);
    //    foreach (var item in shopItems)
    //    {

    //        tempToOriginalIndexMapping[tempIndexs] = originalIndex;
    //        tempIndexs++;
    //        originalIndex++;

    //    }

    //    tempIndex = shopItem.temporaryIndex;
    //    if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
    //    {

    //        Armors.Model.Armors shpItem = GetItemAt(originalIndexs);

    //        SwordBuys(shopItem);





    //    }


    public PartsCollect Parts; 
    //}
    public void CPUBuys(CPUWorldExchangerItem CPUItem)
    {
        if (CPUItem != null)
        {
            Debug.Log("Buyedd");

           Parts = ConvertCPUtoCollect(CPUItem);

            if (Parts == null)
            {

                Debug.LogWarning("Null inventory item returned from conversion.");
            }
            else
            {

                Bounds parentBounds = GameManager.instance.GetParentBounds();

                // Calculate a random position within the bounds of the parent object
                Vector3 randomPosition = new Vector3(
                    UnityEngine.Random.Range(parentBounds.min.x, parentBounds.max.x),
                    UnityEngine.Random.Range(parentBounds.min.y, parentBounds.max.y),
                    0f);



                // Instantiate or update the GameObject based on the retrieved data
                var obj = Instantiate(Parts, randomPosition, Quaternion.identity, GameManager.instance.partsToCollect);

                obj.transform.localScale = new Vector3(0.71716f, 0.71716f, 0.71716f);

                GameManager.instance.PartsToCollect.Add(obj.gameObject);
                GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);
                GameManager.instance.ShowPopUp(Parts, true);

                Debug.Log("GameObject instantiated/updated: " + obj.name);
                Debug.Log("Item Delivered ");
            }

        }
        else
        {
            Debug.LogWarning("Null shop item passed to HandlePurchase.");
        }

    }

    public PartsCollect ConvertCPUtoCollect(CPUWorldExchangerItem shopItem)
    {

        List<CPUs> shopItems = so.Procies;
        int tempIndexs;
        int originalIndex;
        int tempIndex;


        tempToOriginalIndexMapping.Clear();
        tempIndexs = 0;
        originalIndex = 0;
        Debug.Log("Toggle: False");
        //Debug.Log(shopItems.Count);
        foreach (var item in shopItems)
        {

            tempToOriginalIndexMapping[tempIndexs] = originalIndex;
            tempIndexs++;
            originalIndex++;

        }

        tempIndex = shopItem.temporaryIndex;
        if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
        {
            // Use the original index to retrieve the ShopItem
            CPUs shpItem = GetItemAt(originalIndexs);

            if (!shpItem.isEmpty)
            {
                Parts.parts = shpItem.item.Parts;
               

            }
            else
            {
                Debug.Log("ShopItem is empty");
            }
        }
        else
        {
            Debug.LogError("Mapping not found for temporary index: " + tempIndex);
        }

        Parts.Quantity = value;

        return Parts;

    }

    //public OtherWorldItemSO ConvertSword(CPUs shpItem)
    //{
    //    //OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
    //    //inventoryItems.name = shpItem.item.Name;
    //    //var spriteArray = GameManager.instance.SpriteCollections.Layers;
    //    //int spriteIndex;

    //    //inventoryItems.Name = shpItem.item.Name;
    //    //inventoryItems.Attack = shpItem.item.attack;
    //    //spriteIndex = shpItem.item.SpriteIndex;

    //    //if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
    //    //{

    //    //    Texture2D texture = spriteArray[8].Textures[spriteIndex];
    //    //    Texture2D text2 = spriteArray[8].GetIcon(texture);
    //    //    // Create a sprite from the texture
    //    //    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

    //    //    inventoryItems.ItemImage = sprite;
    //    //}

    //    //inventoryItems.Category = "Sword";

    //    ////if want not stackable
    //    //inventoryItems.IsStackable = false;
    //    //inventoryItems.MaxStackableSize = 1;
    //    //inventoryItems.SpriteIndex = shpItem.item.SpriteIndex;
    //    //// if stackable
    //    ////inventoryItems.IsStackable = true;
    //    ////inventoryItems.MaxStackableSize = 99;

    //    //return inventoryItems;
    //}
}
