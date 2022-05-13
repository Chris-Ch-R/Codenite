using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Update(){
        Destroy(gameObject,2f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
