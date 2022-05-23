using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer_manager 
{
    public List<Item> ansList = new List<Item>();

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

    public void addAnswer(Item item)
    {
        this.ansList.Add(item);

    }
    
    public void removeAnswer(Item item)
    {
        this.ansList.Remove(item);

    }

    public List<Item> getAnser(){
        return ansList;
    }

    public void clearAnsList(){
        ansList.Clear();
        Debug.Log(ansList.Count);
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