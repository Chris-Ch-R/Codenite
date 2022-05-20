using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float slowToSpeed = 2f; 
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<MyCharacterController>().SetSpeed(slowToSpeed);
        }
    }

        void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<MyCharacterController>().ResetSpeed();
        }
    }
}
