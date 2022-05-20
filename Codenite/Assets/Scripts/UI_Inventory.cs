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
    }

    private void RefreshInventoryItems(){
        int x = 0;
        int y = 0;
        float itemSlotCellSizeX = 100f;
        float itemSlotCellSizeY = 40f;
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        TestInventory iv = TestInventory.Instance;
        foreach(Item item in this.inventory.GetItemList()){
            iv.setItemList(item);
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSizeX, y * itemSlotCellSizeY);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            Debug.Log("item : " + item);
            image.sprite = item.GetSprite();
            x++;
            if(x > 2){
                x = 0;
                y--;
            }

        }
    }
}

