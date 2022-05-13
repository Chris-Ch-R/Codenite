using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{

    public Transform playerTransform = null;
    public CinemachineVirtualCamera virtualCamera = null;
    private CinemachineTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        virtualCamera.gameObject.SetActive(true);

        enabled = true;
    }
}
