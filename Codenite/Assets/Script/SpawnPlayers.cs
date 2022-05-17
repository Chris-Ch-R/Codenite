using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start() {
        Vector2 randomPosition = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
        GameObject follow = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }   
}
