using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    
    private Inventory inventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    

    private void Awake(){
        itemSlotContainer = transform.Find("itemSlotContainer");

        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

    }

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
        RefreshInventoryItems();

        // this.inventory.RemoveItem(this.inventory.GetItemList()[3]);
        // RefreshInventoryItems();

    }

    private void RefreshInventoryItems(){
        Debug.Log("Doing this!");
        // int x = 0;
        // int y = 0;
        // float itemSlotCellSizeX = 120f;
        // float itemSlotCellSizeY = 70f;
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotTemplate.gameObject.SetActive(false);
        TestInventory iv = TestInventory.Instance;

        int i = 0;
         foreach (Transform child in itemSlotContainer) {
             if (i != 0)
             {
                Destroy(child.gameObject);
                 
             }
             i++;
        }
        foreach(Item item in this.inventory.GetItemList()){
            iv.setItemList(item);
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            // itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSizeX, y * itemSlotCellSizeY);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            

        }
    }
}

