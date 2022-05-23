using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
   
    public static Inventory Instance {
        get {
            if (instance == null) {
                instance = new Inventory ();
            }
            return instance;
        }
    }

    private List<Item> itemList  = new List<Item>();

    // public Inventory(){
    //     itemList = new List<Item>();

    //     Item item1 = new Item {id =  0, itemType = Item.ItemType.Common , amount = 1 };
    //     Item item2 = new Item {id =  1, itemType = Item.ItemType.Common , amount = 1 };
    //     Item item3 = new Item {id =  2, itemType =Item.ItemType.Rare , amount = 1 };
    //     Item item4 = new Item {id =  3, itemType =Item.ItemType.Epic , amount = 1 };
    //     Item item5 = new Item {id =  4, itemType =Item.ItemType.Epic , amount = 1 };
    //     // AddItem(item1);
    //     AddItem(item2);
    //     AddItem(item3);
    //     AddItem(item4);
    //     // AddItem(item5);
    //     // RemoveItem(item5);
    //     // AddItem(item5);
    //     // AddItem(item5);


        
    // }

    public void AddItem(Item item)
    {
        if(itemList.Count <= 5){

            itemList.Add(item);
        }
        else{
            Debug.Log("inventory full");
        }
        return;

    }
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);

    }

    public List<Item> GetItemList(){
        return itemList;
    }




































    // int i = 1;
    // public Item[] inventory = new Item[6];
    // public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    // public bool Add(Item item){

    //     Debug.Log(item.id);
    //     // find first item open slot in the inventory
    //     for(int i = 0; i < inventory.Length; i++){
    //         if (inventory[i] == null)
    //         {
    //             inventory[i] = item;
    //             inventorySlots[i].item = item;
    //             Debug.Log(item + " was added");
    //             return true;
    //         }
    //     }
    //     return false;

    // }

    // public void UpdateSlotUI(){
    //     for(int i = 0 ; i< inventorySlots.Count; i++){
    //         inventorySlots[i].UpdateSlot();

    //     }
    // }

    // public void addItem(Item item){
    //     item.id = i;
    //     i++;
    //     Debug.Log("clk");
    //     bool hasAdded = Add(item);
    //     if(hasAdded){
    //         UpdateSlotUI();
    //     }

    // }

    // public void clearInventory(){
    //     Array.Clear(inventory, 0, inventory.Length);
    //     Debug.Log("clear Inventory " + inventory);
    //     for(int i = 0 ; i< inventorySlots.Count; i++){
    //         inventorySlots[i].UpdateClearSlot();

    //     }

    // }

}
