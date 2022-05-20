using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler 
{
    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            Debug.Log("OnDrop");
            UI_ItemDrag uI_ItemDrag = UI_ItemDrag.Instance;
            
            Item item = uI_ItemDrag.GetItem(); 
            Debug.Log(item.itemType);

            Debug.Log(GetComponent<RectTransform>().anchoredPosition);

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        }
    }
}
