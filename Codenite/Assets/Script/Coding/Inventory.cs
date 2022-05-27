
using System;
using System.Linq;
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
    private List<Item> tmpItemList  = new List<Item>();
    private List<Item> tmp2 = new List<Item>();


    public void AddItem(Item item)
    {
        if(itemList.Count <= 5){

            itemList.Add(item);
            tmpItemList.Add(item);
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

    public List<Item> getTmpItemList(){
        return tmpItemList;

    }

    public List<Item> GetItemList(){
        return itemList;
    }
    public Inventory resetItemList(){
        this.itemList.Clear();
        itemList = tmpItemList.ToList();
        return this;
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
