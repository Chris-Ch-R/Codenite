using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int limitPlayerInZone = 1;
    public string targets;
    public Collider2D door;
    private int currentPlayerInZone;
    private void Update() {
        Debug.Log(currentPlayerInZone);
        if(currentPlayerInZone >= limitPlayerInZone)
        {
            door.isTrigger = false;
            Debug.Log("active door");
        }
        else
        {
            door.isTrigger = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            currentPlayerInZone += 1;
    }

      void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            currentPlayerInZone -= 1;
    }
}
