using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler 
{
    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            Debug.Log("OnDrop");
            UI_ItemDrag uI_ItemDrag = UI_ItemDrag.Instance;
            
            Item item = uI_ItemDrag.GetItem(); 
            Debug.Log(item.id);

            Answer_manager answer_Manager = Answer_manager.Instance;
            answer_Manager.addAnswer(item);
            // Debug.Log("ans list : " + answer_Manager.getAnser().Count);
            // eventData.pointerDrag.GetComponent<RectTransform>().SetAsLastSibling();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;

            Image image = transform.Find("Background").GetComponent<Image>();
            image.sprite = item.GetSprite();

        }
    }
}
