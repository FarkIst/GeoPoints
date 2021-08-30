using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHelper : MonoBehaviour
{
    private void Start()
    {
        Input.compass.enabled = true;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, -Input.compass.trueHeading);
    }
}
