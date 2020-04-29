using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAmount;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(rotationAmount);
    }
}
