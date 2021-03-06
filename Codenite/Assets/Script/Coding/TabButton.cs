using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image backGround;

    void Start() {

        backGround = GetComponent<Image>();
        tabGroup.Subscribe(this);    
    }

    public void OnPointerClick(PointerEventData eventData){
        tabGroup.OnTabSelected(this);
        
    }
    public void OnPointerEnter(PointerEventData eventData){
        tabGroup.OnTabEnter(this);
        
    }
    public void OnPointerExit(PointerEventData eventData){
        tabGroup.OnTabExit(this);

    }
}
