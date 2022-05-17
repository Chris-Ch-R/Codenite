using UnityEngine;

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

    private string[] anss;

    public string[] getAnswer() {
        return anss;
    }

    public void setAnswer(string[] ans) {
        this.anss = ans;
    }

}