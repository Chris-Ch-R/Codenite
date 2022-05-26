using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer_manager 
{
    public List<ItemSlot> ansSlotList = new List<ItemSlot>();

    public bool ansCheck;

    private static Answer_manager instance;
   
    public static Answer_manager Instance {
        get {
            if (instance == null) {
                instance = new Answer_manager ();
            }
            return instance;
        }
    }

    public void addAnswer(ItemSlot itemSlot)
    {
        this.ansSlotList.Add(itemSlot);

    }
    
    public void removeAnswer(ItemSlot item)
    {
        this.ansSlotList.Remove(item);

    }

    public List<ItemSlot> getAnser(){
        return ansSlotList;
    }

    public void clearAnsList(){
        ansSlotList.Clear();
        Debug.Log(ansSlotList.Count);
    }
    public bool getAnsCheck(){
        return ansCheck;
    }

    public void setAnsCorrect()
    {
        this.ansCheck = true;

    }
    public void setAnsInCorrect()
    {
        this.ansCheck = false;

    }
}