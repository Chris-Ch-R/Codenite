using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler, IDragHandler, IEventSystemHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    // public Vector3 StartPosition;
    [SerializeField] private Item item;
    [SerializeField] private UI_Inventory ui_inventory;

    private Vector3 startPosition;

    int ansBfCount;

    Inventory inventory;

    private void Awake(){
        if( rectTransform == null ){
            // rectTransform = transform.parent.GetComponent<RectTransform>();
            rectTransform = transform.GetComponent<RectTransform>();
            startPosition = rectTransform.position;
            // Debug.Log(rectTransform.position);
        }
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvas == null) {
            Transform testCanvasTransform = transform.parent;
            while( testCanvasTransform != null){
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if(canvas != null){
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }

        }
        
    }
    public void OnBeginDrag(PointerEventData eventData){
        int itemIndex =  transform.parent.GetSiblingIndex() - 1;
        
        List<Item> itemList = ui_inventory.getInventory().GetItemList();
        Debug.Log("itemList : " + itemList.Count);
        item = itemList[itemIndex];
        
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        rectTransform.SetAsLastSibling();
        UI_ItemDrag uI_ItemDrag = UI_ItemDrag.Instance;
        uI_ItemDrag.SetItem(item);

        Answer_manager answer_Manager = Answer_manager.Instance;
        ansBfCount = answer_Manager.getAnser().Count;
    }
    public void OnDrag(PointerEventData eventData){
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData){
        
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //TODO : start position
        

        if (Answer_manager.Instance.getAnser().Count == ansBfCount )
        {
            rectTransform.anchoredPosition = startPosition;
            
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        
    }

    public void OnInitializePotentialDrag(PointerEventData eventData){
        // startPosition = rectTransform.anchoredPosition;
        // Debug.Log(startPosition);
    }
    
}
