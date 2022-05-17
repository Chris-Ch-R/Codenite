using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void LateUpdate() {
        Vector3 movePosition = target.position + offset;
        transform.position = movePosition;

        transform.rotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);
    }
}
