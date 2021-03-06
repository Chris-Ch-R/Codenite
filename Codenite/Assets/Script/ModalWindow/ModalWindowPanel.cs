using System;
using System.Windows;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class ModalWindowPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform box;
    [SerializeField] private Image uiImage;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Transform errWindowBox;
    [SerializeField] private Image uiErrImage;
    [SerializeField] private Button confirmErrButton;
    [SerializeField] private TabGroup tabGroup;

    private Action onConfirmAction;

    public void Confirm(){
        Debug.Log("click");
        // transform.gameObject.SetActive(false);
        // tabGroup.objectToSwap[0].SetActive(false);
        // tabGroup.objectToSwap[1].SetActive(true);
        // onConfirmAction?.Invoke();
        // Close();
        PhotonNetwork.LeaveRoom();
    }

    public void ShowMissonGUI(Sprite missionImage){
        transform.gameObject.SetActive(true);
        box.gameObject.SetActive(true);
        errWindowBox.gameObject.SetActive(false);
        
        uiImage.sprite = missionImage;

        // onConfirmCallBack = confirmAction;

    }

    public void ShowErrorGui(Sprite errImage){
        transform.gameObject.SetActive(true);
        box.gameObject.SetActive(false);
        uiErrImage.sprite = errImage;

        // onConfirmCallBack = confirmAction;

    }
    public void ConfirmErr(){
        Debug.Log("click");
        transform.gameObject.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("ConnectToServer");
    }
}
