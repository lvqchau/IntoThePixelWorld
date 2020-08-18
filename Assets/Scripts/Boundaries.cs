using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    void LateUpdate() {
       transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.6f, 7.6f), Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
    }
}
