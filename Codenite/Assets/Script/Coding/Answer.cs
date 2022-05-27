using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private ModalWindowPanel modalWindowPanel;
    private Inventory inventory ;


    private Transform ui_coding;

    private Answer_manager answer_Manager;

    private string[] ans1 = {"Count = 1" , "Count <= 10" , "Count ++"};
    
    public void checkAnswer(){
        answer_Manager = Answer_manager.Instance;

        Transform uiCoding = transform.parent.Find("UI_Coding");

        for(int i = 1; i < uiCoding.childCount; i++)
        {
            ItemSlot itSlot = uiCoding.GetChild(i).GetComponent<ItemSlot>();
            Debug.Log(itSlot.slotItem.value + "==" + ans1[i-1]);
            if(!itSlot.slotItem.value.Equals( ans1[i-1] ) ){
                answer_Manager.setAnsInCorrect();
                modalWindowPanel.ShowErrorGui(WindowAssets.Instance.getMissionError());
                Debug.Log("Ans : " + answer_Manager.getAnsCheck());
                return;
            }
        }
        answer_Manager.setAnsCorrect();
        Debug.Log("Ans : " + answer_Manager.getAnsCheck());
        modalWindowPanel.ShowMissonGUI(WindowAssets.Instance.getMissionOneComplete());
        return;
        
    }

    public void resetAnswer(){
        inventory = uiInventory.getInventory().resetItemList();
        uiInventory.SetInventory(inventory);
        ui_coding = transform.parent.Find("UI_Coding");
        foreach (Transform child in ui_coding) {
              if(child.Find("Background")){
                Image image = child.Find("Background").GetComponent<Image>();
                image.sprite = null;

              }
        }

        answer_Manager = Answer_manager.Instance;
        answer_Manager.clearAnsList();
        Debug.Log(transform.parent.Find("UI_Coding"));
    }

}
