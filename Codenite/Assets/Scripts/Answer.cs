using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory ;


    private Transform ui_coding;

    private Answer_manager answer_Manager;

    private string[] ans1 = {"count=1" , "count<=10" , "count++"};
    
    public void checkAnswer(){
        //TODO : fix bug แค่เลือกลำดับถูก ไม่ต้องถูกช่องก็ผ่านล้าา
        answer_Manager = Answer_manager.Instance;
        List<Item> listAns = answer_Manager.getAnser();


        for(int i = 0; i < ans1.Length; i++ ){
            Debug.Log("AnsList : " + answer_Manager.getAnser()[i].id + " vs " + listAns[i].id + " == " + listAns[i].value.Equals( ans1[i])  );

            if(!listAns[i].value.Equals( ans1[i] ) ){
                answer_Manager.setAnsInCorrect();
                Debug.Log("Ans : " + answer_Manager.getAnsCheck());
                return;
            }

        }

        answer_Manager.setAnsCorrect();
        Debug.Log("Ans : " + answer_Manager.getAnsCheck());

        return;
        
        
    }

    public void resetAnswer(){
        inventory = Inventory.Instance;
        
        Debug.Log(inventory.GetItemList().Count);

        Item item1 = new Item {id =  0, itemType = Item.ItemType.Common , value = "count=1" };
        Item item2 = new Item {id =  1, itemType =Item.ItemType.Rare , value = "count++" };
        Item item3 = new Item {id =  2, itemType =Item.ItemType.Epic , value = "count<=10" };
        inventory.AddItem(item1);
        inventory.AddItem(item2);
        inventory.AddItem(item3);
        
        
        // inventory = inventory.GetItemList
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
