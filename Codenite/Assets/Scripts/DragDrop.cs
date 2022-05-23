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

    private Vector3 startPosition;

    int ansBfCount;

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

        List<Item> itemList = TestInventory.Instance.GetItemList();
        
        if(itemList.Count - 1 >= 0){
            item = itemList[itemList.Count - 1];

        }

        

        

        

        
    }
    public void OnBeginDrag(PointerEventData eventData){
        
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
