using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    int currentIndex = 0;
    public string target = "Player";
    public Vector3 offset;
    public float damping;
    List<GameObject> Players = new List<GameObject>();
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        Players = FindActiveObject(GameObject.FindGameObjectsWithTag(target));
                Debug.Log("init player list");

    }

    void LateUpdate()
    {
        Players = FindActiveObject(GameObject.FindGameObjectsWithTag(target));
        Debug.Log("update player list");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            currentIndex += 1;
            if(currentIndex >= Players.Count)
            {
                currentIndex = 0;
            }
        }

        if(Players.Count > 0 && currentIndex < Players.Count)
        {
            if(Players[currentIndex].active)
            {
                Vector3 movePosition = Players[currentIndex].transform.position + offset;
                transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
            }
        }
        else
            return;
    }

    private List<GameObject> FindActiveObject(GameObject[] targets)
    {
        List<GameObject> inActive = new List<GameObject>();
        for(int i = 0; i<targets.Length; i++)
        {
            if(targets[i].active)
            {
                inActive.Add(targets[i]);
            }
        }

        return inActive;
    }
}
