using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour
{
    public InputField userNameInput;
    public Button startButton;

    private void Update() {
        OnChangeUserNameInput();
    }

    public void OnChangeUserNameInput(){
        if(userNameInput.text.Length >= 3){
            startButton.interactable = true;
        }
        else{
            startButton.interactable = false;
        }
    }

    public void SetUserName()
    {
        PhotonNetwork.NickName = userNameInput.text;
        SceneManager.LoadScene("Lobby");
    }
}
