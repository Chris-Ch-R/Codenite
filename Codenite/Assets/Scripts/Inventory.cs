using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] inventory = new Item[6];
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public bool Add(Item item){


        // find first item open slot in the inventory
        for(int i = 0; i < inventory.Length; i++){
            if (inventory[i] == null)
            {
                inventory[i] = item;
                inventorySlots[i].item = item;
                Debug.Log(item + " was added");
                return true;
            }
        }
        Debug.Log("out ");
        return false;

    }

    public void UpdateSlotUI(){
        for(int i = 0 ; i< inventorySlots.Count; i++){
            inventorySlots[i].UpdateSlot();

        }
    }

    public void addItem(Item item){
        bool hasAdded = Add(item);
        if(hasAdded){
            UpdateSlotUI();
        }

    }

}
