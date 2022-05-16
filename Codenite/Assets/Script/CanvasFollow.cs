using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private void FixedUpdate() {
        Vector3 movePosition = target.position + offset;
        transform.position = movePosition;
    }
}
