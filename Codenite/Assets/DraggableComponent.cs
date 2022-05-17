using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DraggableComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IEventSystemHandler
// , IDropHandler


{

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool FollowCursor {get; set;} = true;
    public Vector3 StartPosition;
    public bool CanDrag {get; set;} = true;

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    // private CanvasGroup canvasGroup;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        // canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData pointerEventData){

        if (!CanDrag)
        {
            return;
        }

        OnDragHandler?.Invoke(pointerEventData);

        if(FollowCursor){

            rectTransform.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;
        }

    }
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        // Debug.Log("OnBeginDrag");
        // canvasGroup.alpha = .6f;
        // canvasGroup.blocksRaycasts = false;
        if (!CanDrag)
        {
            return;
        }

        OnBeginDragHandler?.Invoke(pointerEventData);
    }
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Debug.Log("OnEndDrag");
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        // canvasGroup.alpha = 1f;
        // canvasGroup.blocksRaycasts = true;

        DropArea dropArea = null;
        foreach (var result in results)
        {
            dropArea = result.gameObject.GetComponent<DropArea>();
            if (dropArea != null)
            {
                break;
            }
        }

        if(dropArea != null){
            if(dropArea.Accepts(this))
            {
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(pointerEventData , true);
                return;
            }
        }
        rectTransform.anchoredPosition = StartPosition;
        OnEndDragHandler?.Invoke(pointerEventData , false);

    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnInitializePotentialDrag(PointerEventData eventData){
        StartPosition = rectTransform.anchoredPosition;
    }

    // public void OnDrop(PointerEventData pointerEventData){
    //     // rectTransform.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;
    //     Debug.Log("OnDrop");

    // }
}
