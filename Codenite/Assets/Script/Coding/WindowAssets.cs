using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public static WindowAssets Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }
    public Sprite missionOneComplete;
    public Sprite missionTwoComplete;
    public Sprite missionThreeComplete;
    public Sprite missionError;

    public Sprite getMissionOneComplete(){
        return missionOneComplete;
    }
    public Sprite getMissionTwoComplete(){
        return missionTwoComplete;
    }
    public Sprite getMissionThreeComplete(){
        return missionThreeComplete;
    }
    public Sprite getMissionError(){
        return missionError;
    }
}
