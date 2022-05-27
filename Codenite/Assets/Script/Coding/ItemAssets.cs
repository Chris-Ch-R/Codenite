using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }
    public Sprite fragment1;
    public Sprite fragment2;
    public Sprite fragment3;
    public Sprite fragment4;
    public Sprite fragment5;
    public Sprite fragment6;
}
