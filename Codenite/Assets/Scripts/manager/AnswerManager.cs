using UnityEngine;
using System;
using System.Collections.Generic;

public class AnswerManager : MonoBehaviour {
    
    public static AnswerManager Instance { get; private set;}

    // public string[] anss;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else{
            Destroy(gameObject);
        }
    }

    private List<int> ansList = new List<int>();

    public List<int> getAnswer() {
        return ansList;
    }

    public void setAnswer(int ans) {
        this.ansList.Add(ans);
    }

}