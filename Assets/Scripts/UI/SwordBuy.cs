using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordBuy : MonoBehaviour
{
    private List<Swords.UI.SwordsItem> toBuy = new List<Swords.UI.SwordsItem>();


    public void SelectItem(Swords.UI.SwordsItem item)
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

    private void SelectNewOrDeselectPrevious(Swords.UI.SwordsItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);


        
            //int index = item.temporaryIndex;
            //Shop.Model.ShopItem sp = GetItemAt(index);
            //ItemPrice = sp.item.Price;
            //total = sp.item.Price;
        





        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }
}
