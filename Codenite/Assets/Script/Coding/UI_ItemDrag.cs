using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemDrag 
{
    private Item item;

    private static UI_ItemDrag instance;
   
    public static UI_ItemDrag Instance {
        get {
            if (instance == null) {
                instance = new UI_ItemDrag ();
            }
            return instance;
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;

    }

    public Item GetItem(){
        return item;
    }
    
}
