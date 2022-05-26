using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;


    [SerializeField] private ModalWindowPanel _modalWindow;

    public ModalWindowPanel modalWindow => _modalWindow;
    private void Awake(){
        instance = this;
    }
}
