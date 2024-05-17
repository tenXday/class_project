using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routate : MonoBehaviour
{
    [SerializeField] float roSpeed = 60f;
    [SerializeField] Vector3 roAxis = new Vector3(0, 20, 0);
    [SerializeField] bool isRo = true;
    // Update is called once per frame
    void Update()
    {
        if (isRo)
        {
            transform.Rotate(roAxis, Time.unscaledDeltaTime * roSpeed, Space.World);
        }

    }
}
