using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrip : MonoBehaviour
{

    private Inventory inventory;

    private Transform fragment;

    private void Awake(){
        fragment = transform.Find("fragment");
        // Debug.Log(fragment);

    }

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems(){
        // int x = 0;
        // int y = 0;
        // float itemSlotCellSizeX = 120f;
        // float itemSlotCellSizeY = 70f;
        // itemSlotContainer = transform.Find("itemSlotContainer");
        // itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        // TestInventory iv = TestInventory.Instance;
        // foreach(Item item in this.inventory.GetItemList()){
        //     iv.setItemList(item);
        //     RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        //     itemSlotRectTransform.gameObject.SetActive(true);

        //     // itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSizeX, y * itemSlotCellSizeY);
        //     Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        //     image.sprite = item.GetSprite();
            

        // }
    }
}
