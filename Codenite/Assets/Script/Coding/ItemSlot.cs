using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler 
{
    private CanvasGroup canvasGroup;
    [SerializeField] private UI_Inventory ui_inventory;


    private void Awake(){
        canvasGroup = transform.Find("Background").GetComponent<CanvasGroup>();
    }
    public Item slotItem;
    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            Debug.Log("OnDrop");
            UI_ItemDrag uI_ItemDrag = UI_ItemDrag.Instance;
            
            Item item = uI_ItemDrag.GetItem(); 

            ui_inventory.getInventory().RemoveItem(item);

            ui_inventory.SetInventory(ui_inventory.getInventory());
            slotItem = item;
            Debug.Log(item.value);

            canvasGroup.alpha = .75f;
            Answer_manager answer_Manager = Answer_manager.Instance;
            answer_Manager.addAnswer(this);

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;

            Image image = transform.Find("Background").GetComponent<Image>();
            image.sprite = item.GetSprite();

        }
    }
}
